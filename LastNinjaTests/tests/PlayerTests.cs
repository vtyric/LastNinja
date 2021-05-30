using NUnit.Framework;
using LastNinja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LastNinja.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [TestCase(30, 30, 0, 0, Direction.Up, 0, 0)]
        public void MoveTest(int x, int y, int mapWidth, int mapHeight, int sizeX, int sizeY,
            Direction direction, int expectedX, int expectedY)
        {
            var map = new Map(mapWidth, mapHeight);
            var player = new Player(map) {Size = (sizeX, sizeY), X = x, Y = y, Direction = direction};

            player.Move();

            player.X.Should().Be(expectedX);
            player.Y.Should().Be(expectedY);
        }
    }
}