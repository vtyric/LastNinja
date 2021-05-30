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

        private readonly Map map;
        private readonly Player player;

        public Game(int mapWidth,int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            PlayerKeyController = new PlayerKeyController(player);
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IStaticObject>();
        }

        public void Start()
        {
            var warrior1 = new Warrior(player, map) {X = 200, Y = 300};
            DynamicObjects.Add(warrior1);
            DynamicObjects.Add(player);

            StaticObjects.Add(new Stone {X = 100, Y = 100});
            StaticObjects.Add(new Stone {X = 200, Y = 200});
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