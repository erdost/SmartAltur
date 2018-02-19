using SmartAltur;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class PartitionTest
    {
        [Fact]
        public void Four_Element_Set_Should_Have_15_Partitions()
        {
            var setOfFourElement = new char[] { 'A', 'B', 'C', 'D' };

            IEnumerable<IList<IList<char>>> expectedPartitions = new List<char[][]>
            {
                new char[][] { new char[] { 'A', 'B', 'C', 'D' }},
                new char[][] { new char[] { 'A', 'B', 'C' }, new char[] { 'D' }},
                new char[][] { new char[] { 'A', 'B', 'D' }, new char[] { 'C' }},
                new char[][] { new char[] { 'A', 'B' }, new char[] { 'C', 'D' }},
                new char[][] { new char[] { 'A', 'B' }, new char[] { 'C' }, new char[] { 'D' } },
                new char[][] { new char[] { 'A', 'C', 'D' }, new char[] { 'B' }},
                new char[][] { new char[] { 'A', 'C' }, new char[] { 'B', 'D' }},
                new char[][] { new char[] { 'A', 'C' }, new char[] { 'B' }, new char[] { 'D' } },
                new char[][] { new char[] { 'A', 'D' }, new char[] { 'B', 'C' }},
                new char[][] { new char[] { 'A' }, new char[] { 'B', 'C', 'D' }},
                new char[][] { new char[] { 'A' }, new char[] { 'B', 'C' }, new char[] { 'D' } },
                new char[][] { new char[] { 'A', 'D' }, new char[] { 'B' }, new char[] { 'C' } },
                new char[][] { new char[] { 'A' }, new char[] { 'B', 'D' }, new char[] { 'C' } },
                new char[][] { new char[] { 'A' }, new char[] { 'B' }, new char[] { 'C', 'D' } },
                new char[][] { new char[] { 'A' }, new char[] { 'B' }, new char[] { 'C' }, new char[] { 'D' } }
            };

            IEnumerable<IList<IList<char>>> partitions = setOfFourElement.Partitions<Char>();

            Assert.Equal<IEnumerable<IList<IList<char>>>>(expectedPartitions, partitions);
        }
    }
}
