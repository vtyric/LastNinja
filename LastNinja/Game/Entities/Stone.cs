namespace LastNinja
{
    public class Stone : IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; } = (50, 60);
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;
    }
}