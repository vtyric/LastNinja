using System;
using System.Drawing;
using System.Windows.Forms;

namespace LastNinja
{


    public class GameForm:Form
    {
        private readonly Game game;
        private readonly Timer timer;
        public GameForm()
        {
            MakeForm();
            game = new Game(1400, 700);
            game.Start();

            KeyDown += game.PlayerKeyController.KeyIsDown;
            KeyUp += game.PlayerKeyController.KeyIsUp;

            timer = new Timer {Interval = 1};
            timer.Start();

            timer.Tick += (sender, args) =>
            {
                game.GameTick();
                Invalidate();
            };

            Paint += DrawDynamicObjects;
        }

        private void DrawDynamicObjects(object sender, PaintEventArgs args)
        {
            foreach (var gameObject in game.StaticObjects)
            {
                if (gameObject is Stone)
                    args.Graphics.DrawImage(Resource1.stone1, gameObject.X, gameObject.Y);
            }

            foreach (var gameObject in game.DynamicObjects)
            {
                if (gameObject is Player)
                    args.Graphics.DrawImage(Resource1.player, gameObject.X, gameObject.Y);

                if (gameObject is Warrior)
                    args.Graphics.DrawImage(Resource1.warrior, gameObject.X, gameObject.Y);
            }
        }

        private void MakeForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1600, 800);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.Bisque;
        }
    }
}