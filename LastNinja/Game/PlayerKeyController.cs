using System.Collections.Generic;
using System.Windows.Forms;

namespace LastNinja
{
    public class PlayerKeyController
    {
        private readonly Player player;
        private readonly Map map;
        private readonly List<IDynamicObject> dynamicObjects;

        public PlayerKeyController(Player player, Map map, List<IDynamicObject> dynamicObjects)
        {
            this.player = player;
            this.map = map;
            this.dynamicObjects = dynamicObjects;
        }

        public void KeyIsDown(object sender, KeyEventArgs args)
        {
            const int speed = 10;

            if (args.KeyCode == Keys.Up)
            {
                player.Up = -speed;
                player.Direction = Direction.Up;
            }

            if (args.KeyCode == Keys.Down)
            {
                player.Down = speed;
                player.Direction = Direction.Down;
            }

            if (args.KeyCode == Keys.Left)
            {
                player.Left = -speed;
                player.Direction = Direction.Left;
            }

            if (args.KeyCode == Keys.Right)
            {
                player.Right = speed;
                player.Direction = Direction.Right;
            }
        }

        public void KeyIsUp(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Up)
                player.Up = 0;

            if (args.KeyCode == Keys.Down)
                player.Down = 0;

            if (args.KeyCode == Keys.Left)
                player.Left = 0;

            if (args.KeyCode == Keys.Right)
                player.Right = 0;

            if (args.KeyCode == Keys.Space)
                dynamicObjects.Add(new Suriken(map, player));
        }
    }
}