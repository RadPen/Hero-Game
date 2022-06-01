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
		public enum Location
		{
			Ash,
			Cemetery,
			Forest,
			City,
			Tavern
		}

		public enum Object
		{
			Empty,
			NameGame,
			Goddess,
			Lightning,
		}

		//public class ObjectImage
  //      {
		//	public Image Image;
		//	public int Width;
		//	public int Height;
		//	public int PositionX;
		//	public int PositionY;

		//	public ObjectImage(Image image, int width, int height, int positionX, int positionY)
  //          {
		//		Image = image;
		//		Width = width;
		//		Height = height;
		//		PositionX = positionX;
		//		PositionY = positionY;
  //          }

		//	public ObjectImage(Image image)
		//	{
		//		Image = image;
		//	}
		//}

		static public Choice Frm = new Choice();
		static public List<int> HistoryChoice = new List<int>();

		public Image LabelBackground = Image.FromFile("../../../images/labelbeck.jpg");
		public int ÑountText;
		public int Move = 0;
		public int WidthForm;
		public int HeightForm;

		public Image ImageObject;

		private Dictionary<Location, Bitmap> background = CreateBackground();
		private Dictionary<Object, Image> objects = CreateObject();

		public GameForm()
		{
			WindowState = FormWindowState.Maximized;
			BackgroundImageLayout = ImageLayout.Stretch;

			NextBackground(Location.Ash);

			CreateImage(Object.NameGame);
			Paint += GameForm_Paint;

			var label = new Label
			{
				Image = LabelBackground,
				ForeColor = Color.White,
			};
			
			var button = new Button
			{
				Text = ">",
				Image = LabelBackground,
				ForeColor = Color.White,
				Font = new Font("Trebuchet MS", 18)
			};

			var namelable = new Label
			{
				Image = LabelBackground,
				ForeColor = Color.White,
				Font = new Font("Trebuchet MS", 18)
			};

			Controls.Add(button);
			Controls.Add(label);
			Controls.Add(namelable);
			SizeChanged += (sender, args) =>
			{
				var height = 100;

				label.Location = new Point(0, ClientSize.Height - 100);
				label.Size = new Size((ClientSize.Width / 10) * 9, height);
				label.Font = new Font("Trebuchet MS", 14);
				button.Location = new Point((ClientSize.Width / 10) * 9, ClientSize.Height - 100);
				button.Size = new Size(ClientSize.Width / 10, height);
				namelable.Location = new Point((ClientSize.Width / 10) * 9, ClientSize.Height - 100);
				namelable.Size = new Size(ClientSize.Width / 10, height);

			};
			button.Click += (sender, args) =>
			{
				Move++;
				switch (Move)
				{
					case 1:
						CreateImage(Object.Empty);
						label.Text = CreateText("../../../text/text1.txt");
						break;
					case 2:
						CreateImage(Object.Goddess);
						label.Text = CreateText("../../../text/text2.txt");
						break;
					case 3:
						label.Text = CreateText("../../../text/text3.txt");
						break;
					case 4:
						label.Text = CreateText("../../../text/deadmoretext1.txt");
						NextBackground(Location.Cemetery);
						break;
					case 5:
						Choice.strChoice = "ß íå ïîìíþ;Êòî òû?;Ãäå ÿ?";
						Frm.ShowDialog();
						break;
					case 6:
						if (HistoryChoice[0] == 0)
							label.Text = CreateText("../../../text/deadmoretext2.1.txt");
						if (HistoryChoice[0] == 1)
							label.Text = CreateText("../../../text/deadmoretext2.2.txt");
						if (HistoryChoice[0] == 2)
							label.Text = CreateText("../../../text/deadmoretext2.3.txt");
						break;
					default:
						break;
				}
			};
		}

		public void NextBackground(Location name)
		{
			Invalidate();
			this.BackgroundImage = background[name];

		}

		public string CreateText(string name)
		{
			ÑountText = name.Length;
			StreamReader sr = new StreamReader(name);

			return sr.ReadLine();
		}

		public void CreateImage(Object name)
		{
			Invalidate();
			ImageObject = objects[name];
		}

		public static Dictionary<Location, Bitmap> CreateBackground()
		{
			var background = new Dictionary<Location, Bitmap>();
			background.Add(Location.Ash, new Bitmap("../../../images/Background/ash.jpg"));
			background.Add(Location.Cemetery, new Bitmap("../../../images/Background/cemetery.jpg"));
			background.Add(Location.Forest, new Bitmap("../../../images/Background/forest.jpg"));
			return background;
		}

		public static Dictionary<Object, Image> CreateObject()
		{
			var objects = new Dictionary<Object, Image>();
			objects.Add(Object.Empty, Image.FromFile("../../../images/Object/empty.png"));
			objects.Add(Object.NameGame, Image.FromFile("../../../images/Object/namegame.png"));
			objects.Add(Object.Goddess, new Bitmap("../../../images/Object/goddess.png"));
			objects.Add(Object.Lightning, new Bitmap("../../../images/Object/lightning.png"));
			return objects;
		}

		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			Image img = ImageObject;

			var g = e.Graphics;
			WidthForm = ClientSize.Width;
			HeightForm = ClientSize.Height-100;

			g.DrawImage(img, 0, 0, WidthForm, HeightForm);
			g.Dispose();
		}
	}
}