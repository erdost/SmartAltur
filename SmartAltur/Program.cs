using Newtonsoft.Json;
using SmartAltur.DTO;
using System.IO;
using SmartAltur.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;

namespace SmartAltur
{
    public class Program
    {
        static void Main(string[] args)
        {
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

            var (vehicles, totalDistance) = CalculateMultipleCarsPathForSmallestDistanceTraveled(startingPoint, passengers, allDistancesBetweenDestinations);

            Console.WriteLine(JsonConvert.SerializeObject(vehicles));

            double totalDistanceInKm = Math.Round(totalDistance / 1000, 2);

            Console.WriteLine($"Total distance {totalDistanceInKm} km");

            Console.ReadLine();
        }

        public static (IList<IList<IPassenger>> vehicles, double totalDistance) CalculateMultipleCarsPathForSmallestDistanceTraveled(GeoLoc startingPoint, List<IPassenger> passengers, List<Distance> allDistances)
        {
            IEnumerable<IList<IList<IPassenger>>> partitions = passengers.Partitions();

            var partitionIndexAndItsDistance = new Dictionary<int, double>();

            for (int i = 0; i < partitions.Count(); i++)
            {
                IList<IList<IPassenger>> groups = partitions.ElementAt(i);

                double totalDistance = 0;

                foreach (var group in groups)
                {
                    var vehicle = new Vehicle(startingPoint, group, allDistances);

                    totalDistance += vehicle.GetDistance();
                }

                partitionIndexAndItsDistance[i] = totalDistance;
            }

            var winner = partitionIndexAndItsDistance.OrderBy(kvp => kvp.Value).First();

            return (vehicles: partitions.ElementAt(winner.Key), totalDistance: winner.Value);
        }
    }
}

