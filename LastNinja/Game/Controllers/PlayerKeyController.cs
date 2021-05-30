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
                player.MoveUp = false;

            if (args.KeyCode == Keys.Down)
                player.MoveDown = false;

            if (args.KeyCode == Keys.Left)
                player.MoveLeft = false;

            if (args.KeyCode == Keys.Right)
                player.MoveRight = false;
        }

        public void KeyIsDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Up)
                player.MoveUp = true;

            if (args.KeyCode == Keys.Down)
                player.MoveDown = true;

            if (args.KeyCode == Keys.Left)
                player.MoveLeft = true;

            if (args.KeyCode == Keys.Right)
                player.MoveRight = true;
        }
    }
}