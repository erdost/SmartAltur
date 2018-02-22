using Newtonsoft.Json;
using SmartAltur.DTO;
using System.IO;
using SmartAltur.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;

namespace SmartAltur
{
    public class Program
    {
        static void Main(string[] args)
        {
            // I don't want to see a coma instead of a decimal point when printing doubles
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            string inputJson = @"ten_workers_from_kollektif.json";

            if (args.Length > 0)
            {
                inputJson = args[0];
            }

            var takeThemHome = JsonConvert.DeserializeObject<TakeThemHome>(File.ReadAllText(inputJson));

            var startingPoint = takeThemHome.StartingPoint;

            var passengers = takeThemHome.Workers.Select(i => Passenger.GetInstanceWithId(i.Name, i.Destination)).ToList();

            var allDistancesBetweenDestinations = DistanceFinder.GetAll(startingPoint, passengers).Result;

            Console.WriteLine("Got distance data from Google...");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var (vehicles, totalDistance) = CalculateMultipleCarsPathForSmallestDistanceTraveled(startingPoint, passengers, allDistancesBetweenDestinations);

            watch.Stop();
            int elapsedSeconds = watch.Elapsed.Seconds;
            Console.WriteLine($"Done in {elapsedSeconds} seconds");

            string json = JsonConvert.SerializeObject(vehicles);
            // Prettifying JSON
            Console.WriteLine(JValue.Parse(json).ToString(Formatting.Indented));

            double totalDistanceInKm = Math.Round(totalDistance / 1000, 2);

            Console.WriteLine($"Total distance {totalDistanceInKm} km");

            Console.ReadLine();
            return;
        }

        public static (IList<IList<IPassenger>> vehicles, double totalDistance) CalculateMultipleCarsPathForSmallestDistanceTraveled(GeoLoc startingPoint, List<IPassenger> allPassengers, List<Distance> allDistances)
        {
            IEnumerable<IList<IList<IPassenger>>> partitions = allPassengers.Partitions();

            int partitionCount = partitions.Count();

            var partitionIndexAndItsDistance = new Tuple<int, double>[partitionCount];

            var vehiclesWithDistances = new HashSet<Vehicle>();

            for (int i = 0; i < partitionCount; i++)
            //Parallel.For(0, partitions.Count(), i =>
            {
                IList<IList<IPassenger>> groups = partitions.ElementAt(i);

                double totalDistance = 0;

                foreach (var passengers in groups)
                {
                    var vehicle = new Vehicle(startingPoint, passengers, allDistances);

                    Vehicle withDistance;
                    bool isFound = vehiclesWithDistances.TryGetValue(vehicle, out withDistance);

                    if (isFound)
                    {
                        totalDistance += withDistance.GetDistance();
                    }
                    else
                    {
                        totalDistance += vehicle.GetDistance();
                        vehiclesWithDistances.Add(vehicle);
                    }
                }

                partitionIndexAndItsDistance[i] = new Tuple<int, double>(i, totalDistance);

                Console.SetCursorPosition(0, 1);
                Console.Write(partitionCount + " - " + i);
            };

            var winner = partitionIndexAndItsDistance.OrderBy(i => i.Item2).First();

            return (vehicles: partitions.ElementAt(winner.Item1), totalDistance: winner.Item2);
        }
    }
}

