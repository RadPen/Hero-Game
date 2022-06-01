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
		static public List<string> HistoryChoice = new List<string>();

		public Image LabelBackground = Image.FromFile("../../../images/labelbeck.jpg");
		public int Move = 0;
		public int WidthForm;
		public int HeightForm;
		public Image ImageObject;
		public Label StoryLabel;
		public Label NamePerson;
		public Button ButtonNext;

		private Dictionary<Location, Bitmap> background = CreateBackground();
		private Dictionary<Object, Image> objects = CreateObject();
		private Dictionary<int, string[]> storys = CreateStory();

		public GameForm()
		{
			WindowState = FormWindowState.Maximized;
			BackgroundImageLayout = ImageLayout.Stretch;

			NextBackground(Location.Ash);
			CreateImage(Object.NameGame);
			Paint += GameForm_Paint;

			StoryLabel = new Label
			{
				Text = "Íà÷àòü èãðó",
				Font = new Font("Trebuchet MS", 14),
				Image = LabelBackground,
				ForeColor = Color.LightYellow,
			};

			ButtonNext = new Button
			{
				Text = ">",
				Image = LabelBackground,
				Font = new Font("Trebuchet MS", 20),
				ForeColor = Color.LightYellow,
			};

			NamePerson = new Label
			{
				Text = "? ? ?",
				Image = LabelBackground,
				ForeColor = Color.LightGoldenrodYellow,
				Font = new Font("Trebuchet MS", 16),
				TextAlign = ContentAlignment.MiddleCenter,
			};

			Controls.Add(ButtonNext);
			Controls.Add(StoryLabel);
			Controls.Add(NamePerson);
			SizeChanged += (sender, args) =>
			{
				var height = 100;

				StoryLabel.Location = new Point(0, ClientSize.Height - 100);
				StoryLabel.Size = new Size((ClientSize.Width / 10) * 9, height);
				StoryLabel.Font = new Font("Trebuchet MS", 14);
				ButtonNext.Location = new Point((ClientSize.Width / 10) * 9, ClientSize.Height - height);
				ButtonNext.Size = new Size(ClientSize.Width / 10, height);
				NamePerson.Location = new Point(0, ClientSize.Height - (height + (ClientSize.Height / 20)));
				NamePerson.Size = new Size(ClientSize.Width / 10, ClientSize.Height / 20);
				NamePerson.Font = new Font("Trebuchet MS", 20 - (NamePerson.Text.Length));
			};

			var step = 0;
			var roomStory = Ñube.RollTheDice(Move);
			var story = storys[roomStory];

			ButtonNext.Click += (sender, args) =>
			{
				var skip = 1;
				if (step >= story.Length)
				{
					Move++;
					if (Move >= storys.Count)
					{
						Move = 0;
					}
					roomStory = Ñube.RollTheDice(Move);
					story = storys[roomStory];
					step = 0;
				}
				var stepText = story[step].Split(';');
				while (stepText[0] == ">" || stepText[0] == "#" || stepText[0] == "%")
				{
					if (stepText[0] == ">")
						NextBackground(StringToLocation(stepText[1]));
					if (stepText[0] == "#")
						CreateImage(StringToObject(stepText[1]));
					if (stepText[0] == "%")
						CreateChoice(stepText[1]);
				step++;
				stepText = story[step].Split(';');
				}
				if (stepText[0] == "$")
				{
					if (HistoryChoice[int.Parse(stepText[1])] != stepText[2])
						skip += int.Parse(stepText[3]);
					step += skip;
					stepText = story[step].Split(';');
				}
				CreateText(stepText);
				step++;
			};
		}

		public Location StringToLocation(string location)
        {
			switch (location)
			{
				case "Ash":
					return Location.Ash;
				case "Cemetery":
					return Location.Cemetery;
				case "Forest":
					return Location.Forest;
				case "Tavern":
					return Location.Tavern;
				default:
					throw new ArgumentNullException();
			}
		}

		public Object StringToObject(string image)
		{
			switch (image)
			{
				case "Empty":
					return Object.Empty;
				case "NameGame":
					return Object.NameGame;
				case "Goddess":
					return Object.Goddess;
				case "Lightning":
					return Object.Lightning;
				default:
					return Object.Empty;
			}
		}

		public void NextBackground(Location name)
		{
			Invalidate();
			BackgroundImage = background[name];

		}

		public void CreateText(string[] text)
		{
			StoryLabel.Text = text[1];
			NamePerson.Text = text[0];
		}

		public static string[] CreateSuperText(string name)
		{
			var sr = File.ReadAllLines(name);

			return sr;
		}

		public void CreateImage(Object name)
		{
			Invalidate();
			ImageObject = objects[name];
		}

		public void CreateChoice(string name)
		{
			Choice.strChoice = name;
			Frm.ShowDialog();
		}

		public static Dictionary<Location, Bitmap> CreateBackground()
		{
			var background = new Dictionary<Location, Bitmap>();
			background.Add(Location.Ash, new Bitmap("../../../images/Background/ash.jpg"));
			background.Add(Location.Cemetery, new Bitmap("../../../images/Background/cemetery.jpg"));
			background.Add(Location.Forest, new Bitmap("../../../images/Background/forest.jpg"));
			background.Add(Location.Tavern, new Bitmap("../../../images/Background/tavern.jpg"));
			return background;
		}

		public static Dictionary<Object, Image> CreateObject()
		{
			var objects = new Dictionary<Object, Image>();
			objects.Add(Object.Empty, Image.FromFile("../../../images/Object/empty.png"));
			objects.Add(Object.NameGame, Image.FromFile("../../../images/Object/namegame.png"));
			objects.Add(Object.Goddess, Image.FromFile("../../../images/Object/goddess.png"));
			objects.Add(Object.Lightning, Image.FromFile("../../../images/Object/lightning.png"));
			return objects;
		}

		public static Dictionary<int, string[]> CreateStory()
		{
			var story = new Dictionary<int, string[]>();
			story.Add(0, CreateSuperText("../../../text/revival.txt"));
			return story;
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