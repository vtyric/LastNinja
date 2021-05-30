namespace LastNinja
{
    public interface IGameObject
    {
        int X { get; set; }
        int Y { get; set; }
        (int Dx,int Dy) Size { get; }
    }
}