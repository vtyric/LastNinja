namespace LastNinja
{
    public class Suriken : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (10, 10);
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
        public int Damage { get; } = 100;

        private readonly Map map;
        private readonly Direction direction;

        public Suriken(Map map, Player player)
        {
            this.map = map;
            (X, Y) = (player.X, player.Y);
            direction = player.Direction;
        }

        public void Move()
        {
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

            if (map.InBounds(x, y, Size.Dx, Size.Dy))
                (X, Y) = (x, y);
            else
                IsWorking = false;
        }
    }
}