using FluentAssertions;
using NUnit.Framework;

namespace LastNinja.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [TestCase(100, 100, 2, 2, 0, 0, 0, 10, 2, 12)]
        [TestCase(100, 100, 2, 2, 10, 0, 0, 0, 12, 2)]
        [TestCase(100, 100, 2, 2, 0, 0, 0, 0, 2, 2)]
        [TestCase(100, 100, 2, 2, 10, 0, 0, 10, 12, 12)]
        [TestCase(100, 100, 2, 2, 10, -10, 0, 0, 2, 2)]
        [TestCase(100, 100, 2, 2, 20, -10, 0, 0, 12, 2)]
        [TestCase(100, 100, 2, 2, 0, -10, 0, 0, 2, 2)]
        [TestCase(100, 100, 2, 2, 0, 0, -10, 0, 2, 2)]
        [TestCase(1, 1, 1, 1, 0, 0, -10, 0, 1, 1)]
        [TestCase(100, 100, 90, 90, 20, 0, -10, 0, 90, 90)]
        [TestCase(100, 100, 90, 90, 0, -20, 0, 20, 90, 90)]
        public void MoveTestWithoutAnyObjects(int mapWidth, int mapHeight, int startX, int startY, int moveRight,
            int moveLeft,
            int moveUp, int moveDown, int shouldX, int shouldY)
        {
            var map = new Map(mapWidth, mapHeight);
            var player = new Player(map)
            {
                Size = (0, 0), X = startX, Y = startY, Down = moveDown, Left = moveLeft, Right = moveRight, Up = moveUp
            };

            player.Move();

            player.X.Should().Be(shouldX);
            player.Y.Should().Be(shouldY);
        }
    }
}