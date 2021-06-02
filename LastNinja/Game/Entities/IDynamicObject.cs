namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        int StaticObjectsDamage { get; }
    }
}