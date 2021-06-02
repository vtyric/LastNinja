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

        public event Action<(int X, int Y, int Health), int, bool> PLayerStateChanged;

        private readonly Map map;
        private readonly HashSet<IGameObject> toDelete;
        private int warriorsCount = 3;
        private readonly Player player;
        private int score;
        private bool endGame;

        public Game(int mapWidth, int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IGameObject>();
            PlayerKeyController = new PlayerKeyController(player, map, DynamicObjects);
            toDelete = new HashSet<IGameObject>();
        }

        public void Start()
        {
            DynamicObjects.Add(new Warrior(player, map));
            DynamicObjects.Add(new Warrior(player, map));
            DynamicObjects.Add(new Warrior(player, map));
            DynamicObjects.Add(player);

            MakeStoneWall(650, 950, 400, 400);
            MakeStoneWall(300, 300, 250, 500);
            MakeStoneWall(650, 950, 200, 200);
            MakeStoneWall(100, 100, 250, 500);
        }

        public void GameTick()
        {
            MoveDynamicObjects();

            CheckDynamicObjectsInteraction();

            DeleteDisabledObjects();

            CheckWarriorsCount();

            SetPlayerState();
        }

        private void MakeStoneWall(int startX, int endX, int startY, int endY)
        {
            const int delta = 75;

            for (var x = startX; x <= endX; x += delta)
            for (var y = endY; y >= startY; y -= delta)
            {
                var stone = new Stone {X = x, Y = y};
                StaticObjects.Add(stone);
                map.Add(stone);
            }
        }

        private void SetPlayerState()
            => PLayerStateChanged?.Invoke((player.X, player.Y, player.Health), score, endGame);

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

        private void CheckWarriorsInteraction(Warrior warrior)
        {
            if (player.IsCollided(warrior) && warrior.IsWorking)
            {
                toDelete.Add(warrior);
                warrior.IsWorking = false;
                player.Health -= warrior.Damage;
                warriorsCount--;
                endGame = player.Health <= 0;
            }

            foreach (var suriken in DynamicObjects.Where(x
                => x is Suriken suriken && suriken.IsCollided(warrior) && warrior.IsWorking && suriken.IsWorking))
            {
                toDelete.Add(suriken);
                toDelete.Add(warrior);
                score++;
                warriorsCount--;
                suriken.IsWorking = false;
            }
        }

        private void DamageStaticObjects(IDynamicObject dynamicObject)
        {
            foreach (var staticObject in StaticObjects.Where(staticObject
                => dynamicObject.IsCollided(staticObject) && dynamicObject.IsWorking))
            {
                staticObject.Health -= dynamicObject.DamageToStaticObjects;

                if (staticObject.Health < 0)
                {
                    map.Remove(staticObject);
                    toDelete.Add(staticObject);
                }

                if (dynamicObject is Suriken suriken)
                {
                    suriken.IsWorking = false;
                    toDelete.Add(suriken);
                }
            }
        }

        private void CheckDynamicObjectsInteraction()
        {
            foreach (var dynamicObject in DynamicObjects)
            {
                if (dynamicObject is Warrior warrior)
                    CheckWarriorsInteraction(warrior);

                DamageStaticObjects(dynamicObject);
            }
        }

        private void DeleteDisabledObjects()
        {
            if (toDelete != null)
                foreach (var gameObject in toDelete)
                    if (gameObject is IDynamicObject dynamicObject)
                        DynamicObjects.Remove(dynamicObject);
                    else
                        StaticObjects.Remove(gameObject);

            toDelete?.Clear();
        }

        private void CheckWarriorsCount()
        {
            if (warriorsCount != 3)
            {
                DynamicObjects.Add(new Warrior(player, map));
                warriorsCount++;
            }
        }
    }
}