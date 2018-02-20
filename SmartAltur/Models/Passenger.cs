using SmartAltur.DTO;
using System.Collections.Generic;

namespace SmartAltur.Models
{
    public class Passenger
    {
        public GeoLoc Destination { get; private set; }

        public string Name { get; private set; }

        public int ID { get; private set; }

        private Dictionary<int, double> _distancesToOtherPassengers;

        public Passenger(int id, string name, GeoLoc destination)
        {
            ID = id;
            Name = name;
            Destination = destination;

            // Should calculate its destination distances to all the passenger's destination and the starting point
        }
    }
}
