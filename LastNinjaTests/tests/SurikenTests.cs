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
    public class SurikenTests
    {
        private const int Speed = 20;

        [TestCase(100, 100, 20, 20, Direction.Down, 20, 20 + Speed)]
        [TestCase(100, 100, 20, 20, Direction.Left, 20 - Speed, 20)]
        [TestCase(100, 100, 21, 20, Direction.Left, 21 - Speed, 20)]
        [TestCase(100, 100, 20, 10, Direction.Up, 20, 10)]
        [TestCase(100, 100, 80, 80, Direction.Down, 80, 80)]
        public void MoveTest(int width, int height, int x, int y, Direction direction, int expectedX, int expectedY)
        {
            var map = new Map(width, height);
            var player = new Player(map) {X = x, Y = y, Direction = direction, Size = (0, 0)};
            var suriken = new Suriken(map, player) {Size = (0, 0)};

            suriken.Move();

            suriken.X.Should().Be(expectedX);
            suriken.Y.Should().Be(expectedY);
        }
    }
}