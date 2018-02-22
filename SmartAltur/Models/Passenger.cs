using SmartAltur.DTO;
using System;

namespace SmartAltur.Models
{
    public class Passenger : IPassenger, IEquatable<IPassenger>
    {
        private static int idSeed = 1;

        public static IPassenger GetInstanceWithId(string name, GeoLoc destination)
        {
            var newPassengerWithId = new Passenger(idSeed, name, destination);
            idSeed = idSeed * 2;
            return newPassengerWithId;
        }

        public bool Equals(IPassenger other)
        {
            return ID == other.ID;
        }

        public GeoLoc Destination { get; private set; }

        public string Name { get; private set; }

        public int ID { get; private set; }

        private Passenger(int id, string name, GeoLoc destination)
        {
            ID = id;
            Name = name;
            Destination = destination;
        }
    }

    public interface IPassenger
    {
        GeoLoc Destination { get; }
        string Name { get; }
        int ID { get; }
    }
}
