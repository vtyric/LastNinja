using System;
using System.Collections.Generic;
using System.Linq;

namespace LastNinja
{
    public class Warrior : IDynamicObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int Dx, int Dy) Size { get; set; }
        public int Health { get; set; }
        public bool IsWorking { get; set; } = true;

        private readonly Player player;
        private readonly Map map;
        private const int Speed = 5;

        public Warrior(Player player, Map map)
        {
            var rnd = new Random();
            Size = (30, 30);
            this.player = player;
            this.map = map;
        }

        public List<(int X, int Y)> GetListPoints((int X, int Y) start, Map map)
        {
            var que = new Queue<(int X, int Y)>();
            que.Enqueue(start);
            var res = new List<(int X, int Y)>();
            var visited = new HashSet<(int X, int Y)>();

            while (que.Count != 0)
            {
                var cur = que.Dequeue();

                foreach (var next in GetNextPoints(cur).Where(n
                    => map.InBounds(n.X, n.Y, Size.Dx, Size.Dy) &&
                       !visited.Contains(n)))
                {
                    que.Enqueue(next);

                    if (player.IsCollided(new Warrior(player, map) {X = next.X, Y = next.Y}))
                        return res;

                    res.Add(next);
                    visited.Add(next);
                }
            }

            return null;
        }

        private static IEnumerable<(int X, int Y)> GetNextPoints((int X, int Y) start)
        {
            yield return (start.X + Speed, start.Y);
            yield return (start.X, start.Y + Speed);
            yield return (start.X - Speed, start.Y);
            yield return (start.X, start.Y - Speed);
        }

        public void Move()
        {
            const int speed = 5;
            var (x, y) = (X, Y);

            if (x > player.X)
                x -= speed;

            if (x < player.X)
                x += speed;

            if (y > player.Y)
                y -= speed;

            if (y < player.Y)
                y += speed;

            if (map.InBounds(x, y, Size.Dx, Size.Dy) && !map.IsSmthAtThisPoint(x, y))
                (X, Y) = (x, y);
        }
    }
}