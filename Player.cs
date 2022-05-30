using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDreamFallen
{
    public enum GameClass
    {
        Warrior,
        Magician,
        Killer,
        Priest,
        Knight
    }
    internal class Player
    {
        static private string name = "Безымянный";
        static private GameClass classGame;
        static private int lvl;
        public int level
        {
            get
            {
                return lvl;
            }
            set
            {
                lvl = progressLevel / 100;
            }
        }
        static private int progressLevel;
        public string Name
        {
            get
            {
                return name;
            }
            set { }
        }

        static public void HappyBirthday(string names, int gameClass)
        {
            name = names;
            classGame = (GameClass)gameClass;
        }
    }
}
