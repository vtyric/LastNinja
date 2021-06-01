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
                Location = new Point(ClientSize.Width /16-80, ClientSize.Height / 2-100),
                Size = new Size(ClientSize.Width, height),
                Image = Resource1.start_game
            };

            SizeChanged += (sender, args) =>
            {
                startGameLabel.Location = new Point(ClientSize.Width / 16-80, ClientSize.Height / 2-100 );
                startGameLabel.Size = new Size(ClientSize.Width, height);
            };

            Controls.Add(startGameLabel);

            startGameLabel.Click += (sender, args) =>
            {
                var game = new GameForm();
                Hide();
                game.FormClosed += (o, eventArgs) => Close();
                game.Show();
            };
        }
    }
}