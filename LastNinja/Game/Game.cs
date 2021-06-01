using System;
using System.Collections.Generic;
using System.Linq;

namespace LastNinja
{
    public class Game
    {
        public PlayerKeyController PlayerKeyController { get; }
        public List<IGameObject> StaticObjects { get; }
        public List<IDynamicObject> DynamicObjects { get; }

        public event Action<(int X, int Y, int Health), int> PLayerStateChanged;

        private readonly Map map;
        private readonly HashSet<IDynamicObject> toDelete;
        private readonly HashSet<IGameObject> warriors;
        private readonly Player player;
        private int score;

        public Game(int mapWidth, int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IGameObject>();
            PlayerKeyController = new PlayerKeyController(player, map, DynamicObjects);
            toDelete = new HashSet<IDynamicObject>();
            warriors = new HashSet<IGameObject>();
        }

        public void Start()
        {
            var warrior1 = new Warrior(player, map) {X = 200, Y = 300};
            var warrior2 = new Warrior(player, map) {X = 500, Y = 600};
            warriors.Add(warrior1);
            warriors.Add(warrior2);
            DynamicObjects.Add(warrior1);
            DynamicObjects.Add(warrior2);
            DynamicObjects.Add(player);

            MakeStoneWall(650, 950, 400, 400);
            MakeStoneWall(300, 300, 250, 500);
        }

        private void MakeStoneWall(int startX, int endX, int startY, int endY)
        {
            for (var x = startX; x <= endX; x += 75)
            for (var y = endY; y >= startY; y -= 75)
                StaticObjects.Add(new Stone {X = x, Y = y});
        }

        private void SetState() => PLayerStateChanged?.Invoke((player.X, player.Y, player.Health), score);

        public void GameTick()
        {
            foreach (var dynamicObject in DynamicObjects)
                if (!StaticObjects.Any(x => x.IsCollided(dynamicObject)))
                {
                    var (x, y) = (dynamicObject.X, dynamicObject.Y);
                    dynamicObject.Move();

                    if (dynamicObject.IsWorking)
                    {
                        map.Field[x, y] = null;
                        map.Field[dynamicObject.X, dynamicObject.Y] = dynamicObject;
                        continue;
                    }

                    toDelete.Add(dynamicObject);
                }
                else
                {
                    if (dynamicObject.Direction == Direction.Right)
                        dynamicObject.X -= 10;
                    if (dynamicObject.Direction == Direction.Left)
                        dynamicObject.X += 10;
                    if (dynamicObject.Direction == Direction.Up)
                        dynamicObject.Y += 10;
                    if (dynamicObject.Direction == Direction.Down)
                        dynamicObject.Y -= 10;
                }

            foreach (var dynamicObject in DynamicObjects)
                if (dynamicObject is Warrior warrior)
                {
                    if (player.IsCollided(warrior))
                    {
                        toDelete.Add(warrior);
                        warrior.IsWorking = false;
                        warriors.Remove(warrior);
                        player.Health -= 2;
                    }

                    foreach (var suriken in DynamicObjects.Where(x => x is Suriken))
                        if (suriken.IsCollided(warrior) && warrior.IsWorking)
                        {
                            toDelete.Add(suriken);
                            toDelete.Add(warrior);
                            warriors.Remove(warrior);
                            score++;
                        }
                        else if (StaticObjects.Any(x => x.IsCollided(suriken)))
                        {
                            toDelete.Add(suriken);
                            suriken.IsWorking = false;
                        }
                }

            if (toDelete != null)
                foreach (var gameObject in toDelete)
                    DynamicObjects.Remove(gameObject);

            toDelete?.Clear();

            if (warriors.Count != 2)
            {
                var warrior = warriors.Count == 0
                    ? new Warrior(player, map) {X = 100, Y = 200}
                    : new Warrior(player, map) {X = 600, Y = 200};

                warriors.Add(warrior);
                DynamicObjects.Add(warrior);
            }

            SetState();
        }
    }
}