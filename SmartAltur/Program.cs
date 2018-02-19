using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAltur
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Char>() { 'A', 'B', 'C', 'D' };

            var partitions = items.Partitions();
            int num = 0;
            foreach (var partition in items.Partitions())
            {
                Console.Write(++num + ") ");
                for (int i = 0; i < partition.Count; i++)
                {
                    var group = partition[i];
                    Console.Write(string.Join("", group));
                    if (i + 1 != partition.Count)
                    {
                        Console.Write(" - ");
                    }

                }
                Console.WriteLine(); Console.WriteLine();
            }

            //var dada = new Combinations<Char>(items, 1);

            //foreach (var item in dada)
            //{
            //    item.ToList().ForEach(i => Console.Write(i));
            //    Console.Write(",");
            //}



            Console.ReadLine();
        }
    }

    public class Group
    {
        public IList<Passenger> Passengers { get; set; }

        public void Print()
        {
            Console.Write(" ( ");
            for (int i = 0; i < Passengers.Count; i++)
            {
                Passengers[i].Print();

                if (i + 1 != Passengers.Count)
                {
                    Console.Write(" ");
                }
            }

            Console.Write(" ) ");
            Console.WriteLine();
        }
    }

    public class Passenger
    {
        public GeoLoc Destination;
        public string Name;

        public void Print()
        {
            Console.Write(Name);
        }
    }

    public class GeoLoc
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
