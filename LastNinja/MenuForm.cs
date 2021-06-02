using System.Drawing;
using System.Windows.Forms;

namespace LastNinja
{
    public class MenuForm : Form
    {
        public MenuForm()
        {
            var height = 200;
            ClientSize = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;

            var startGameLabel = new Label
            {
                Location = new Point(ClientSize.Width / 16 - 80, ClientSize.Height / 2 - 100),
                Size = new Size(ClientSize.Width, height),
                Image = Resource1.start_game
            };
            var game = new GameForm();

            var controlLabel = new Label
            {
                Text =
                    @"управление:
передвижение - стрелочками
кикинуть сюрикен - space


Максимальный счет: 0",
                Font = new Font("Arial", 20),
                Location = new Point(ClientSize.Width/3, 20),
                Size = new Size(400, 200)
            };

            SizeChanged += (sender, args) =>
            {
                startGameLabel.Location = new Point(ClientSize.Width / 16 - 80, ClientSize.Height / 2 - 100);
                controlLabel.Location = new Point(ClientSize.Width / 3, 20);
                startGameLabel.Size = new Size(ClientSize.Width, height);
            };

            Controls.Add(controlLabel);
            Controls.Add(startGameLabel);

            startGameLabel.Click += (sender, args) =>
            {
                Hide();
                game.FormClosed += (o, eventArgs) =>
                {
                    Show();
                    controlLabel.Text = $@"управление:
передвижение - стрелочками
кикинуть сюрикен - space


Максимальный счет: {game.MaxScore}";
                    startGameLabel.Image = Resource1.restart;
                };
                game.Show();
            };
        }
    }
}