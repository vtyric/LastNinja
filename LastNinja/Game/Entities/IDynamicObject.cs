namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        Direction Direction { get; }
    }
}