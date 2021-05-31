using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace LastNinja
{
    public class Game
    {
        public PlayerKeyController PlayerKeyController { get; }
        public List<IStaticObject> StaticObjects { get; }
        public List<IDynamicObject> DynamicObjects { get; }
        public int Score { get; private set; }
        public Player Player { get; }

        private readonly Map map;
        private readonly HashSet<IDynamicObject> toDelete;
        private readonly HashSet<IGameObject> warriors;

        public Game(int mapWidth,int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            Player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IStaticObject>();
            PlayerKeyController = new PlayerKeyController(Player, map, DynamicObjects);
            toDelete = new HashSet<IDynamicObject>();
            warriors = new HashSet<IGameObject>();
        }

        public void Start()
        {
            var warrior1 = new Warrior(Player, map) {X = 200, Y = 300};
            var warrior2 = new Warrior(Player, map) { X = 500, Y = 600 };
            warriors.Add(warrior1);
            warriors.Add(warrior2);
            DynamicObjects.Add(warrior1);
            DynamicObjects.Add(warrior2);
            DynamicObjects.Add(Player);

            MakeStoneWall(650, 950, 400, 400);
            MakeStoneWall(300,300,250,500);
        }

        private void MakeStoneWall(int startX, int endX, int startY, int endY)
        {
            for (var x = startX; x <= endX; x += 75)
            for (var y = endY; y >= startY; y -= 75)
                StaticObjects.Add(new Stone {X = x, Y = y});
        }

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
                    if (Player.IsCollided(warrior))
                    {
                        toDelete.Add(warrior);
                        warrior.IsWorking = false;
                        warriors.Remove(warrior);
                        Player.Health -= 2;
                    }

                    foreach (var suriken in DynamicObjects.Where(x => x is Suriken))
                        if (suriken.IsCollided(warrior) && warrior.IsWorking)
                        {
                            toDelete.Add(suriken);
                            toDelete.Add(warrior);
                            warriors.Remove(warrior);
                            Score++;
                        }
                }

            if (toDelete != null)
                foreach (var gameObject in toDelete)
                    DynamicObjects.Remove(gameObject);

            toDelete?.Clear();

            if (warriors.Count != 2)
            {
                var warrior = warriors.Count == 0
                    ? new Warrior(Player, map) {X = 100, Y = 200}
                    : new Warrior(Player, map) {X = 600, Y = 200};

                warriors.Add(warrior);
                DynamicObjects.Add(warrior);
            }
        }
    }
}