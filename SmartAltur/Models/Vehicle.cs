using SmartAltur.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SmartAltur.Models
{
    public class Vehicle
    {
        private readonly IList<IPassenger> _passengers;

        private readonly GeoLoc _startingPoint;

        private readonly List<Distance> _distances;

        private double? _pathDistance;

        public Vehicle(GeoLoc startingPoint, IList<IPassenger> passenger, List<Distance> distances)
        {
            _startingPoint = startingPoint;
            _passengers = passenger;
            _distances = distances;
        }

        public double GetDistance()
        {
            if (_pathDistance == null)
            {
                _pathDistance = ShortestPath();
            }

            return _pathDistance.Value;
        }

        private double ShortestPath()
        {
            var graph = InitGraph();

            double shortestOfShortestPaths = double.MaxValue;

            // We run shortest path algorithm with each passenger as the end point 
            foreach (var passenger in _passengers)
            {
                var pathDistance = CalculatePathDistance(graph.ShortestPath('S', passenger.ID));

                if(pathDistance < shortestOfShortestPaths)
                {
                    shortestOfShortestPaths = pathDistance;
                }
            }
            return shortestOfShortestPaths;
        }

        private Graph InitGraph()
        {
            var g = new Graph();

            // S => 1, S => 2 ...
            var startToPassengerVertexDistances = new Dictionary<char, double>();
            foreach (var passenger in _passengers)
            {
                var startToPassenger = _distances.FirstOrDefault(i => i.From == "S" && i.To == passenger.ID.ToString());
                startToPassengerVertexDistances.Add(passenger.ID, startToPassenger.Meters);
            }
            g.AddVertex('S', startToPassengerVertexDistances);

            // 1 => 2, 2 => 1 ...            
            foreach (var passenger in _passengers)
            {
                var passengerToPassengerVertexDistances = new Dictionary<char, double>();
                foreach (var otherPassenger in _passengers)
                {
                    if (!passenger.Equals(otherPassenger)) // TODO: overwrite != and == 
                    {
                        var distanceBetweenTwo = _distances.FirstOrDefault(i => i.From == passenger.ID.ToString() && i.To == otherPassenger.ID.ToString());
                        passengerToPassengerVertexDistances.Add(otherPassenger.ID, distanceBetweenTwo.Meters);
                    }
                }
                g.AddVertex(passenger.ID, passengerToPassengerVertexDistances);
            }
            return g;
        }

        private double CalculatePathDistance(List<char> path)
        {
            double result = _distances.FirstOrDefault(i => i.From == "S" && i.To == path.Last().ToString()).Meters;

            for (int i = path.Count - 1; i > 0; i--)
            {
                result += _distances.FirstOrDefault(d => d.From == path[i].ToString() && d.To == path[i-1].ToString()).Meters;
            }

            return result;
        }
    }
}
