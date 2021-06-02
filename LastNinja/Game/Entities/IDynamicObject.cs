namespace LastNinja
{
    public interface IDynamicObject : IGameObject
    {
        void Move();
        int DamageToStaticObjects { get; }
        bool IsWorking { get; set; }
    }
}