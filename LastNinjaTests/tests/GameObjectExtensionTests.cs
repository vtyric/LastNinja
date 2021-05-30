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
    [TestFixture()]
    public class GameObjectExtensionTests
    {
        
        [TestCase(10, 10, 5, 5, 15, 15, 10, 10, true)]
        [TestCase(20, 20, 5, 5, 40, 40, 5, 5, false)]
        [TestCase(10, 10, 10, 10, 20, 20, 20, 20, true)]
        [TestCase(0, 0, 10, 10, 20, 10, 20, 20, true)]
        [TestCase(20, 20, 20, 20, 0, 0, 10, 10, true)]
        public void IsCollidedTest(int firstX, int firstY, int firstSizeX, int firstSizeY, int secondX, int secondY,
            int secondSizeX, int secondSizeY,bool expected)
        {
            var first = new Stone { X = firstX, Y = firstY, Size = (firstSizeX, firstSizeY) };
            var second = new Stone {X = secondX, Y = secondY, Size = (secondSizeX, secondSizeY)};

            first.IsCollided(second).Should().Be(expected);
        }
    }
}