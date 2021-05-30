using System.Reflection;

namespace LastNinja
{
    public class Player : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
        public Direction Direction { get; set; } = Direction.Afk;

        private readonly Map map;

        public Player(Map map)
        {
            this.map = map;
        }

        public void Move()
        {
            const int speed = 10;
            var x = X;
            var y = Y;

            if (Direction == Direction.Up)
                y -= speed;

            if (Direction == Direction.Down)
                y += speed;

            if (Direction == Direction.Left)
                x -= speed;

            if (Direction == Direction.Right)
                x += speed;

            var newPlayer = new Player(map) {Direction = Direction, Size = (Size.Dx, Size.Dy), X = x, Y = y};
            if (map.InBounds(newPlayer) && !map.IsStaticObjectAtThisPoint(newPlayer))
                (X, Y) = (x, y);
        }
    }
}