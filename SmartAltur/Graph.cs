using System.Collections.Generic;

namespace SmartAltur
{
    // Taken from https://github.com/mburst/dijkstras-algorithm/blob/master/dijkstras.cs
    // Modified a little bit to accommodate double vertex weigths 
    public class Graph
    {
        Dictionary<char, Dictionary<char, double>> vertices = new Dictionary<char, Dictionary<char, double>>();

        public void AddVertex(char name, Dictionary<char, double> edges)
        {
            vertices[name] = edges;
        }

        
        public List<char> ShortestPath(char start, char finish)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, double>();
            var nodes = new List<char>();

            List<char> path = null;

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = double.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort(
                    (x, y) => {
                        if (distances[x] == distances[y]) return 0;
                        else if (distances[x] > distances[y]) return 1;
                        else  return -1;
                    });
                //nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == double.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }
}
