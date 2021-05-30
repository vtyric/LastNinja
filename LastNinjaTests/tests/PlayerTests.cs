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
        private const int speed = 10;

        [TestCase(30, 30, 100, 100, 20, 20, Direction.Up, 30, 30 - speed)]
        [TestCase(30, 30, 100, 100, 20, 20, Direction.Down, 30, 30 + speed)]
        [TestCase(30, 30, 100, 100, 20, 20, Direction.Left, 30 - speed, 30)]
        [TestCase(30, 30, 100, 100, 20, 20, Direction.Right, 30 + speed, 30)]
        [TestCase(20, 20, 100, 100, 20, 20, Direction.Up, 20, 20)]
        [TestCase(20, 90, 100, 100, 20, 20, Direction.Down, 20, 90)]
        public void MoveTestWithoutStaticObjects(int x, int y, int mapWidth, int mapHeight, int sizeX, int sizeY,
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