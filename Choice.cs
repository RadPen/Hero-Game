using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDreamFallen
{
    public partial class Choice : Form
    {
        static public string strChoice = "error";
        public Choice()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            var labelBackground = Image.FromFile("../../../images/labelbeck.jpg");
            var buttons = new List<Button>();
            var butname = strChoice.Split(';');

            var label = new Label
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, ClientSize.Height / (butname.Length + 1)),
                Text = "Сделайте выбор",
                Image = labelBackground,
                ForeColor = Color.White,
                Font = new Font("Trebuchet MS", 20)
        };
            Controls.Add(label);

            var bot = label.Bottom;

            InitializeComponent();
            foreach (var but in butname)
            {
                var button = new Button();
                button.Text = but;
                button.Location = new Point(0, bot);
                button.Size = Size = label.Size;
                bot = button.Bottom;
                buttons.Add(button);
                button.Image = labelBackground;
				button.ForeColor = Color.White;
				button.Font = new Font("Trebuchet MS", 18);
            }
            InitializeComponent();
            for (var i = 0; i < buttons.Count; i++)
                Controls.Add(buttons[i]);

            for (var i = 0; i < buttons.Count; i++)
                buttons[i].Click += (sender, args) => ClickComm(buttons.IndexOf((Button)sender));
        }

        private void ClickComm(int i)
        {
            GameForm.moveChoice.Add(i);
            this.Close();
        }
    }
}
