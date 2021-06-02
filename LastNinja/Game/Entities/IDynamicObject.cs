namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        int PrevX { get; }
        int PrevY { get; }
    }
}