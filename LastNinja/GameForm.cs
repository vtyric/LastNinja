using System;
using System.Drawing;
using System.Windows.Forms;

namespace LastNinja
{
    public class GameForm : Form
    {
        private readonly Game game;
        private readonly Timer timer;
        private const int MapWidth = 1100;
        private const int MapHeight = 700;
        private const int UpLabelHeight = 20;

        public GameForm()
        {
            MakeForm();
            game = new Game(MapWidth, MapHeight);
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

            MakeLabels();
            Paint += DrawDynamicObjects;
        }

        private void DrawDynamicObjects(object sender, PaintEventArgs args)
        {
            var pen = new Pen(Color.Black, 20);
            args.Graphics.DrawRectangle(pen, 40, 60 + UpLabelHeight, MapWidth, MapHeight);

            foreach (var gameObject in game.StaticObjects)
                if (gameObject is Stone)
                    args.Graphics.DrawImage(Resource1.stone1, gameObject.X, gameObject.Y + UpLabelHeight);

            foreach (var gameObject in game.DynamicObjects)
            {
                if (gameObject is Player)
                    args.Graphics.DrawImage(Resource1.player, gameObject.X, gameObject.Y + UpLabelHeight);

                if (gameObject is Warrior)
                    args.Graphics.DrawImage(Resource1.warrior, gameObject.X, gameObject.Y + UpLabelHeight);

                if(gameObject is Suriken)
                    args.Graphics.DrawImage(Resource1.suriken, gameObject.X, gameObject.Y + UpLabelHeight);
            }
        }

        private void MakeLabels()
        {
            var controlLabel = new Label
            {
                Text =
                    @"управление:
передвижение - стрелочками
кикинуть сюрикен - space",
                Font = new Font("Arial", 20),
                Location = new Point(1200, 40),
                Size = new Size(520, 300)
            };

            var healthLabel = new ProgressBar
            {
                Value = game.Player.Health,
                Location = new Point(game.Player.X, game.Player.Y - 15 + UpLabelHeight),
                Size = new Size(70, 5)
            };

            var scoreLabel = new Label
            {
                Text = $@"Score: {game.Score}",
                Location = new Point(MapWidth / 2, 20),
                Size = new Size(200, 30),
                Font = new Font("Arial", 20)
            };

            Controls.Add(healthLabel);
            Controls.Add(scoreLabel);
            Controls.Add(controlLabel);

            timer.Tick += (sender, args) =>
            {
                scoreLabel.Text = $@"Score: {game.Score}";
                healthLabel.Value =game.Player.Health;
                healthLabel.Location = new Point(game.Player.X, game.Player.Y - 20 + UpLabelHeight);
            };
        }

        private void MakeForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1600, 800);
            BackColor = Color.Bisque;
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}