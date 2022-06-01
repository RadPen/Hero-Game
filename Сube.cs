using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDreamFallen
{
    internal class Сube : GameForm
    {
        private static int cubedice;
        static public int RollTheDice(int roll)
        {
            Random rnd = new Random();
            if (roll == 0)
                cubedice = 0;
            else
                cubedice = rnd.Next(1, roll+1);
            return cubedice;
        }
    }
}
