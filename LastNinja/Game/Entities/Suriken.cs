using System;

namespace LastNinja
{
    public class Suriken:IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; }
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }

        private readonly Map map;
        private readonly Player player;
        private readonly Direction direction;

        public Suriken(Map map, Player player)
        {
            this.map = map;
            this.player = player;
            (X, Y) = (player.X, player.Y);
            Size = (10, 10);
            direction = player.Direction;
        }

        public void Move()
        {
            (PrevX, PrevY) = (X, Y);
            var (x, y) = (X, Y);
            const int speed = 20;

            if (direction == Direction.Down)
                y += speed;
            if (direction == Direction.Up)
                y -= speed;
            if (direction == Direction.Right)
                x += speed;
            if (direction == Direction.Left)
                x -= speed;

            if (map.InBounds(new Suriken(map, player) {X = x, Y = y}))
                (X, Y) = (x, y);
            else
                IsWorking = false;
        }
    }
}