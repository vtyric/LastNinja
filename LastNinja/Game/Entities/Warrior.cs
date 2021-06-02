using System;
using System.Collections.Generic;
using System.Linq;

namespace LastNinja
{
    public class Warrior : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (30, 30);
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player, Map map)
        {
            var rnd = new Random();
            X = rnd.Next(-300, 300) + player.X;
            Y = rnd.Next(200, 300) + player.Y;
            (X, Y) = map.InBounds(X, Y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X, Y)
                ? (X, Y)
                : (map.Width / 2, map.Height / 2);
            this.player = player;
            this.map = map;
        }

        public void Move()
        {
            const int speed = 5;

            if (X > player.X && map.InBounds(X - speed, Y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X - speed, Y))
                X -= speed;

            if (X < player.X && map.InBounds(X + speed, Y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X + speed, Y))
                X += speed;

            if (Y > player.Y && map.InBounds(X, Y - speed, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X, Y - speed))
                Y -= speed;

            if (Y < player.Y && map.InBounds(X, Y + speed, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X, Y + speed))
                Y += speed;
        }
    }
}