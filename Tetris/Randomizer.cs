using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Randomizer
    {
        private static Random rnd = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}
