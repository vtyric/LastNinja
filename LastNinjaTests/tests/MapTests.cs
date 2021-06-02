﻿using System.Linq;
using System.Runtime.InteropServices;
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


        [TestCase(0, 0, 0, 0, 100, 100, 0)]
        [TestCase(10, 10, 10, 1, 100, 100, 40)]
        [TestCase(10, 10, 10, 2, 100, 100, 80)]
        [TestCase(10, 10, 10, 3, 100, 100, 120)]
        [TestCase(10, 10, 20, 20, 20, 20, 400)]
        public void AddTest(int x, int y, int sizeDx, int sizeDy, int width, int height, int expected)
        {
            var map = new Map(width, height);
            var player = new Player(map) { X = x, Y = y, Size = (sizeDx, sizeDy) };

            map.Add(player);

            map.Field.Cast<IGameObject>().Count(gameObject => gameObject == player).Should().Be(expected);
        }

        [TestCase(0, 0, 10, 10,100,100, 0, 0, true)]
        [TestCase(0, 0, 10, 10, 100, 100, 2, 2, true)]
        [TestCase(0, 0, 10, 10, 100, 100, -1, 0, false)]
        [TestCase(10, 10, 1, 10, 100, 10, 0, 0, false)]
        [TestCase(10, 10, 10, 10, 100, 100, 19, 19, true)]
        public void IsSmthAtThisPointTest(int x, int y, int sizeDx, int sizeDy, int width, int height, int resX,
            int resY, bool expected)
        {
            var map = new Map(width, height);
            var player = new Player(map) {X = x, Y = y, Size = (sizeDx, sizeDy)};

            map.Add(player);

            map.IsSmthAtThisPoint(resX, resY).Should().Be(expected);
        }
    }
}