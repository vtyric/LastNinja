using System.Windows.Forms;

namespace LastNinja
{
    public class PlayerKeyController
    {
        private readonly Player player;

        public PlayerKeyController(Player player)
        {
            this.player = player;
        }

        public void KeyIsUp(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Up)
                player.Direction = Direction.Afk;

            if (args.KeyCode == Keys.Down)
                player.Direction = Direction.Afk;

            if (args.KeyCode == Keys.Left)
                player.Direction = Direction.Afk;

            if (args.KeyCode == Keys.Right)
                player.Direction = Direction.Afk;
        }

        public void KeyIsDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Up)
                player.Direction = Direction.Up;

            if (args.KeyCode == Keys.Down)
                player.Direction = Direction.Down;

            if (args.KeyCode == Keys.Left)
                player.Direction = Direction.Left;

            if (args.KeyCode == Keys.Right)
                player.Direction = Direction.Right;
        }
    }
}