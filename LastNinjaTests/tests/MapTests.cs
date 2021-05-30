using LastNinja;
using FluentAssertions;
using NUnit.Framework;

namespace LastNinja.Tests
{
    [TestFixture]
    public class MapTests
    {
        [TestCase(20, 20, 100, 100, 1000, 1000, true)]
        [TestCase(20, 20, 20, 20, 100, 100, true)]
        [TestCase(20, 20, 19, 19, 100, 100, false)]
        [TestCase(20, 20, 20, 19, 100, 100, false)]
        [TestCase(20, 20, 90, 20, 100, 100, false)]
        [TestCase(20, 20, 20, 80, 100, 100, false)]
        [TestCase(20, 20, 20, 79, 100, 100, true)]
        public void InBoundsTest(int sizeDx, int sizeDy, int x, int y, int mapWidth, int mapHeight, bool expected)
        {
            var map = new Map(mapWidth, mapHeight);
            var player = new Player(map) { Size = (sizeDx, sizeDy), X = x, Y = y };

            map.InBounds(player).Should().Be(expected);
        }

        [TestCase()]
        public void IsStaticObjectAtThisPointTest()
        {
            Assert.Fail();
        }
    }
}