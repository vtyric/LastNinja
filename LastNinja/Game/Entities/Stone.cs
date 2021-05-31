namespace LastNinja
{
    public class Stone : IStaticObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }

        public Stone()
        {
            Size = (50, 40);
        }
    }
}