using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
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
        private Label controlLabel;
        private ProgressBar healthLabel;
        private Label scoreLabel;

        public int Score { get; private set; }

        public GameForm()
        {
            MakeForm();
            game = new Game(MapWidth, MapHeight);
            game.Start();

            KeyDown += game.PlayerKeyController.KeyIsDown;
            KeyUp += game.PlayerKeyController.KeyIsUp;

            timer = new Timer {Interval = 1};
            timer.Start();

            timer.Tick += (sender, args) => game.GameTick();
            Paint += DrawDynamicObjects;
            MakeLabels();

            game.PLayerStateChanged += UpdateLabels;
        }

        private void UpdateLabels((int X, int Y, int Health) player, int score, bool endGame)
        {
            if (endGame)
            {
                Score = score;
                Close();
                return;
            }

            scoreLabel.Text = $@"Score: {score}";
            healthLabel.Value = player.Health;
            healthLabel.Location = new Point(player.X, player.Y - 20 + UpLabelHeight);
            Invalidate();
        }

        private void DrawDynamicObjects(object sender, PaintEventArgs args)
        {
            var pen = new Pen(Color.Black, 20);
            args.Graphics.DrawRectangle(pen, 40, 60 + UpLabelHeight, MapWidth, MapHeight);

            foreach (var gameObject in game.StaticObjects.OfType<Stone>())
                args.Graphics.DrawImage
                    (Resource1.stone1, ((IGameObject) gameObject).X, ((IGameObject) gameObject).Y + UpLabelHeight);

            foreach (var gameObject in game.DynamicObjects)
            {
                if (gameObject is Player player)
                {
                    if (player.Direction == Direction.Right)
                        args.Graphics.DrawImage(Resource1.player_right, player.X, player.Y + UpLabelHeight);
                    if (player.Direction == Direction.Left)
                        args.Graphics.DrawImage(Resource1.player_left, player.X, player.Y + UpLabelHeight);
                    if (player.Direction == Direction.Up)
                        args.Graphics.DrawImage(Resource1.player_back, player.X, player.Y + UpLabelHeight);
                    if (player.Direction == Direction.Down)
                        args.Graphics.DrawImage(Resource1.player_right, player.X, player.Y + UpLabelHeight);
                }

                if (gameObject is Warrior warrior)
                    args.Graphics.DrawImage(Resource1.warrior, warrior.X, warrior.Y + UpLabelHeight);

                if (gameObject is Suriken suriken)
                    args.Graphics.DrawImage(Resource1.suriken, suriken.X, suriken.Y + UpLabelHeight);
            }
        }

        private void MakeLabels()
        {
            controlLabel = new Label
            {
                Text =
                    @"управление:
передвижение - стрелочками
кикинуть сюрикен - space",
                Font = new Font("Arial", 20),
                Location = new Point(1200, 40),
                Size = new Size(520, 300)
            };

            healthLabel = new ProgressBar {Size = new Size(70, 5)};

            scoreLabel = new Label
            {
                Location = new Point(MapWidth / 2, 20),
                Size = new Size(200, 30),
                Font = new Font("Arial", 20)
            };

            Controls.Add(healthLabel);
            Controls.Add(scoreLabel);
            Controls.Add(controlLabel);
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