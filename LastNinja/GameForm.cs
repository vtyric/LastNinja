using System.Drawing;
using System.Windows.Forms;

namespace LastNinja
{
    public class GameForm:Form
    {
        public GameForm()
        {
            MakeForm();
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