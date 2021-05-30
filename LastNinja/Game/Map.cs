namespace LastNinja
{
    public class Map
    {
        public IGameObject[,] Field { get; set; }

        public Map(int mapWidth,int mapHeight)
        {
            Field = new IGameObject[mapWidth, mapHeight];
        }

        public int Width => Field.GetLength(0);
        public int Height => Field.GetLength(1);

        public bool InBounds(IGameObject gameObject) 
            => gameObject.X >= gameObject.Size.Dx
               && gameObject.X < Width - gameObject.Size.Dx
               && gameObject.Y >= gameObject.Size.Dy
               && gameObject.Y < Height - gameObject.Size.Dy;
    }
}