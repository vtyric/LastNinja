namespace LastNinja
{
    public interface IGameObject
    {
        int X { get; }
        int Y { get; }
        (int Dx,int Dy) Size { get; }
    }
}