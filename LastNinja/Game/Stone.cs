namespace LastNinja
{
    public class Stone : IStaticObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; }

        public Stone()
        {
            Size = (40, 40);
        }
    }
}