using System.Collections.Generic;

namespace SmartAltur.Models
{
    public class Vehicle
    {
        private readonly IList<Passenger> _passengers;

        private double _routeDistance;

        public Vehicle(IList<Passenger> passenger)
        {
            _passengers = passenger;
        }
    }
}
