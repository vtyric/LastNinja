namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        int Damage { get; }
    }
}