namespace LastNinja
{
    public class Stone : IStaticObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;

        public Stone()
        {
            Size = (50, 40);
        }
    }
}