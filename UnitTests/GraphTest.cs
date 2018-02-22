using SmartAltur;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class GraphTest
    {
        [Fact]
        public void Shortest_Path_Between_A_C_Should_Pass_By_B()
        {
            Graph g = new Graph();
            g.AddVertex(1, new Dictionary<int, double>() { { 2, 1 }, { 10, 2 } });
            g.AddVertex(2, new Dictionary<int, double>() { { 1, 1 }, { 3, 1 } });
            g.AddVertex(3, new Dictionary<int, double>() { { 2, 1 }, { 10, 1 } });
            g.AddVertex(10, new Dictionary<int, double>() { { 1, 2 }, { 3, 1 } });

            List<int> path = g.ShortestPath(1, 3);

            List<int> expectedPath = new List<int> { 3, 2 };

            Assert.Equal<List<int>>(expectedPath, path);
        }

        [Fact]
        public void Shortest_Path_Between_A_H_Should_Pass_By_B_F()
        {
            Graph g = new Graph();
            g.AddVertex(1, new Dictionary<int, double>() { { 2, 7 }, { 3, 8 } });
            g.AddVertex(2, new Dictionary<int, double>() { { 1, 7 }, { 6, 2 } });
            g.AddVertex(3, new Dictionary<int, double>() { { 1, 8 }, { 6, 6 }, { 7, 4 } });
            g.AddVertex(4, new Dictionary<int, double>() { { 6, 8 } });
            g.AddVertex(5, new Dictionary<int, double>() { { 8, 1 } });
            g.AddVertex(6, new Dictionary<int, double>() { { 2, 2 }, { 3, 6 }, { 4, 8 }, { 7, 9 }, { 8, 3 } });
            g.AddVertex(7, new Dictionary<int, double>() { { 3, 4 }, { 6, 9 } });
            g.AddVertex(8, new Dictionary<int, double>() { { 5, 1 }, { 6, 3 } });

            List<int> path = g.ShortestPath(1, 8);
            
            List<int> expectedPath = new List<int> { 8, 6, 2 };

            Assert.Equal<List<int>>(expectedPath, path);
        }
    }
}
