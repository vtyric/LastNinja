using System.Collections.Generic;

namespace LastNinja
{
    public class Game
    {
        public PlayerKeyController PlayerKeyController { get; }
        public List<IDynamicObject> DynamicObjects { get; }

        private readonly Map map;
        private readonly Player player;

        public Game(int mapWidth,int mapHeight)
        {
            map = new Map(mapWidth, mapHeight);
            player = new Player(map) {X = mapWidth / 2, Y = mapHeight / 2};
            PlayerKeyController = new PlayerKeyController(player);
            DynamicObjects = new List<IDynamicObject>();
        }

        public void Start()
        {
            DynamicObjects.Add(player);
        }

        public void GameTick()
        {
            foreach (var dynamicObject in DynamicObjects)
            {
                dynamicObject.Move();
            }
        }
    }
}