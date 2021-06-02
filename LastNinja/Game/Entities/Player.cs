namespace LastNinja
{
    public class Player : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (30, 30);
        public int Health { get; set; } = 100;
        public int DamageToStaticObjects { get; } = 0;
        public bool IsWorking { get; set; } = true;
        public int Right { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }
        public int Left { get; set; }
        public Direction Direction { get; set; } = Direction.Right;

        private readonly Map map;

        public Player(Map map)
        {
            this.map = map;
        }

        public void Move()
        {
            var (x, y) = (X, Y);
            x += Right + Left;
            y += Up + Down;

            if (map.InBounds(x, y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(x, y))
                (X, Y) = (x, y);
        }
    }
}