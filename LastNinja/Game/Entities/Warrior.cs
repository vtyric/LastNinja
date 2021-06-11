using System;

namespace LastNinja
{
    public class Warrior : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (30, 30);
        public int DamageToStaticObjects { get; } = 2;
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public int Damage { get; } = 5;

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player, Map map)
        {
            this.player = player;
            this.map = map;
            (X, Y) = GeneratePosition();
        }

        private (int, int) GeneratePosition()
        {
            var rnd = new Random();
            var x = rnd.Next(-300, 300) + player.X;
            var y = rnd.Next(200, 300) + player.Y;

            if (map.InBounds(x, y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(x, y))
                return (x, y);

            var next = rnd.Next(1, 3);

            switch (next)
            {
                case 1:
                    return (map.Width / 2, map.Height / 2);
                case 2:
                    return (200, 100);
                default:
                    return (800, 600);
            }
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