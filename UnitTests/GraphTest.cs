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
            g.AddVertex('A', new Dictionary<char, double>() { { 'B', 1 }, { 'X', 2 } });
            g.AddVertex('B', new Dictionary<char, double>() { { 'A', 1 }, { 'C', 1 } });
            g.AddVertex('C', new Dictionary<char, double>() { { 'B', 1 }, { 'X', 1 } });
            g.AddVertex('X', new Dictionary<char, double>() { { 'A', 2 }, { 'C', 1 } });

            List<char> path = g.ShortestPath('A', 'C');

            List<char> expectedPath = new List<char> { 'C', 'B' };

            Assert.Equal<List<char>>(expectedPath, path);
        }

        [Fact]
        public void Shortest_Path_Between_A_H_Should_Pass_By_B_F()
        {
            Graph g = new Graph();
            g.AddVertex('A', new Dictionary<char, double>() { { 'B', 7 }, { 'C', 8 } });
            g.AddVertex('B', new Dictionary<char, double>() { { 'A', 7 }, { 'F', 2 } });
            g.AddVertex('C', new Dictionary<char, double>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
            g.AddVertex('D', new Dictionary<char, double>() { { 'F', 8 } });
            g.AddVertex('E', new Dictionary<char, double>() { { 'H', 1 } });
            g.AddVertex('F', new Dictionary<char, double>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
            g.AddVertex('G', new Dictionary<char, double>() { { 'C', 4 }, { 'F', 9 } });
            g.AddVertex('H', new Dictionary<char, double>() { { 'E', 1 }, { 'F', 3 } });

            List<char> path = g.ShortestPath('A', 'H');

            List<char> expectedPath = new List<char> { 'H', 'F', 'B' };

            Assert.Equal<List<char>>(expectedPath, path);
        }
    }
}
