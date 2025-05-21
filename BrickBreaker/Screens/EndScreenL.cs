using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker.Screens
{
    public partial class EndScreenL : UserControl
    {
        public EndScreenL()
        {
            InitializeComponent();
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);

        }

        private void endButton_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void EndScreenL_Load(object sender, EventArgs e)
        {
            scoreLabel.Text = $"{GameScreen.score}";
        }
    }
}
