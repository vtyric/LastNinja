namespace LastNinja
{
    public class Map
    {
        public IGameObject[,] Field { get; }

        public Map(int mapWidth, int mapHeight)
        {
            Field = new IGameObject[mapWidth, mapHeight];
        }

        public int Width => Field.GetLength(0);
        public int Height => Field.GetLength(1);

        public void Add(IGameObject gameObject)
        {
            var startX = gameObject.X - gameObject.Size.Dx < 0 ? 0 : gameObject.X - gameObject.Size.Dx;
            var startY = gameObject.Y - gameObject.Size.Dy < 0 ? 0 : gameObject.Y - gameObject.Size.Dy;
            var endX = gameObject.X + gameObject.Size.Dx >= Width ? Width : gameObject.X + gameObject.Size.Dx;
            var endY = gameObject.Y + gameObject.Size.Dy >= Height ? Height : gameObject.Y + gameObject.Size.Dy;

            for (var x = startX; x < endX; x++)
            for (var y = startY; y < endY; y++)
                Field[x, y] = gameObject;
        }

        public void Remove(IGameObject gameObject)
        {
            var startX = gameObject.X - gameObject.Size.Dx < 0 ? 0 : gameObject.X - gameObject.Size.Dx;
            var startY = gameObject.Y - gameObject.Size.Dy < 0 ? 0 : gameObject.Y - gameObject.Size.Dy;
            var endX = gameObject.X + gameObject.Size.Dx >= Width ? Width : gameObject.X + gameObject.Size.Dx;
            var endY = gameObject.Y + gameObject.Size.Dy >= Height ? Height : gameObject.Y + gameObject.Size.Dy;

            for (var x = startX; x < endX; x++)
            for (var y = startY; y < endY; y++)
                Field[x, y] = null;
        }

        public bool InBounds(IGameObject gameObject)
            => gameObject.X >= gameObject.Size.Dx
               && gameObject.X < Width - gameObject.Size.Dx
               && gameObject.Y >= gameObject.Size.Dy
               && gameObject.Y < Height - gameObject.Size.Dy;

        public bool IsSmthAtThisPoint(int x, int y)
            => x > -1 && x < Width && y > -1 && y < Height && Field[x, y] != null;
    }
}