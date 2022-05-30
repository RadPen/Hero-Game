using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TheDreamFallen
{
	public partial class GameForm : Form
	{
		static public Choice Frm = new Choice();
		public string strImag;
		public int countText;
		public int move = 0;
		static public List<int> moveChoice = new List<int>();
		public GameForm()
		{
			WindowState = FormWindowState.Maximized;
			BackgroundImageLayout = ImageLayout.Stretch;

			NextBackground("../../../images/fon.jpg");
			var labelBackground = Image.FromFile("../../../images/labelbeck.jpg");
			strImag = "../../../images/Gostbeck.png";
			Paint += GameForm_Paint;
			Choice.strChoice = "Я не помню;Кто ты?;Где я?";

			var label = new Label
			{
				Image = labelBackground,
				ForeColor = Color.White,
			};
			
			var button = new Button
			{
				Text = ">",
				Image = labelBackground,
				ForeColor = Color.White,
				Font = new Font("Trebuchet MS", 18)
			};

			var plug = new Label
			{
				Image = labelBackground,
				ForeColor = Color.White,
				Font = new Font("Trebuchet MS", 18)
			};

			Controls.Add(button);
			Controls.Add(label);
			Controls.Add(plug);
			SizeChanged += (sender, args) =>
			{
				var height = 100;

				label.Location = new Point(0, ClientSize.Height - 100);
				label.Size = new Size((ClientSize.Width / 10) * 9, height);
				label.Font = new Font("Trebuchet MS", 14);
				button.Location = new Point((ClientSize.Width / 10) * 9, ClientSize.Height - 100);
				button.Size = new Size(ClientSize.Width / 10, height);
				plug.Location = new Point((ClientSize.Width / 10) * 9, ClientSize.Height - 100);
				plug.Size = new Size(ClientSize.Width / 10, height);

			};
			button.Click += (sender, args) =>
			{
				move++;
				if (move == 1)
				{
					strImag = "../../../images/Gostbeck.png";
					label.Text = CreateText("../../../text/text1.txt");
				}
				if (move == 2)
                {
					strImag = "../../../images/godpik.png";
					label.Text = CreateText("../../../text/text2.txt");
				}
				if (move == 3)
					label.Text = CreateText("../../../text/text3.txt");
				if (move == 4)
				{
					label.Text = CreateText("../../../text/deadmoretext1.txt");
					NextBackground("../../../images/deadmore.jpg");
				}
				if (move == 5)
				{
					Choice.strChoice = "Я не помню;Кто ты?;Где я?";
					Frm.ShowDialog();
				}
				if (move == 6)
				{
					if (moveChoice[0] == 0)
						label.Text = CreateText("../../../text/deadmoretext2.1.txt");
					if (moveChoice[0] == 1)
						label.Text = CreateText("../../../text/deadmoretext2.2.txt");
					if (moveChoice[0] == 2)
						label.Text = CreateText("../../../text/deadmoretext2.3.txt");
				}
			};
		}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//	Image img = Image.FromFile(strImag);
		//	Bitmap temp = new Bitmap(img);
		//	Graphics g = Graphics.FromImage(temp);

		//	//Приписываем g к нашему окну.
		//	g = this.CreateGraphics();
		//	int a = this.ClientSize.Width;
		//	int b = this.ClientSize.Height - 100;

		//	//Выводим на g рисунок.
		//	g.DrawImage(img, 0, 0, a, b);
		//	g.Dispose();
		//}

		public void NextBackground(string name)
		{
			Invalidate();
			this.BackgroundImage = new Bitmap(name);

		}

		public string CreateText(string name)
		{
			countText = name.Length;
			StreamReader sr = new StreamReader(name);

			return sr.ReadLine();
		}

		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			Image img = Image.FromFile(strImag);
			Bitmap temp = new Bitmap(img);
			Graphics g = Graphics.FromImage(temp);

			//Приписываем g к нашему окну.
			g = this.CreateGraphics();
			int a = this.ClientSize.Width;
			int b = this.ClientSize.Height - 100;

			//Выводим на g рисунок.
			g.DrawImage(img, 0, 0, a, b);
			g.Dispose();
		}
	}
}