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
    public class WarriorTests
    {
        private const int Speed = 5;

        [TestCase(100, 100, 10, 10, 20, 20, 20 - Speed, 20 - Speed)]
        [TestCase(100, 100, 10, 10, 10, 10, 10, 10)]
        [TestCase(100, 100, 10, 10, 15, 15, 10, 10)]
        [TestCase(9, 9, 1, 1, 10, 10, 10, 10)]
        public void MoveTest(int width, int height, int playerX, int playerY, int warriorX, int warriorY, int expectedX,
            int expectedY)
        {
            var map = new Map(width, height);
            var player = new Player(map) {X = playerX, Y = playerY, Size = (0, 0)};
            var warrior = new Warrior(player, map) {Size = (0, 0), X = warriorX, Y = warriorY};

            warrior.Move();

            warrior.X.Should().Be(expectedX);
            warrior.Y.Should().Be(expectedY);
        }
    }
}