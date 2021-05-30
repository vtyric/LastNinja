namespace LastNinja
{
    public static class GameObjectExtension
    {
        public static bool IsCollided(this IGameObject first, IGameObject second)
            => first.X - first.Size.Dx < second.X + second.Size.Dx
               && second.X - second.Size.Dx < first.X + first.Size.Dx
               && first.Y - first.Size.Dy < second.Y + second.Size.Dy
               && second.Y - second.Size.Dy < first.Y + first.Size.Dy;
    }
}   