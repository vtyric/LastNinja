using System;

namespace LastNinja
{
    public class Warrior:IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player,Map map)
        {
            var rnd = new Random();
            Size = (30, 30);
            this.player = player;
            this.map = map;
            map.Add(this);
        }

        public void Move()
        {
            const int speed = 5;
            (PrevX, PrevY) = (X, Y);
            var (x, y) = (X, Y);

            if (x > player.X)
                x -= speed;

            if (x < player.X)
                x += speed;

            if (y > player.Y)
                y -= speed;

            if (y < player.Y)
                y += speed;

            if (map.InBounds(new Warrior(player, map) {X = x, Y = y}))
                (X, Y) = (x, y);
        }
    }
}