using System.Reflection;

namespace LastNinja
{
    public class Player : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
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

            var newPlayer = new Player(map) {Direction = Direction, Size = (Size.Dx, Size.Dy), X = x, Y = y};

            if (map.InBounds(newPlayer) && !map.IsSmthAtThisPoint(newPlayer))
                (X, Y) = (x, y);
        }
    }
}