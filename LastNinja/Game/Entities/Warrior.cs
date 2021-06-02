using System;

namespace LastNinja
{
    public class Warrior : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (30, 30);
        public int StaticObjectsDamage { get; } = 2;
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public int Damage { get; } = 5;

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player, Map map)
        {
            var rnd = new Random();
            X = rnd.Next(-300, 300) + player.X;
            Y = rnd.Next(200, 300) + player.Y;
            (X, Y) = map.InBounds(X, Y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X, Y)
                ? (X, Y)
                : (map.Width / rnd.Next(2, 5), map.Height / rnd.Next(2, 5));
            this.player = player;
            this.map = map;
        }

        public void Move()
        {
            const int speed = 5;
            var (dx, dy) = (Math.Sign(player.X - X) * speed, Math.Sign(player.Y - Y) * speed);

            if (map.InBounds(dx + X, Y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X + dx, Y))
                X += dx;

            if (map.InBounds(X, Y + dy, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(X, Y + dy))
                Y += dy;
        }
    }
}