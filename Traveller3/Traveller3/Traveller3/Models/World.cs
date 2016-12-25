using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Traveller3.Models
{
    public class World
    {
        public string Sector;           // Sector map (SEC file)
        public string SubSector;        // subsector name
        public string name;            // name of system
        public string hex;             // map position
        public char starport;          // star port
        public int size;               // size (decimal)
        public int atmo;
        public int hydro;
        public int pop;
        public int gov;
        public int law;
        public int tech;
        public string travelclass;     // travel class: red, amber, green
        public string bases;           // bases
        public string alliance;        // political alliance
        public int gasGiant;           // gas giant? count
        public string stellar;         // stellar info
        public ArrayList tradeclass;    // trade classifications (1 per line, uses structure stTrade)
        public int popModifier;        // population modifier
        public int belts;              // asteroid belts
        public string secstring;       // saved off SEC string
        public string berka;           // Berka-style planetary descriptions

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

        private string[] stAtmosphere;
        private string[] stGovernment;
        private string[] stAllianceCodes;
        private string systemNotes;
        private string npcNotes;

        public Regex worldRegex = new Regex(@"^" +
            @"( \s*             (?<name>        [^\s.](.*?[^\+\s.])?  ) )? \+?\.* " +    // Name
            @"( \s*             (?<hex>         \d\d\d\d              ) )      " +    // Hex
            @"( \s+             (?<uwp>         \w{7}-\w              ) )      " +    // UWP (Universal World Profile)
            @"( \s+             (?<base>        \w | \*               ) )?     " +    // Base
            @"( \s{1,3}         (?<codes>       .{10,}?               ) )      " +    // Codes
            @"( \s+             (?<zone>        \w                    ) )?     " +    // Zone
            @"( \s+             (?<pbg>         \d[0-9A-F][0-9A-F]    ) )      " +    // PGB (Population multiplier, Belts, Gas giants)
            @"( \s+  (\w\w\/)?  (?<allegiance>  (\w\w\b|\w-|--)       ) )      " +    // Allegiance
            @"( \s*             (?<stellar>     .*                    ) )      "        // Stellar data (etc)
            , RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

    }
}
