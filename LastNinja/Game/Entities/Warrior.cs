namespace LastNinja
{
    public class Warrior:IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public Direction Direction { get; private set; }

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player,Map map)
        {
            Size = (30, 30);
            this.player = player;
            this.map = map;
        }

        public void Move()
        {
            const int speed = 5;
            var (x, y) = (X, Y);

            if (x > player.X)
            {
                x -= speed;
                Direction = Direction.Left;
            }

            if (x < player.X)
            {
                x += speed;
                Direction = Direction.Right;
            }

            if (y > player.Y)
            {
                y -= speed;
                Direction = Direction.Up;
            }

            if (y < player.Y)
            {
                y += speed;
                Direction = Direction.Down;
            }

            if (map.InBounds(new Warrior(player, map) {X = x, Y = y}))
                (X, Y) = (x, y);
        }
    }
}