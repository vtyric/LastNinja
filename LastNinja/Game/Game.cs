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
        private int warriorsCount = 2;
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
        }

        public void Start()
        {
            var warrior1 = new Warrior(player, map) {X = 200, Y = 300};
            var warrior2 = new Warrior(player, map) {X = 500, Y = 600};
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
            {
                var stone = new Stone {X = x, Y = y};
                StaticObjects.Add(stone);
                map.Add(stone);
            }
        }

        public void GameTick()
        {
            MoveDynamicObjects();

            CheckDynamicObjectsInteraction();

            DeleteDisabledObjects();

            CheckWarriorsCount();

            SetPlayerState();
        }

        private void SetPlayerState()
        {
            PLayerStateChanged?.Invoke((player.X, player.Y, player.Health), score);
        }

        private void MoveDynamicObjects()
        {
            foreach (var dynamicObject in DynamicObjects)
            {
                dynamicObject.Move();

                if (!dynamicObject.IsWorking)
                {
                    toDelete.Add(dynamicObject);
                    dynamicObject.IsWorking = false;
                }
            }
        }

        private void CheckDynamicObjectsInteraction()
        {
            foreach (var dynamicObject in DynamicObjects)
                if (dynamicObject is Warrior warrior)
                {
                    if (player.IsCollided(warrior) && warrior.IsWorking)
                    {
                        toDelete.Add(warrior);
                        warrior.IsWorking = false;
                        player.Health -= 2;
                        warriorsCount--;
                    }

                    foreach (var suriken in DynamicObjects.Where(x => x is Suriken))
                        if (suriken.IsCollided(warrior) && warrior.IsWorking)
                        {
                            toDelete.Add(suriken);
                            toDelete.Add(warrior);
                            score++;
                            warriorsCount--;
                        }
                        else if (StaticObjects.Any(x => x.IsCollided(suriken)))
                        {
                            toDelete.Add(suriken);
                            suriken.IsWorking = false;
                        }
                }
        }

        private void DeleteDisabledObjects()
        {
            if (toDelete != null)
                foreach (var gameObject in toDelete)
                    DynamicObjects.Remove(gameObject);

            toDelete?.Clear();
        }

        private void CheckWarriorsCount()
        {
            if (warriorsCount != 2)
            {
                var warrior = warriorsCount == 0
                    ? new Warrior(player, map) {X = 100, Y = 200}
                    : new Warrior(player, map) {X = 600, Y = 200};

                DynamicObjects.Add(warrior);
                warriorsCount++;
            }
        }
    }
}