using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDreamFallen
{
    internal class Сube
    {
        private static int cubedice;
        static public int rollTheDice(int roll)
        {
            Random rnd = new Random();
            if (roll != 0)
                cubedice = rnd.Next(1, roll+1);
            return cubedice;
        }
    }
}
