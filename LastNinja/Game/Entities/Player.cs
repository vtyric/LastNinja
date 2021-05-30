namespace LastNinja
{
    public class Player : IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
    }
}