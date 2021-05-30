namespace LastNinja
{
    public class Warrior:IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; }
        public Direction Direction { get; private set; }
        public int Speed { get; } = 4;

        private readonly Player player;
        private readonly Map map;

        public Warrior(Player player,Map map)
        {
            Size = (40, 40);
            this.player = player;
            this.map = map;
        }

        public void Move()
        {
            var (x, y) = (X, Y);

            if (x > player.X)
            {
                x -= Speed;
                Direction = Direction.Left;
            }

            if (x < player.X)
            {
                x += Speed;
                Direction = Direction.Right;
            }

            if (y > player.Y)
            {
                y -= Speed;
                Direction = Direction.Up;
            }

            if (y < player.Y)
            {
                y += Speed;
                Direction = Direction.Down;
            }

            if (map.InBounds(new Warrior(player,map) {X = x, Y = y}))
                (X, Y) = (x, y);
        }

    }
}