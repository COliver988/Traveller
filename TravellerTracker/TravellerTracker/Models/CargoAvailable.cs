﻿using System.Collections.Generic;
using Traveller.Support;

namespace Traveller.Models
{
    public class CargoAvailable
    {
        public World world { get; set; }
        public int HighPassage { get; set; }
        public int MidPassage { get; set; }
        public int LowPassage { get; set; }
        public List<int> CargoMajor { get; set; }
        public List<int> CargoMinor { get; set; }
        public List<int> CargoIncidental { get; set; }

        public string CargoListing { get { return $"Major: {CargoMajor.Count} Minor: {CargoMinor.Count} Incidental: {CargoIncidental.Count}"; } }


        public CargoAvailable(Ship ship, World destination)
        {
            World origin = ship.theWorld;
            Utilities util = new Utilities();
            world = destination;
            int paxDM = 0;
            if (destination.Pop <= 4) paxDM = -3;
            if (destination.Pop >= 8) paxDM = 3;
            paxDM += origin.Tech - destination.Tech;
            int cargoDM = 0;
            if (destination.Pop <= 4) cargoDM = -4;
            if (destination.Pop >= 8) cargoDM = 1;
            cargoDM += origin.Tech - destination.Tech;
            int major = 0;
            int minor = 0;
            int incidental = 0;

            CargoMajor = new List<int>();
            CargoMinor = new List<int>();
            CargoIncidental = new List<int>();

            switch (origin.Pop)
            {
                case 0:
                    HighPassage = 0;
                    MidPassage = 0;
                    LowPassage = 0;
                    major = 0;
                    minor = 0;
                    incidental = 0;
                    break;
                case 1:
                    HighPassage = 0;
                    MidPassage = util.d6() - 2;
                    LowPassage = util.d6(2) - 6;
                    major = util.d6() - 4;
                    minor = util.d6() - 4;
                    incidental = 0;
                    break;
                case 2:
                    HighPassage = util.d6() - util.d6();
                    MidPassage = util.d6();
                    LowPassage = util.d6(2);
                    major = util.d6() - 2;
                    minor = util.d6() - 1;
                    incidental = 0;
                    break;
                case 3:
                    HighPassage = util.d6(2) - util.d6(2);
                    MidPassage = util.d6(2) - util.d6();
                    LowPassage = util.d6(2);
                    major = util.d6() - 1;
                    minor = util.d6();
                    incidental = 0;
                    break;
                case 4:
                    HighPassage = util.d6(2) - util.d6(1);
                    MidPassage = util.d6(2) - util.d6();
                    LowPassage = util.d6(3) - util.d6();
                    major = util.d6();
                    minor = util.d6() + 1;
                    incidental = 0;
                    break;
                case 5:
                    HighPassage = util.d6(2) - util.d6(1);
                    MidPassage = util.d6(2) - util.d6(2);
                    LowPassage = util.d6(3) - util.d6();
                    major = util.d6() + 1;
                    minor = util.d6() + 2;
                    incidental = 0;
                    break;
                case 6:
                    HighPassage = util.d6(3) - util.d6(2);
                    MidPassage = util.d6(3) - util.d6(2);
                    LowPassage = util.d6(3);
                    major = util.d6() + 2;
                    minor = util.d6() + 3;
                    incidental = util.d6() - 3;
                    break;
                case 7:
                    HighPassage = util.d6(3) - util.d6(2);
                    MidPassage = util.d6(3) - util.d6(1);
                    LowPassage = util.d6(3);
                    major = util.d6() + 3;
                    minor = util.d6() + 4;
                    incidental = util.d6() - 3;
                    break;
                case 8:
                    HighPassage = util.d6(3) - util.d6(1);
                    MidPassage = util.d6(3) - util.d6(1);
                    LowPassage = util.d6(4);
                    major = util.d6() + 4;
                    minor = util.d6() + 5;
                    incidental = util.d6() - 2;
                    break;
                case 9:
                    HighPassage = util.d6(3) - util.d6(1);
                    MidPassage = util.d6(3);
                    LowPassage = util.d6(5);
                    major = util.d6() + 5;
                    minor = util.d6() + 6;
                    incidental = util.d6() - 2;
                    break;
                case 10:
                    HighPassage = util.d6(3);
                    MidPassage = util.d6(4);
                    LowPassage = util.d6(6);
                    major = util.d6() + 6;
                    minor = util.d6() + 7;
                    incidental = util.d6();
                    break;
                default:
                    break;
            }

            HighPassage += paxDM;
            if (HighPassage < 0) HighPassage = 0;
            MidPassage += paxDM;
            if (MidPassage < 0) MidPassage = 0;
            LowPassage += paxDM;
            if (LowPassage < 0) LowPassage = 0;

            major += cargoDM;
            if (major < 0) major = 0;
            minor += cargoDM;
            if (minor < 0) minor = 0;
            incidental += cargoDM;
            if (incidental < 0) incidental = 0;

            if (major > 0)
                for (int i = 0; i < major; i++)
                    CargoMajor.Add(util.d6() * 10);
            if (minor > 0)
                for (int i = 0; i < minor; i++)
                    CargoMinor.Add(util.d6() * 5);
            if (incidental > 0)
                for (int i = 0; i < incidental; i++)
                    CargoIncidental.Add(util.d6());
        }
    }
}