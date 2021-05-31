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
        public int PlayerHealth { get; private set; } = 100;
        public Player Player { get; }

        private readonly Map map;

        public Game(int mapWidth,int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            Player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            PlayerKeyController = new PlayerKeyController(Player);
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IStaticObject>();
        }

        public void Start()
        {
            var warrior1 = new Warrior(Player, map) {X = 200, Y = 300};
            var warrior2 = new Warrior(Player, map) { X = 500, Y = 600 };
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
                    dynamicObject.Move();
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
        }
    }
}