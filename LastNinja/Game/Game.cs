using System.Collections.Generic;
using System.Linq;

namespace LastNinja
{
    public class Game
    {
        public PlayerKeyController PlayerKeyController { get; }
        public List<IGameObject> GameObjects { get; }

        private readonly Map map;
        private readonly Player player;
        public readonly List<IDynamicObject> dynamicObjects;

        public Game(int mapWidth,int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            PlayerKeyController = new PlayerKeyController(player);
            dynamicObjects = new List<IDynamicObject>();
            GameObjects = new List<IGameObject>();
        }

        public void Start()
        {
            var warrior1 = new Warrior(player, map){X=200,Y=300};
            GameObjects.Add(warrior1);
            dynamicObjects.Add(warrior1);

            dynamicObjects.Add(player);
            GameObjects.Add(player);
            
            GameObjects.Add(new Stone {X = 100, Y = 100});
            GameObjects.Add(new Stone {X = 200, Y = 200});
        }

        public void GameTick()
        {
            foreach (var dynamicObject in dynamicObjects)
            {
                if (!GameObjects.Any(x => x != dynamicObject && x.IsCollided(dynamicObject)))
                {
                    dynamicObject.Move();
                }
                else
                {
                    if (dynamicObject.Direction == Direction.Right)
                        dynamicObject.X -= dynamicObject.Speed;
                    if (dynamicObject.Direction == Direction.Left)
                        dynamicObject.X += dynamicObject.Speed;
                    if (dynamicObject.Direction == Direction.Up)
                        dynamicObject.Y += dynamicObject.Speed;
                    if (dynamicObject.Direction == Direction.Down)
                        dynamicObject.Y -= dynamicObject.Speed;
                }
            }
        }
    }
}