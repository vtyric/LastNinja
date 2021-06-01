using System.Drawing;
using System.Windows.Forms;

namespace LastNinja
{
    public class StartForm : Form
    {
        public StartForm()
        {
            var height = 100;
            ClientSize = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;

            var startGameLabel = new Label
            {
                Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2),
                Size = new Size(ClientSize.Width, height),
                Text = "Начать игру",
                Font = new Font("Arial", 30)
            };

            SizeChanged += (sender, args) =>
            {
                startGameLabel.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2 );
                startGameLabel.Size = new Size(ClientSize.Width, height);
            };

            Controls.Add(startGameLabel);

            startGameLabel.Click += (sender, args) =>
            {
                var game = new GameForm();
                game.Show();
            };
        }
    }
}