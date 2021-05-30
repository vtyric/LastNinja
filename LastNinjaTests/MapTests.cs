using FluentAssertions;
using NUnit.Framework;

namespace LastNinja.Tests
{
    [TestFixture()]
    public class MapTests
    {
        [TestCase(20, 20, 100, 100, 1000, 1000,true)]
        [TestCase(20, 20, 20, 20, 100, 100, true)]
        [TestCase(20, 20, 19, 19, 100, 100, false)]
        [TestCase(20, 20, 20, 19, 100, 100, false)]
        [TestCase(20, 20, 90, 20, 100, 100, false)]
        [TestCase(20, 20, 20, 80, 100, 100, false)]
        [TestCase(20, 20, 20, 79, 100, 100, true)]
        public void InBoundsTest(int sizeDx, int sizeDy, int X, int Y, int mapWidth, int mapHeight,bool expected)
        {
            var player = new Player {Size = (sizeDx, sizeDy), X = X, Y = Y};
            var map = new Map(mapWidth, mapHeight);

            map.InBounds(player).Should().Be(expected);
        }
    }
}