namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        Direction Direction { get; }
        int PrevX { get; }
        int PrevY { get; }
    }
}