using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTracker.Support
{
    public class Utilities
    {
        private Random die = new Random();

        static public int CharToHex(char c)
        {
            return int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
        }

        public int d6()
        {
            return (die.Next(1, 7));
        }
    }
}
