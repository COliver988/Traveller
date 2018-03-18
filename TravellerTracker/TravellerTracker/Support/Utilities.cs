using System;

namespace Traveller.Support
{
    public class Utilities
    {
        private Random die = new Random();

        static public int HexToInt(char c)
        {
            try
            {
                return int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int d6()
        {
            return (die.Next(1, 7));
        }

        public int d6(int dice)
        {
            int result = 0;
            for (int i = 0; i < dice; i++)
            {
                result += d6();
            }
            return result;
        }

        /// <summary>
        /// calculate the distance between two hexes
        /// </summary>
        /// <param name="W1">first hex number; 1234</param>
        /// <param name="W2">second hex number; 5678</param>
        /// <returns>int - distance in jumps/parsecs</returns>
        public int calcDistance(string W1, string W2)
        {
            int ST1 = Convert.ToInt32(W1.Substring(0, 2));
            int CR1 = Convert.ToInt32(W1.Substring(2, 2));
            int ST2 = Convert.ToInt32(W2.Substring(0, 2));
            int CR2 = Convert.ToInt32(W2.Substring(2, 2));
            int STDistance, CRMin, CRMax, CRMod;

            STDistance = Math.Abs(ST1 - ST2);

            int CROffset = (STDistance / 2);

            int mod1 = ST1 % 2;
            int mod2 = ST2 % 2;
            if (mod1 == 1 & mod2 == 0)
                CROffset++;

            CRMin = CR1 - CROffset;
            CRMax = CRMin + STDistance;

            CRMod = 0;
            if (CR2 < CRMin)
                CRMod = CRMin - CR2;
            if (CR2 > CRMax)
                CRMod = CR2 - CRMax;

            STDistance += CRMod;

            return STDistance;
        }
    }
}
