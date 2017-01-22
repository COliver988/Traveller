using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Traveller3.Models
{
    public class World
    {
        public World(string sec, Support.Versions version)
        {
            try
            {
                // establish UWP parameters
                UWP = sec.Substring(26, 9);
                SEC = sec;
                this.name = sec.Substring(5, 20);
                this.starport = UWP[0];
                this.size = Support.HexToInt(UWP[1]);
                this.atmo = Support.HexToInt(UWP[2]);
                this.hydro = Support.HexToInt(UWP[3]);
                this.pop = Support.HexToInt(UWP[4]);
                this.gov = Support.HexToInt(UWP[5]);
                this.law = Support.HexToInt(UWP[6]);
                this.tech = Support.HexToInt(UWP[8]);

                this.hex = sec.Substring(0, 4);
                this.bases = sec.Substring(105, 2);
                int i = 0;
                if (int.TryParse(sec.Substring(79, 2), out i)) Importance = i;
                this.travelclass = sec.Substring(108, 1);
                if (travelclass == "-") travelclass = "G";
                allegience = sec.Substring(117, 4);
                calcWTN();

            }
            catch (Exception ex)
            {
                string hi = ex.Message;
            }
        }

        private void calcWTN()
        {
            // 1st, unmodified WTN = pop digit / 2 + tech mod
            wtn = this.pop / 2;
            int TL = this.tech;                 // tech level

            // find the tech level for this via TLModifier file
            int level = -1;
            decimal tmod = 0;

            // read through the file, ignore white space & leading #
            // increment a counter for each valid line, since each line
            // matches a tech level (0-based)
            foreach (var line in ((App)Application.Current).support.TLModifier)
            {
                if (line.Length > 0)
                {
                    if (line.Substring(0, 1) != "#")
                    {
                        if (++level == TL)  // at our tech level
                        {
                            try
                            {
                                tmod = Convert.ToDecimal(line);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            wtn += (float)tmod;

            // and now for the port modifier
            double portmod = 0;

            try
            {
                // read through the file, ignore white space & leading #
                foreach (string line in ((App)Application.Current).support.PortModifier)
                    if (line.Length > 0)
                        if (line.Substring(0, 1) != "#")
                        {
                            string[] dMods = line.Split(new char[] { ',' });
                            decimal mod = Convert.ToDecimal(dMods[0]);
                            if (wtn < (float)mod)
                            {
                                switch (this.starport)
                                {
                                    case 'A':
                                        portmod = Convert.ToDouble(dMods[1]);
                                        break;
                                    case 'B':
                                        portmod = Convert.ToDouble(dMods[2]);
                                        break;
                                    case 'C':
                                        portmod = Convert.ToDouble(dMods[3]);
                                        break;
                                    case 'D':
                                        portmod = Convert.ToDouble(dMods[4]);
                                        break;
                                    case 'E':
                                        portmod = Convert.ToDouble(dMods[5]);
                                        break;
                                    default:
                                        portmod = Convert.ToDouble(dMods[6]);
                                        break;
                                }
                            }
                        }
            }
            catch { }

            if (wtn < 0)
                wtn = 0;
        }


        public string Sector { get; set; }           // Sector map (SEC file)
        public string SubSector { get; set; }       // subsector name
        public string UWP { get; set; }
        public string name { get; set; }           // name of system
        public string hex { get; set; }             // map position
        public char starport { get; set; }          // star port
        public int size { get; set; }               // size (decimal)
        public int atmo { get; set; }
        public int hydro { get; set; }
        public int pop { get; set; }
        public int gov { get; set; }
        public int law { get; set; }
        public int tech { get; set; }
        public string travelclass { get; set; }     // travel class: red, amber, green
        public string bases { get; set; }           // bases
        public string alliance { get; set; }        // political alliance
        public int gasGiant { get; set; }           // gas giant? count
        public string stellar { get; set; }         // stellar info
        public ArrayList tradeclass { get; set; }    // trade classifications (1 per line, uses structure stTrade)
        public int popModifier { get; set; }        // population modifier
        public int belts { get; set; }              // asteroid belts
        public string secstring { get; set; }       // saved off SEC string
        public string berka { get; set; }           // Berka-style planetary descriptions
        private float wtn;
        public float WTN { get { return wtn; } }
        public string SEC { get; }
        public string ShortName { get { return string.Format("{0} {1}", hex, name); } }
        public int Importance { get; set; }
        private string allegience;
        public string Allegience
        {
            get
            {
                if (((App)Application.Current).support.Allegiances == null) return allegience;
                if (((App)Application.Current).support.Allegiances.ContainsKey(allegience)) return ((App)Application.Current).support.Allegiances[allegience];
                return "unknown";
            }
        }
        public string Notes { get; set; }

        public List<string> misc;      // miscellaneous info, depending on version
        public List<string> images;    // any attached images

        public string errmsg;          // errors

        // structure for trade classification
        public struct stTrade
        {
            public string code;     // 2 character code
            public string desc;     // description
            public string size;     // size range
            public string atm;      // atmosphere ranges
            public string hyd;      // hydrosphere
            public string pop;      // population
            public string gov;      // government
            public string law;      // law level
            public string TL;       // tech level
            public int buymod;      // purchase price mod (based on trade codes)
        }
    }
}
