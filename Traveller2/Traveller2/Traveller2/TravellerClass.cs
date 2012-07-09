using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Data;
using CPrintReportStringDemo;

namespace Traveller
{
    public class World
    {
        private string name;            // name of system
        private string hex;             // map position
        private char starport;          // star port
        private int size;               // size (decimal)
        private int atmo;
        private int hydro;
        private int pop;
        private int gov;
        private int law;
        private int tech;
        private string travelclass;     // travel class: red, amber, green
        private string bases;           // bases
        private string alliance;        // political alliance
        private int gasGiant;           // gas giant? count
        private string stellar;         // stellar info
        private ArrayList tradeclass;    // trade classifications (1 per line, uses structure stTrade)
        private int popModifier;        // population modifier
        private int belts;              // asteroid belts
        private string secstring;       // saved off SEC string
        private string berka;           // Berka-style planetary descriptions

        private List<string> misc;      // miscellaneous info, depending on version
        private List<string> images;    // any attached images

        private string errmsg;          // errors

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

        /// <summary>
        /// generic world object for utilites
        /// </summary>
        public World()
        {
            clearData();
        }

        /// <summary>
        /// pass along an SEC line & version to create a world
        /// </summary>
        /// <param name="sec">sec - string, a line from an SEC file</param>
        /// <param name="tradetable">version - string, version (T5, CT, MT, CU)</param>
        public World(string sec, string version)
        {
            clearData();

            this.secstring = sec;

            try
            {
                Match worldMatch = worldRegex.Match(sec);

                // establish UWP parameters
                string UWP = worldMatch.Groups["uwp"].Value;
                this.name = worldMatch.Groups["name"].Value;
                this.starport = UWP[0];
                this.size = int.Parse(UWP[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.atmo = int.Parse(UWP[2].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.hydro = int.Parse(UWP[3].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.pop = int.Parse(UWP[4].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.gov = int.Parse(UWP[5].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.law = int.Parse(UWP[6].ToString(), System.Globalization.NumberStyles.HexNumber);
                this.tech = int.Parse(UWP[8].ToString(), System.Globalization.NumberStyles.HexNumber);

                this.hex = worldMatch.Groups["hex"].Value;
                this.bases = worldMatch.Groups["base"].Value;
                this.travelclass = worldMatch.Groups["zone"].Value;
                switch (this.travelclass)
                {
                    case "A":
                    case "a":
                        this.travelclass = "Amber";
                        break;
                    case "R":
                    case "r":
                        this.travelclass = "Red";
                        break;
                    default:
                        this.travelclass = "Green";
                        break;
                }
                this.alliance = worldMatch.Groups["allegiance"].Value;

                string PBG = worldMatch.Groups["pbg"].Value;
                int.TryParse(PBG[0].ToString(), out this.popModifier);
                int.TryParse(PBG[1].ToString(), out this.belts);
                int.TryParse(PBG[2].ToString(), out this.gasGiant);

                this.stellar = worldMatch.Groups["stellar"].Value;
                this.bases = worldMatch.Groups["base"].Value;

                this.misc = new List<string>(); // empty set of misc data
                setTradeClasses(version);       // set up the trade classifications
                loadSupport();                  // load support data files
                loadNotes(version);             // load any notes

                switch (version)
                {
                    case "T5":
                        T5Extensions();                 // load up any T5 extensions
                        break;
                    default:
                        break;
                }

                this.images = new List<string>();   // empty set of images
                loadImages();                   // and load up any images

                calcWTN();                      // get the GURPS World Trade Number
                this.berka = BerkaSystem();

                // special trade class from SEC record
                string tCodes = worldMatch.Groups["codes"].Value;
                if (tCodes.Contains("Cp"))
                {
                    stTrade tc = new stTrade();
                    tc.desc = "Subsector Capital";
                    tc.code = "Cp";
                    this.tradeclass.Add(tc);
                }
                if (tCodes.Contains("Cs"))
                {
                    stTrade tc = new stTrade();
                    tc.desc = "Sector Capital";
                    tc.code = "Cs";
                    this.tradeclass.Add(tc);
                }
                if (tCodes.Contains("Ab"))
                {
                    stTrade tc = new stTrade();
                    tc.desc = "Data Repository";
                    tc.code = "Ab";
                    this.tradeclass.Add(tc);
                }
            }
            catch (Exception ex) { this.errmsg = ex.Message; }
        }

        /// <summary>
        /// clear data members
        /// </summary>
        private void clearData()
        {
            this.name = "";
            this.hex = "";
            this.starport = ' ';
            this.size = -1;
            this.atmo = -1;
            this.hydro = -1;
            this.pop = -1;
            this.gov = -1;
            this.law = -1;
            this.tech = -1;
            this.travelclass = null;
            this.bases = null;
            this.alliance = "";
            this.gasGiant = -1;
            this.tradeclass = null;
            this.stellar = "";
            this.popModifier = -1;
            this.belts = -1;
            this.misc = null;
            this.berka = "";

            this.systemNotes = "";
            this.npcNotes = "";

            this.errmsg = "";
        }

        /// <summary>
        /// load up support descriptions files
        /// </summary>
        private void loadSupport()
        {
            try
            {
                stAtmosphere = File.ReadAllLines("Atmospheres.txt");
            }
            catch (Exception ex) { this.errmsg = ex.Message; }
            try
            {
                stGovernment = File.ReadAllLines("Governments.txt");
            }
            catch (Exception ex) { this.errmsg = ex.Message; }
            try
            {
                stAllianceCodes = File.ReadAllLines("AllianceCodes.txt");
            }
            catch (Exception ex) { this.errmsg = ex.Message; }
        }


        /// <summary>
        /// load any notes for this system from the Notes.xml file
        /// also loads up any extensions
        /// </summary>
        private void loadNotes(string version)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("Notes.xml");
                XElement sn = xmlDoc.Root.Elements("system").Where(r => (string)r.Attribute("sec") == this.secstring).FirstOrDefault();
                if (sn != null)
                {
                    this.systemNotes = sn.Element("note").Value;
                    this.npcNotes = sn.Element("npcnote").Value;

                    foreach (XElement extElem in sn.Elements("extensions"))
                    {
                        if (extElem.Attribute("version").Value == version)
                        {
                            foreach (XElement ex in extElem.Elements())
                            {
                                this.misc.Add(ex.Value);                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }
        }

        /// <summary>
        /// load up any images in the notes.xml file
        /// </summary>
        /// <remarks>uses notes/system/images node</remarks>
        private void loadImages()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("Notes.xml");
                XElement sn = xmlDoc.Root.Elements("system").Where(r => (string)r.Attribute("sec") == this.secstring).FirstOrDefault();
                if (sn != null)
                {
                    XElement imageNode = sn.Element("images");
                    if (imageNode != null)
                    {
                        foreach (XElement imgElem in imageNode.Elements())
                        {
                            this.images.Add(imgElem.Attribute("desc").Value + "|" + 
                                imgElem.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }
        }

        /// <summary>
        /// save off any version extensions
        /// </summary>
        private void saveExtensions(string version)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("Notes.xml");

                XElement sn = xmlDoc.Root.Elements("system").Where(r => (string)r.Attribute("sec") == this.secstring).FirstOrDefault();
                if (sn == null)
                {
                    List<XElement> exts = new List<XElement>();
                    foreach (string extension in this.misc)
                    {
                        XElement x = new XElement("ex", extension);
                        exts.Add(x);
                    }
                    xmlDoc.Root.Add(new XElement(
                            new XElement("system",
                                new XAttribute("sec", this.secstring),
                                new XElement("npcnote", ""),
                                new XElement("note", ""),
                                new XElement("extensions",
                                    new XAttribute("version", version),
                                        exts))));
                }
                xmlDoc.Save("Notes.xml");
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }
        }

        /// <summary>
        /// get the Berka planetary description (11 world types)
        /// </summary>
        public string BerkaDesc
        {
            get { return this.berka; }
        }

        #region public method access

        /// <summary>
        /// save off any notes
        /// </summary>
        /// <param name="notes">string - notes</param>
        /// <param name="npcNotes">string - npc notes</param>
        public void saveNotes(string note, string npc)
        {
            if (this.hex == "" | this.name == "")
                return;             // nothing to do

            if (note.Length == 0 & npc.Length == 0)
                return;             // still nothing

            try
            {
                XDocument xmlDoc = XDocument.Load("Notes.xml");

                XElement sn = xmlDoc.Root.Elements("system").Where(r => (string)r.Attribute("sec") == this.secstring).FirstOrDefault();
                if (sn != null)
                {
                    sn.SetElementValue("note", note);
                    sn.SetElementValue("npcnote", npc);
                }
                else
                {
                    xmlDoc.Root.Add(
                            new XElement("system",
                                new XAttribute("sec", this.secstring),
                                new XElement("npcnote", npc),
                                new XElement("note", note),
                                new XElement("extensions", "")));
                }
                xmlDoc.Save("Notes.xml");
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }
        }

        /// <summary>
        /// add a new image to the sysem image node
        /// </summary>
        /// <param name="image">string - name of the image</param>
        /// <param name="desc">string - description for the menu</param>
        /// <remarks>adds the new image to the notes for this systek; adds the images node if necessary</remarks>
        public void saveImage(string image, string desc)
        {
            XDocument xmlDoc = XDocument.Load("Notes.xml");

            XElement sn = xmlDoc.Root.Elements("system").Where(r => (string)r.Attribute("sec") == this.secstring).FirstOrDefault();
            if (sn != null)
            {
                XElement imageNode = sn.Element("images");
                if (imageNode == null)
                {
                    sn.Add(
                        new XElement("images",
                            new XElement("img", image,
                                new XAttribute("desc", desc))));
                }
                else
                {
                    imageNode.Add(
                            new XElement("img", image,
                                new XAttribute("desc", desc)));
                }
                xmlDoc.Save("Notes.xml");
            }
        }

        #region public data access

        TravUtils util = new TravUtils();

        public string Name 
        {
            get { return this.name;}
        }

        public string Hex
        {
            get { return this.hex; }
        }

        public char Starport
        {
            get { return this.starport; }
        }

        public char Size
        {
            get { return util.convertToHex(this.size); }
        }

        public char Atmosphere
        {
            get { return util.convertToHex(this.atmo); }
        }

        public char Hydrographics
        {
            get { return util.convertToHex(this.hydro); }
        }

        public char Population
        {
            get { return util.convertToHex(this.pop); }
        }

        public char Government
        {
            get { return util.convertToHex(this.gov); }
        }

        public char LawLevel
        {
            get { return util.convertToHex(this.law); }
        }

        public char TechLevel
        {
            get { return util.convertToHex(this.tech); }
        }

        public int PopulationMod
        {
            get { return this.popModifier; }
        }

        public int Belts
        {
            get { return this.belts; }
        }

        /// <summary>
        /// return the UWP in XXXXXXX-X format
        /// </summary>
        public string UWP
        {
            get {
                return String.Format("{0}{1}{2}{3}{4}{5}{6}-{7}",
                this.starport,
                util.convertToHex(this.size),
                util.convertToHex(this.atmo),
                util.convertToHex(this.hydro),
                util.convertToHex(this.pop),
                util.convertToHex(this.gov),
                util.convertToHex(this.law),
                util.convertToHex(this.tech));
            }
        }

        public int GasGiant
        {
            get { return this.gasGiant; }
        }

        public ArrayList TradeClass
        {
            get { return this.tradeclass; }
        }

        public string TravelCode
        {
            get { return this.travelclass; }
        }

        public string Alliance
        {
            get { return this.alliance; }
        }

        public string Bases
        {
            get { return this.bases; }
        }

        public string Stellar
        {
            get { return this.stellar; }
        }

        public string Notes
        {
            get { return this.systemNotes; }
        }

        public string NPCNotes
        {
            get { return this.npcNotes; }
        }

        public string SEC
        {
            get { return this.secstring; }
        }

        // expanded descriptions

        public string descSize
        {
            get
            {
                return this.size * 1000 + " miles in diameter";
            }
        }

        public string descAtmo
        {
            get
            {
                if (stAtmosphere.Length > this.atmo)
                {
                    return stAtmosphere[this.atmo];
                }
                else
                {
                    return "atmosphere not defined: " + this.atmo.ToString();
                }           
            }
        }

        public string descHydro
        {
            get
            {
                string retValue;
                switch (this.hydro)
                {
                    case 0:
                        retValue = "desert world";
                        break;
                    case 10:
                        retValue = "water world";
                        break;
                    default:
                        retValue = this.hydro * 10 + "% water";
                        break;
                }
                return retValue;
            }
        }

        public string descGov
        {
            get
            {
                if (stGovernment.Length > this.gov)
                {
                    return stGovernment[this.gov];
                }
                else
                {
                    return "government not defined: " + this.gov.ToString();
                }
            }
        }

        public string descPop
        {
            get
            {
                int popvalue = this.popModifier;
                for (int i = 0; i < this.pop; i++)
                {
                    popvalue = popvalue * 10;
                }
                return "approximately " + popvalue.ToString("N0");
            }
        }

        public string descAlliance
        {
            get
            {
                string retValue = "unknown";
                foreach (string acode in stAllianceCodes)
                {
                    if (acode.StartsWith(this.alliance))
                    {
                        retValue = acode.Substring(3);
                    }
                }
                return retValue;
            }
        }

        public List<string> Misc
        {
            get { return this.misc; }
        }

        public string ErrMsg
        {
            get { return this.errmsg; }
        }

        public List<string> Images
        {
            get { return this.images; }
        }

        #endregion

        #endregion

        #region internal utilities

        /// <summary>
        /// set the GURPs world trade number
        /// </summary>
        /// <param name="world">UWP structure</param>
        /// <returns>double - world trade number</returns>
        /// <remarks>I know, it is GURPs, but it helps to set traffic density
        /// WTN = pop digit / 2 + tech mod + port mod
        /// </remarks>
        private void calcWTN()
        {
            // 1st, unmodified WTN = pop digit / 2 + tech mod
            decimal WTN = this.pop / 2;
            int TL = this.tech;                 // tech level

            // find the tech level for this via TLModifier file
            int level = -1;
            decimal tmod = 0;

            // read through the file, ignore white space & leading #
            // increment a counter for each valid line, since each line
            // matches a tech level (0-based)
            StreamReader rd = new StreamReader("TLModifier.txt");
            string line = "";
            while ((line = rd.ReadLine()) != null)
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
            WTN += (decimal)tmod;

            // and now for the port modifier
            double portmod = 0;

            try
            {
                // read through the file, ignore white space & leading #
                rd = new StreamReader("PortModifier.txt");
                while ((line = rd.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        if (line.Substring(0, 1) != "#")
                        {
                            string[] dMods = line.Split(new char[] { ',' });
                            decimal mod = Convert.ToDecimal(dMods[0]);
                            if (WTN < mod)
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
                }
            }
            catch { }

            if (WTN < 0)
                WTN = 0;

            this.misc.Add("WTN           : " + WTN.ToString());
        }

        // get the T5 extensions
        // if already loaded via loadnotes, then nevermind
        private void T5Extensions()
        {
            if (this.misc.Count > 0)
                return;                 // already existing!

            int importance = 0;
            if (this.starport == 'A' | this.starport == 'B')
                importance++;
            if (this.starport == 'D' | this.starport == 'E' | this.starport == 'X')
                importance--;
            if (this.tech >= 10)
                importance++;
            if (this.tech <= 7)
                importance--;


            // nobility
            string nobility = "";

            foreach (stTrade tc in this.tradeclass)
            {
                switch (tc.code)
                {
                    case "Ag":
                        importance++;
                        nobility = "Nobility      : Baron";
                        break;
                    case "Hi":
                        importance++;
                        nobility = "Nobility      : Count";
                        break;
                    case "In":
                        importance++;
                        nobility = "Nobility      : Duke";
                        break;
                    case "Ph":
                        nobility = "Nobility      : Viscount";
                        break;
                    case "Pi":
                        importance++;
                        nobility = "Nobility      : Marquis";
                        break;
                    case "Ri":
                        importance++;
                        nobility = "Nobility      : Baron";
                        break;
                    case "Pr":
                        importance++;
                        nobility = "Nobility      : Baronet";
                        break;
                    case "Pa":
                        importance++;
                        nobility = "Nobility      : Baronet";
                        break;
                    default:
                        break;
                }
            }

            this.misc.Add("Importance    : " + importance.ToString());

            int resources = (util.rollDx(6) + util.rollDx(6)) + this.gasGiant + this.belts;
            this.misc.Add("Resources     : " + resources.ToString());
            this.misc.Add("Labor         : " + this.pop.ToString());
            this.misc.Add("Infrastructure: " + (util.rollDx(6) + util.rollDx(6) + importance).ToString());

            int barriers = (util.rollDx(6) + util.rollDx(6)) - 2;
            this.misc.Add("Barriers      : " + barriers.ToString());

            // resource units; if any of the items are 0, set to '1'
            if (resources <= 0)
                resources = 1;
            if (importance <= 0)
                importance = 1;
            if (barriers == 5)
                barriers = 4;       // so that 5 - barriers = 1
            int RU = resources * this.pop * importance * (5 - barriers);
            this.misc.Add("Resource units: " + RU.ToString());

            if (nobility.Length > 0)
                this.misc.Add(nobility);

            // number of worlds
            int worldCnt = 1 + this.gasGiant + this.belts + util.rollDx(6) + util.rollDx(6);
            this.misc.Add("Worlds: " + worldCnt.ToString());

            // and save them off
            saveExtensions("T5");
        }

        /// <summary>
        /// get the trade classifications
        /// </summary>
        /// <param name="filename">string - filename of trade class file to use</param>
        /// <remarks>loads the trade class</remarks>
        private void setTradeClasses(string prefix)
        {
            this.tradeclass = new ArrayList();              // create a valid array list

            StreamReader rd = new StreamReader(prefix + "TradeCodes.txt");
            string line = "";

            while ((line = rd.ReadLine()) != null)
            {
                if (line.StartsWith("#"))
                    continue;                   // notes
                if (line.StartsWith(" "))
                    continue;                   // comment or white space
                if (line.Length < 12)
                    continue;

                stTrade tc = new stTrade();
                tc.code = line.Substring(0, 2); // code
                string[] ln = line.Split(new char[] { ',' });
                tc.desc = ln[0].Substring(3);
                tc.size = ln[1];
                tc.atm = ln[2];
                tc.hyd = ln[3];
                tc.pop = ln[4];
                tc.gov = ln[5];
                tc.law = ln[6];
                tc.TL = ln[7];
                try
                {
                    tc.buymod = Convert.ToInt32(ln[8]);
                }
                catch
                {
                    tc.buymod = 0;
                }

                // and see if the parameters match
                TravUtils util = new TravUtils();
                if ((tc.size.Contains(util.convertToHex(this.size)) | tc.size == "-") &
                    (tc.atm.Contains(util.convertToHex(this.atmo)) | tc.atm == "-") &
                    (tc.hyd.Contains(util.convertToHex(this.hydro)) | tc.hyd == "-") &
                    (tc.pop.Contains(util.convertToHex(this.pop)) | tc.pop == "-") &
                    (tc.gov.Contains(util.convertToHex(this.gov)) | tc.gov == "-") &
                    (tc.law.Contains(util.convertToHex(this.law)) | tc.law == "-") &
                    (tc.TL.Contains(util.convertToHex(this.tech)) | tc.TL == "-"))
                {
                    this.tradeclass.Add(tc);
                }
            }
        }

        /// <summary>
        /// determine world type onthe Berka descriptions
        /// </summary>
        /// <returns>string - terran, garden, etc</returns>
        private string BerkaSystem()
        {
            string desc = "unknown";

            if (this.hydro >= 10)
                desc = "ocean";
            if (this.atmo >= 2 & this.atmo <= 9)
            {
                if (this.hydro >= 1)
                    desc = "garden";
                else
                    desc = "desert";
            }
            if ((this.atmo == 6 | this.atmo == 8) & this.hydro >= 3)
                desc = "terran";
            if (this.atmo == 10 | this.atmo == 13)
                desc = "exotic";
            if (this.atmo == 11 | this.atmo == 12)
            {
                if (this.hydro >= 1)
                    desc = "exotic";
                else
                    desc = "hothouse";
            }
            if ((this.atmo == 0 | this.atmo == 1) & this.hydro >= 1)
                desc = "icecapped";
            if (this.size >= 1 & this.atmo == 0 & this.hydro == '0')
                desc = "rockball";
            if (this.size == '0')
                desc = "asteroids";

            return desc;
        }

        /// <summary>
        /// see if a string is a valid SEC formatted string
        /// </summary>
        /// <param name="SEC">string - the string to verify</param>
        /// <returns>true if the string is a valid SEC string, false otherwise</returns>
        public bool isValidSEC(string SEC)
        {
            return worldRegex.IsMatch(SEC);
        }

        /// <summary>
        /// get a list of systems within jump range
        /// </summary>
        /// <param name="filename">string - file we are using as our source</param>
        /// <param name="jump">int - jump range</param>
        /// <returns>List - a list of SEC strings in jump range</returns>
        public List<string> jumpRange(string filename, int jump)
        {
            List<string> range = new List<string>();
            string[] systems = File.ReadAllLines(filename);

            // figure the max/min ranges for our hex positions
            // 1st two are horizontal, 2nd pair vertical
            int origH = Convert.ToInt32(this.hex.Substring(0, 2));
            int origV = Convert.ToInt32(this.hex.Substring(2, 2));

            int maxV = origV + jump;        // jump distance down from origin
            int minV = origV - jump;        // jump distance up from origin
            if (minV < 0)
                minV = 0;                   // can't go off the top
            int maxH = origH + jump;        // jump distance to the right
            int minH = origH - jump;        // jump distance to the left
            if (minH < 0)
                minH = 0;                   // can't go off the left side

            Traveller.TravUtils util = new TravUtils();

            // now we have our box - find all systems inside that range
            foreach (string sec in systems)
            {
                if (isValidSEC(sec))
                {
                    Match worldMatch = worldRegex.Match(sec);
                    string hex = worldMatch.Groups["hex"].Value;
                    int h = Convert.ToInt32(hex.Substring(0, 2));
                    int v = Convert.ToInt32(hex.Substring(2, 2));
                    if (h >= minH & h <= maxH & v >= minV & v <= maxV)
                    {
                        int distance = util.calcDistance(hex, this.hex);
                        if (distance <= jump)
                            range.Add(sec);
                    }
                }
            }

            return range;
        }

        #endregion
    }

    #region TravUtils - utility class

    // general purpose utilities
    public class TravUtils
    {
        private string hexString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private Random die = new Random();
        private string errmsg;

        public TravUtils()
        {
            this.errmsg = "";
        }

        public string errMsg
        {
            get { return this.errmsg; }
        }

        /// <summary>
        /// convert a number into a single hex character (Traveller pseudo-hex goes to Z)
        /// </summary>
        /// <param name="i">int - number to convert</param>
        /// <returns>char - hexadecimal value</returns>
        /// <remarks> must be between 0 and 26, else returns '?' </remarks>
        public char convertToHex(int i)
        {
            if (i >= 0 & i < 26)
            {
                return hexString[i];
            }
            else
            {
                return '?';
            }
        }

        /// <summary>
        /// convert to a int value
        /// </summary>
        /// <param name="s">string - hex string to convert</param>
        /// <returns></returns>
        public int HexToInt(string s)
        {
            return int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        /// <summary>
        /// calculate the distance between two hexes
        /// </summary>
        /// <param name="hex1">first hex number; 1234</param>
        /// <param name="hex2">second hex number; 5678</param>
        /// <returns>int - distance in jumps/parsecs (hexes)</returns>
        public int calcDistance(string hex1, string hex2)
        {
            int ST1 = Convert.ToInt32(hex1.Substring(0, 2));
            int CR1 = Convert.ToInt32(hex1.Substring(2, 2));
            int ST2 = Convert.ToInt32(hex2.Substring(0, 2));
            int CR2 = Convert.ToInt32(hex2.Substring(2, 2));
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

        /// <summary>
        /// roll a single die
        /// </summary>
        /// <param name="x">int - sided die (6 = d6, 10 = d10)</param>
        /// <returns>int - roll between 1 and x</returns>
        public int rollDx(int x)
        {
            return (die.Next(x) + 1);
        }

        /// <summary>
        /// roll a flux roll (d6-d6)
        /// </summary>
        /// <returns></returns>
        public int flux()
        {
            return ((die.Next(6) + 1) - (die.Next(6) + 1));
        }

        /// <summary>
        /// load up a CSV file
        /// </summary>
        /// <param name="filename">string - file to load</param>
        /// <remarks>ignores white space, comments (# line)
        /// dynamically creates columns as necessary</remarks>
        /// <returns>datatable of file</returns>
        public DataTable loadTable(string filename)
        {
            DataTable tbl = new DataTable();
            int fieldcnt = 0;
            try
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string  line in lines)
                {
                    if (line.Length == 0)
                        continue;
                    if (line.StartsWith(@"#") | line.StartsWith(" "))
                        continue;
                    string[] fields = line.Split(new char[] { ',' });
                    if (fields.Length > fieldcnt)
                    {
                        for (int i = 0; i < (fields.Length - fieldcnt); i++)
                        {
                            tbl.Columns.Add();
                        }
                        fieldcnt = fields.Length;
                    }
                    DataRow row = tbl.NewRow();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        row[i] = fields[i];
                    }
                    tbl.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
                throw;
            }
            return tbl;
        }

        /// <summary>
        /// load a CSV file up
        /// </summary>
        /// <param name="filename">string - file to load</param>
        /// <returns>string[] - file</returns>
        /// <remarks>ignores white space, comments</remarks>
        public List<string> loadCSV(string filename)
        {
            List<string> retValue = new List<string>();
            try
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    if (line.Length == 0)
                        continue;
                    if (line.StartsWith(@"#") | line.StartsWith(" "))
                        continue;
                    retValue.Add(line);
                }
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }
            return retValue;
        }

        /// <summary>
        /// verify the current version's trade files
        /// </summary>
        /// <returns></returns>
        public List<string> verifyFiles(string version)
        {
            List<string> results = new List<string>();

            switch (version)
            {
                case "MT":
                    results = verifyMT();
                    break;
                default:
                    results.Add("Version is not currently supported: " + version);
                    break;
            }

            return results;
        }

        /// <summary>
        /// verify Mongoose Traveller files
        /// </summary>
        /// <returns>List(string) - verification notes</returns>
        private List<string> verifyMT()
        {
            List<string> results = new List<string>();

            bool codesOK = true;
            bool goodsOK = true;
            bool valuesOK = true;

            results.Add("Testing trade code file    [MTTradeCodes.txt]");
            try
            {
                List<string> codes = loadCSV("MTTradeCodes.txt");
                if (codes.Count != 17)
                    results.Add("  warning --> should have 17 lines; only " + codes.Count.ToString() +
                        " are listed");
                for (int i = 0; i < codes.Count; i++)
                {
                    string[] codeDetail = codes[i].Split(new char[] { ',' });
                    if (codeDetail.Length != 10)
                    {
                        results.Add("  error   --> line " + i.ToString() + " does not have 10 fields");
                        results.Add("              " + codes[i]);
                        codesOK = false;
                    }
                }
                if (codesOK)
                {
                    results.Add("  --> MT Trade Codes table appears ok");
                }
            }
            catch (Exception ex)
            {
                results.Add("  --> error: " + ex.Message);
                codesOK = false;
            }

            results.Add(" ");
            results.Add("Testing trade goods file   [MTGoods.csv]");
            try
            {
                List<string> MTGoods = loadCSV("MTGoods.csv");
                if (MTGoods.Count != 36)
                {
                    results.Add("  warning --> should have 36 lines; only " + MTGoods.Count.ToString() +
                        " are listed");
                }
                for (int i = 0; i < MTGoods.Count; i++)
                {
                    string[] codeDetail = MTGoods[i].Split(new char[] { ',' });
                    if (codeDetail.Length != 7)
                    {
                        results.Add("  error   --> line " + i.ToString() + " does not have 7 fields");
                        results.Add("              " + MTGoods[i]);
                        goodsOK = false;
                    }
                }
                if (goodsOK)
                {
                    results.Add("  --> MT Trade Goods tables appears ok");
                }
            }
            catch (Exception ex2)
            {
                results.Add("  --> error: " + ex2.Message);
                goodsOK = false;
            }

            results.Add(" ");
            results.Add("Testing actual values file [MTValue.csv]");
            try
            {
                List<string> avTable = loadCSV("MTValue.csv");
                if (avTable.Count != 23)
                {
                    results.Add("  error --> should have 23 lines; only " + avTable.Count.ToString() +
                        " are listed");
                    valuesOK = false;
                }
                for (int i = 0; i < avTable.Count; i++)
                {
                    string[] line = avTable[i].Split(new char[] { ',' });
                    if (line.Length != 3)
                    {
                        results.Add("  error --> line " + i.ToString() + " does not have 3 values");
                        results.Add("            " + avTable[i]);
                        valuesOK = false;
                    }
                    else
                    {
                        int dRoll = 0;
                        int.TryParse(line[0], out dRoll);
                        if (dRoll != (i - 1))
                        {
                            string error = String.Format("  error --> line {0} should have the die roll = {1} not {2}",
                                i, i - 1, line[0]);
                            results.Add(error);
                            results.Add("            " + avTable[i]);
                            valuesOK = false;
                        }
                    }
                }

                if (valuesOK)
                {
                    results.Add("  --> actual value tables appear correct");
                } 
            }
            catch (Exception ex3)
            {
                results.Add("  --> error: " + ex3.Message);
                valuesOK = false;
            }

            results.Add(" ");
            if (codesOK & goodsOK & valuesOK)
            {
                results.Add("all files pass initial inspection");
            }
            else
            {
                results.Add("errors in one or more files!");
            }

            return results;
        }

    }

    #endregion

    #region Starship - ship class

    /// <summary>
    /// Ship Class
    /// </summary>
    public class Starship
    {
        private string name;        // ship name
        private int man;            // maneuver rating
        private int jump;           // jump rating
        private int power;          // power rating
        private int cargo;          // total cargo tonnage available (dTon)
        private int cargoHeld;      // current cargo held (dTon)
        private int monthly;        // monthly costs (maintenance, mortgage)
        private int jumpCost;       // cost per jump (life support, fuel)
        private string lastPaid;    // last monthly payment date
        private string filename;    // file that contains the current data
        private List<cargoDesc> manifest;
        private List<cargoDesc> sold;   // sold items
        private string version;     // version of Traveller
        private int credits;        // current credits
        private int day;            // imperial day
        private int year;           // imperial year
        private string sec;         // current world system
        private List<travelDesc> log;
        private string secfile;     // SEC file
        private string sectorname;  // sector name (for TravellerMap jump map)
        private int cargoid;        // sequential numbering for unique cargo ID
        private int tradedm;        // buy/sell trade DM
        private bool illegals;      // do we allow illegals? true = ok, no = re-roll 61..65 (Mongoose)

        private string errMsg;       // in case of error

        // cargo description structure
        // trying to encompass all versions, so some things may not be needed
        //   across versions
        public struct cargoDesc
        {
            public string desc;         // item description
            public string origCode;     // origination code
            public int purchasePrice;   // how much was paid for
            public string purchaseDate; // when it was purchased
            public int sellingPrice;    // sold at price
            public string sellingDate;  // when it was sold
            public string notes;        // any notes
            public int dtons;           // how many tons (dTons)
            public int basecostbuy;     // actual cost pre-mods per ton buying
            public int basecostsell;    // selling base cost
            public string origSEC;      // where it was purchased - SEC format
            public string destSEC;      // where it was sold - SEC format
            public double avBuy;        // actual value percentage (1.1 = 110%) when buying
            public double avSell;       // actual value percentage when selling
            public int cargoID;         // unique cargo code ID for retrieval
        }

        // travelogue note structure; may have multiple notes for the same place/date
        public struct travelDesc
        {
            public string date;
            public string system;
            public List<string> notes;
        }

        /// <summary>
        /// create/open a new ship file
        /// </summary>
        /// <param name="file">string - name of the ship file</param>
        public Starship(string file)
        {
            clearData();
            this.filename = file;
            loadData(file);
        }

        /// <summary>
        /// create a new ship's data file
        /// </summary>
        /// <param name="shipname"></param>
        /// <param name="man"></param>
        /// <param name="power"></param>
        /// <param name="jump"></param>
        /// <param name="power"></param>
        /// <param name="cargo">int - cargo capacity in Dtons</param>
        /// <param name="credits">int - current credits</param>
        /// <param name="day">int - day (0..365)</param>
        /// <param name="jumpcost">int - cost per jump (fuel, life support, etc)</param>
        /// <param name="monthly">int - monthly costs (mortgage, maintenance, etc)</param>
        /// <param name="sec">string - SEC string of initial system</param>
        /// <param name="version">string - version: CT, MT, T5, CU</param>
        /// <param name="year">int - imperial year (i.e., 1105)</param>
        /// <param name="secfile">string - SEC format file</param>
        public Starship(string shipname, int man, int power, int jump, int cargo, int monthly, int jumpcost, int day, int year, 
            int credits, string version, string secfile, string sec, string sectorname, int tradeDM, bool illegals)
        {
            XDocument ns = new XDocument();
            XDeclaration dec = new XDeclaration("1.0", "utf-8", "yes");
            ns.AddAnnotation(dec);

            XElement rootNode = new XElement("ShipData");
            ns.Add(rootNode);

            ns.Element("ShipData").Add(
                new XElement("system",
                    new XAttribute("version", version),
                    new XElement("day", day.ToString()),
                    new XElement("year", year.ToString()),
                    new XElement("sec", sec),
                    new XElement("secfile", secfile),
                    new XElement("sectorName", sectorname),
                    new XElement("cargoID",0),
                    new XElement("tradeDM", tradeDM),
                    new XElement("illegals", illegals)));

            ns.Element("ShipData").Add(
                new XElement("Ship"));

            ns.Element("ShipData").Element("Ship").Add(
                    new XElement("Name", shipname),
                    new XElement("Manuever", man.ToString()),
                    new XElement("Power", power.ToString()),
                    new XElement("Jump", jump.ToString()),
                    new XElement("Cargo", cargo.ToString()),
                    new XElement("CargoHeld", "0"),
                    new XElement("credits", credits),
                    new XElement("costs",
                        new XElement("Monthly", monthly.ToString()),
                        new XElement("perJump", jumpcost.ToString()),
                        new XElement("lastPaid", String.Format("{0:000}-{1:0000}", day, year))));

            ns.Element("ShipData").Add(
                new XElement("Cargo"));
            ns.Element("ShipData").Add(
                new XElement("Travelogue"));

            ns.Save(shipname + ".xml");

            clearData();
        }

        /// <summary>
        /// load up the actual ship data
        /// </summary>
        /// <param name="filename">string - filename</param>
        private void loadData(string filename)
        {
            if (!filename.EndsWith(".xml"))
            {
                filename = filename + ".xml";
            }

            XDocument shipdoc = XDocument.Load(filename);
            this.filename = filename;

            try
            {
                XElement shipNode = shipdoc.Element("ShipData").Element("Ship");
                XElement nameNode = shipNode.Element("Name");
                this.name = nameNode.Value;
                XElement powNode = shipNode.Element("Power");
                this.power = Convert.ToInt32(powNode.Value);
                XElement manNode = shipNode.Element("Manuever");
                this.man = Convert.ToInt32(manNode.Value);
                XElement jumpNode = shipNode.Element("Jump");
                this.jump = Convert.ToInt32(jumpNode.Value);
                XElement cargoNode = shipNode.Element("Cargo");
                this.cargo = Convert.ToInt32(cargoNode.Value);
                XElement creditNode = shipNode.Element("credits");
                this.credits = Convert.ToInt32(creditNode.Value);
                XElement chNode = shipNode.Element("CargoHeld");
                this.cargoHeld = Convert.ToInt32(chNode.Value);
                XElement costsNode = shipNode.Element("costs");
                XElement monthlyNode = costsNode.Element("Monthly");
                this.monthly = Convert.ToInt32(monthlyNode.Value);
                XElement jNode = costsNode.Element("perJump");
                this.jumpCost = Convert.ToInt32(jNode.Value);
                XElement mNode = costsNode.Element("lastPaid");
                this.lastPaid = mNode.Value;

                XElement manifestNode = shipdoc.Element("ShipData").Element("Cargo");
                this.manifest = new List<cargoDesc>();
                this.sold = new List<cargoDesc>();
                try
                {
                    foreach (XElement cargoitem in manifestNode.Elements())
                    {
                        cargoDesc cd = new cargoDesc();
                        int.TryParse(cargoitem.Attribute("cargoID").Value, out cd.cargoID);
                        cd.origCode = cargoitem.Attribute("origCode").Value;
                        cd.dtons = Convert.ToInt32(cargoitem.Attribute("dTons").Value);
                        cd.desc = cargoitem.Element("desc").Value;

                        XElement purchaseNode = cargoitem.Element("purchase");
                        cd.purchasePrice = Convert.ToInt32(purchaseNode.Element("finalprice").Value);
                        cd.origSEC = purchaseNode.Attribute("system").Value;
                        cd.purchaseDate = purchaseNode.Attribute("date").Value;
                        double.TryParse(purchaseNode.Element("avMod").Value, out cd.avBuy);

                        XElement sellNode = cargoitem.Element("selling");
                        cd.sellingDate = sellNode.Attribute("date").Value;
                        int.TryParse(sellNode.Element("finalprice").Value, out cd.sellingPrice);
                        cd.destSEC = sellNode.Attribute("system").Value;
                        if (cargoitem.Attribute("sold").Value == "false")
                        {
                            this.manifest.Add(cd);
                        }
                        else
                        {
                            this.sold.Add(cd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.errMsg = "Error in cargo loading: " + ex.Message;
                }

                XElement logNode = shipdoc.Element("ShipData").Element("Travelogue");
                this.log = new List<travelDesc>();
                try
                {
                    foreach (XElement log in logNode.Elements())
                    {
                        travelDesc td = new travelDesc();
                        td.system = log.Attribute("system").Value;
                        td.date = log.Attribute("date").Value;
                        td.notes = new List<string>();
                        foreach (XElement note in log.Elements())
                        {
                            td.notes.Add(note.Value);
                        }
                        this.log.Add(td);
                    }
                }
                catch (Exception ex)
                {
                    this.errMsg += " Error in travelogue loading: " + ex.Message;
                }

                // system info (version, date, current location (SEC), sec file & last cargo ID
                XElement systemNode = shipdoc.Element("ShipData").Element("system");
                this.version = systemNode.Attribute("version").Value;
                XElement dayNode = systemNode.Element("day");
                this.day = Convert.ToInt32(dayNode.Value);
                XElement yearNode = systemNode.Element("year");
                this.year = Convert.ToInt32(yearNode.Value);
                XElement secNode = systemNode.Element("sec");
                this.sec = secNode.Value;
                XElement secfileNode = systemNode.Element("secfile");
                this.secfile = secfileNode.Value;
                XElement cargoIDNode = systemNode.Element("cargoID");
                int.TryParse(cargoIDNode.Value, out this.cargoid);
                XElement tradeDMNode = systemNode.Element("tradeDM");
                int.TryParse(cargoIDNode.Value, out this.tradedm);
                this.sectorname = systemNode.Element("sectorName").Value;

                // see if there is an illegals allowed flag; if not, add it
                // new feature added 1/5/2010
                try
                {
                    this.illegals = Convert.ToBoolean(systemNode.Element("illegals").Value);
                }
                catch
                {
                    this.illegals = false;
                    systemNode.Add(new XElement("illegals", false));
                    shipdoc.Save(filename);
                }
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
            }
        }

        private void clearData()
        {
            this.name = "";
            this.man = 0;
            this.jump = 0;
            this.power = 0;
            this.cargo = 0;
            this.cargoHeld = 0;
            this.monthly = 0;
            this.jumpCost = 0;
            this.lastPaid = "";
            this.manifest = null;
            this.sold = null;
            this.monthly = 0;
            this.credits = 0;
            this.sec = "";
            this.secfile = "";
            this.cargoid = 0;
            this.errMsg = "";
            this.tradedm = 0;
            this.illegals = false;
        }

        /// <summary>
        /// save off the current ship settings
        /// </summary>
        /// <remarks>saves off system & ship data; travelogue & cargo manifest handled dynamically</remarks>
        public void save()
        {
            XDocument sd = XDocument.Load(this.filename);

            // system data 
            XElement shipNode = sd.Element("ShipData").Element("system");
            shipNode.SetElementValue("day", String.Format("{0:000}", this.day));
            shipNode.SetElementValue("year", String.Format("{0:0000}", this.year));
            shipNode.SetElementValue("sec", this.sec);
            shipNode.SetElementValue("cargoID", this.cargoid);
            if (shipNode.Element("tradeDM") != null)
            {
                shipNode.SetElementValue("tradeDM", this.tradedm);
            }
            else
            {
                shipNode.Add(
                    new XElement("tradeDM", this.tradedm));
            }
            if (shipNode.Element("illegals") != null)
            {
                shipNode.SetElementValue("illegals", this.illegals);
            }
            else
            {
                shipNode.Add(
                    new XElement("illegals", this.illegals));
            }

            // ship data
            // see if the last paid is > 30 days ago, if so, reset & charge 
            bool madeMonthlyPayment = makeMonthly();
            if (madeMonthlyPayment)
            {
                this.credits -= this.monthly;
            }

            shipNode = sd.Element("ShipData").Element("Ship");
            shipNode.SetElementValue("CargoHeld", this.cargoHeld.ToString());
            shipNode.SetElementValue("credits", this.credits.ToString());
            shipNode.SetElementValue("Manuever", this.man.ToString());
            shipNode.SetElementValue("Power", this.power.ToString());
            shipNode.SetElementValue("Jump", this.jump.ToString());
            shipNode.SetElementValue("Cargo", this.cargo.ToString());

            // costs node; lastPaid is only thing that can change here
            shipNode = sd.Element("ShipData").Element("Ship").Element("costs");
            shipNode.SetElementValue("lastPaid", this.lastPaid);
            shipNode.SetElementValue("Monthly", this.monthly.ToString());
            shipNode.SetElementValue("perJump", this.jumpCost.ToString());

            sd.Save(this.filename);

            // and add the note if we've made the monthly payment
            // yes - we *should* do it here as we've already got the file 
            //     open, but as we've also already got a routine to do this
            //     may as well use that
            if (madeMonthlyPayment)
            {
                travelogue(String.Format("Monthly costs (Cr{0}) paid on {1}; remaining balance: Cr{2}",
                    this.monthly, this.lastPaid, this.credits));
            }
        }

        /// <summary>
        /// see if we need to make the monthly payments
        /// </summary>
        /// <returns>true if we need to make the monthly payments</returns>
        /// <remarks>will also update the last paid date & add note</remarks>
        private bool makeMonthly()
        {
            bool makepayment = false;

            string[] paid = this.lastPaid.Split(new char[] { '-', '/' });
            int lastDay = 0;
            int.TryParse(paid[0], out lastDay);
            if (lastDay > 0)
            {
                if (this.day < lastDay)  // end of year wrap
                {
                    if ((this.day + 365) > (lastDay + 30))
                    {
                        makepayment = true;
                    }
                }
                else
                {
                    if (this.day > (lastDay + 30))
                    {
                        makepayment = true;
                    }
                }
            }

            // and if we need to update the payment date
            if (makepayment)
            {
                lastDay += 30;
                if (lastDay > 365)
                {
                    lastDay -= 365;
                }
                this.lastPaid = String.Format("{0:000}-{1:0000}", lastDay, this.year);
            }

            return makepayment;
        }

        #region public data access

        /// <summary>
        /// ship name
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// maneuver rating
        /// </summary>
        public int Man
        {
            get { return this.man; }
            set { this.man = value; }
        }

        /// <summary>
        /// power rating
        /// </summary>
        public int Power
        {
            get { return this.power; }
            set { this.power = value; }
        }

        /// <summary>
        /// jump rating
        /// </summary>
        public int Jump
        {
            get { return this.jump; }
            set { this.jump = value; }
        }

        /// <summary>
        /// cargo capacity in dTon
        /// </summary>
        public int Cargo
        {
            get { return this.cargo; }
            set { this.cargo = value; }
        }

        public int CargoHeld
        {
            get { return this.cargoHeld; }
            set { this.cargoHeld = value; }
        }

        public List<cargoDesc> Manifest
        {
            get { return this.manifest; }
        }

        public List<cargoDesc> Sold
        {
            get { return this.sold; }
        }

        /// <summary>
        /// current Cr
        /// </summary>
        public int Credits
        {
            get { return this.credits; }
            set { this.credits = value; }
        }

        public string SEC
        {
            get { return this.sec; }
            set { this.sec = value; }
        }

        public string Version
        {
            get { return this.version; }
        }

        public int Day
        {
            get { return this.day; }
            set { this.day = value; }
        }

        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }

        public string Date
        {
            get { return String.Format("{0:000} - {1:0000}", this.day, this.year);}
        }

        public int CargoID
        {
            get { return this.cargoid; }
            set
            {
                if (value > this.cargoid)
                {
                    this.cargoid = value;
                }
                else
                {
                    this.cargoid++;
                }
            }
        }

        public int TradeDM
        {
            get { return this.tradedm; }
        }

        /// <summary>
        /// monthly costs
        /// </summary>
        public int MonthlyCosts
        {
            get { return this.monthly; }
            set { this.monthly = value; }        
        }

        /// <summary>
        /// cost per jump
        /// </summary>
        public int PerJumpCost
        {
            get { return this.jumpCost; }
            set { this.jumpCost = value; }
        }

        public string ErrMessage
        {
            get { return this.errMsg; }
        }

        public string Filename
        {
            get { return this.filename; }
        }

        public List<travelDesc> Log
        {
            get { return this.log; }
        }

        public string SECDataFile
        {
            get { return this.secfile; }
        }

        public string SectorName
        {
            get { return this.sectorname; }
        }

        public bool IllegalCargoAllowed
        {
            get { return this.illegals; }
        }

        #endregion

        #region ship utilities

        /// <summary>
        /// add to the ship's travelogue
        /// </summary>
        /// <param name="note">string - note to add</param>
        /// <param name="date">string - transaction date</param>
        /// <remarks>will append if the date & system already exist, else add a new node</remarks>
        public void travelogue(string note)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(this.filename);
            
                XElement sn = xmlDoc.Root.Elements("Travelogue").Elements("system").Where(r => (string)r.Attribute("system") == this.sec & (string)r.Attribute("date") == this.Date).FirstOrDefault();
                if (sn != null)
                {
                    sn.Add(new XElement("note", note));
                }
                else
                {
                    xmlDoc.Root.Element("Travelogue").Add(
                            new XElement("system",
                                new XAttribute("system", this.sec),
                                new XAttribute("date", this.Date),
                                new XElement("note", note)));
                }
                xmlDoc.Save(this.filename);
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
            }
        }

        /// <summary>
        /// load a cargo based on the cargo ID
        /// </summary>
        /// <param name="cargoid"></param>
        /// <returns></returns>
        public Traveller.Starship.cargoDesc loadCargo(int cargoid)
        {
            Traveller.Starship.cargoDesc cargo = new Starship.cargoDesc();

            XDocument xdoc = XDocument.Load(this.filename);
            //XElement cargoNode = xmlDoc.Root.Elements("Cargo").Elements("lot").Where(r => (int)r.Attribute("cargoID") == cargo.cargoID
            //    & (string)r.Attribute("sold") == "false").FirstOrDefault();
            XElement sn = xdoc.Root.Elements("Cargo").Elements("lot").Where(r => (int)r.Attribute("cargoID") == cargoid).FirstOrDefault();
            if (sn != null)
            {
                cargo.desc = sn.Element("desc").Value;
                cargo.origCode = sn.Attribute("origCode").Value;
                cargo.dtons = Convert.ToInt32(sn.Attribute("dTons").Value);
                cargo.cargoID = cargoid;
                int.TryParse(sn.Element("purchase").Element("baseprice").Value, out cargo.basecostbuy);
                int.TryParse(sn.Element("purchase").Element("finalprice").Value, out cargo.purchasePrice);
                int.TryParse(sn.Element("selling").Element("baseprice").Value, out cargo.basecostsell);
                int.TryParse(sn.Element("selling").Element("finalprice").Value, out cargo.sellingPrice);
            }

            return cargo;
        }

        /// <summary>
        /// add a cargo to the ship's manifest
        /// </summary>
        /// <param name="cargo">cargoDesc - cargo to add</param>
        public void addCargo(cargoDesc cargo)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(this.filename);

                xmlDoc.Element("ShipData").SetElementValue("cargoID", ++cargoid);
                xmlDoc.Root.Element("Cargo").Add(
                        new XElement("lot",
                            new XAttribute("cargoID", this.cargoid.ToString()),
                            new XAttribute("origCode", cargo.origCode),
                            new XAttribute("dTons", cargo.dtons),
                            new XAttribute("sold", false),
                            new XElement("desc", cargo.desc),
                            new XElement("purchase",
                                new XAttribute("date", cargo.purchaseDate),
                                new XAttribute("system", cargo.origSEC),
                                new XElement("baseprice", cargo.basecostbuy),
                                new XElement("finalprice", cargo.purchasePrice),
                                new XElement("avMod", cargo.avBuy)),
                            new XElement("selling",
                                new XAttribute("date", " "),
                                new XAttribute("system", " "),
                                new XElement("baseprice", " "),
                                new XElement("finalprice", ""),
                                new XElement("avMod", ""))));
                xmlDoc.Save(this.filename);
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
            }
        }

        /// <summary>
        /// sell a cargo
        /// </summary>
        /// <param name="cargo">cargoDesc structure</param>
        /// <remarks>if we find the node, set the sold to true and update price and location sold</remarks>
        public void sellCargo(cargoDesc cargo)
        {
            this.errMsg = "";

            try
            {
                XDocument xmlDoc = XDocument.Load(this.filename);

                XElement cargoNode = xmlDoc.Root.Elements("Cargo").Elements("lot").Where(r => (int)r.Attribute("cargoID") == cargo.cargoID
                    & (string)r.Attribute("sold") == "false").FirstOrDefault();
                if (cargoNode != null)
                {
                    cargoNode.SetAttributeValue("sold", true);
                    XElement sellNode = cargoNode.Element("selling");
                    sellNode.SetAttributeValue("system", cargo.destSEC);
                    sellNode.SetElementValue("baseprice", cargo.basecostbuy);
                    sellNode.SetElementValue("avMod", cargo.avSell);
                    sellNode.SetAttributeValue("date", cargo.sellingDate);
                    sellNode.SetElementValue("finalprice", cargo.sellingPrice);

                    // and update the ship's coffers
                    this.cargoHeld -= cargo.dtons;          // free up the space
                    if (this.cargoHeld < 0)
                        this.cargoHeld = 0;                 // just in case
                    this.credits += cargo.sellingPrice;     // more money!

                    XElement shipNode = xmlDoc.Element("ShipData").Element("Ship");
                    shipNode.SetElementValue("CargoHeld", this.cargoHeld.ToString());
                    shipNode.SetElementValue("credits", this.credits.ToString());

                    // and update the datafile
                    xmlDoc.Save(this.filename);
                }
                else
                {
                    this.errMsg = "cannot find cargo! " + cargoid.ToString();
                }
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
            }
        }

        /// <summary>
        /// add days to the ship's log
        /// </summary>
        /// <param name="days">int - number of days to add</param>
        /// <remarks>will do the required year flip if necessary (365 days/year)</remarks>
        public void addDays(int days)
        {
            if (this.day + days < 365)
            {
                this.day += days;
            }
            else
            {
                this.year++;
                this.day = (this.day - 365 + days);
            }
        }

        /// <summary>
        /// make a jump
        /// </summary>
        /// <param name="sec">string - world jumping to (SEC format)</param>
        /// <remarks>add a week, add the travelogue, subtract jump cost</remarks>
        public void makeJump(string sec)
        {
            Traveller.World w = new World();
            Match worldMatch = w.worldRegex.Match(this.sec);
            string currentWorld = worldMatch.Groups["name"].Value;
            worldMatch = w.worldRegex.Match(sec);
            string nextWorld = worldMatch.Groups["name"].Value;

            this.travelogue(String.Format("Jumping from {0} to {1}", currentWorld, nextWorld));
            this.credits -= this.jumpCost;  // cost per jump
            this.sec = sec;                 // new system
            addDays(7);                     // jump
            save();
            this.travelogue(String.Format("arrived at {0}", nextWorld));
        }

        #endregion

        #region reporting

        /// <summary>
        /// print the ship's current cargo
        /// </summary>
        /// <param name="preview"></param>
        public void printManifest(bool detailed)
        {
            StringBuilder sb = new StringBuilder();

            int col1 = 24;
            int col2 = 29;
            int col3 = 12;
            int col4 = 8;

            // create two column headers, can have max of 4
            string ch1 = "Cargo Code".PadRight(col1) +
              "Description".PadRight(col2) +
              "Cost".PadLeft(col3) +
              "dTons".PadLeft(col4);
            int total = 5;

            // generate the detail lines (Dummy Data)
            int totalTons = 0;
            int totalCreds = 0;
            foreach (Traveller.Starship.cargoDesc lot in this.manifest)
            {
                string desc = lot.desc;
                if (desc.Length > 24)
                    desc = desc.Substring(0, 24);
                string origCode = lot.origCode;
                if (origCode.Length > 24)
                    origCode = origCode.Substring(0, 24);

                sb.Append(String.Format("{0,-24} {1,-30} {2,8} {3,8}",
                    origCode, desc, lot.purchasePrice, lot.dtons) +
                    Environment.NewLine);
                total += 5;
                totalTons += lot.dtons;
                totalCreds += lot.purchasePrice;

                // and if detailed, add the origin & other info
                if (detailed)
                {
                    sb.Append(String.Format("  origination: {0}", lot.origSEC) +
                        Environment.NewLine);
                    sb.Append(String.Format("  destination: {0}", lot.destSEC) +
                        Environment.NewLine);
                    sb.Append(String.Format("  purch date : {0}", lot.purchaseDate) +
                        Environment.NewLine);
                    sb.Append(String.Format("  notes      : {0}", lot.notes) +
                        Environment.NewLine + Environment.NewLine);
                    total += 25;
                }
            }

            // now we will pretend to have had a total break and change the
            // subtitle, after forcing a page change
            sb.Append(Environment.NewLine + " ".PadLeft(45) +
              "Total Cr" + totalCreds.ToString().PadLeft(11) +
              totalTons.ToString().PadLeft(9) + Environment.NewLine);

            // finally, set up & print the report
            // instantiate the object
            CPrintReportStringDemo.CPrintReportString cprs = new CPrintReportStringDemo.CPrintReportString();

            // set the title font points size and style
            cprs.TitleFontSize = 14;
            cprs.TitleFontStyle = TitleStyle.BoldItalic;

            // get margin extenders if any
            cprs.LeftMarginExtender = MarginExtender.OneHalfInch;
            cprs.RightMarginExtender = MarginExtender.OneHalfInch;
            cprs.TopMarginExtender = MarginExtender.OneHalfInch;
            cprs.BottomMarginExtender = MarginExtender.OneHalfInch;

            cprs.Footer = String.Format("Cargo Lading for {0} as of {1}", this.name, this.Date);

            // call the print or preview method
            string title = String.Format("Current Manifest for {0} on {1}", this.name, this.Date);
            string subTitle = String.Format("current location: {0}", this.sec);
            cprs.SubTitle2 = " ";
            cprs.SubTitle3 = "";
            cprs.SubTitle4 = "";
            cprs.PrintOrPreview(CharsPerLine.CPL80,
              sb.ToString(), title, subTitle, PrintPreview.Preview,
              PrintOrientation.Portrait, ch1);
        }

        /// <summary>
        /// print the ship's cargo history
        /// </summary>
        public void printHistory()
        {
            StringBuilder sb = new StringBuilder();

            int col1 = 25;
            int col2 = 25;
            int col3 = 9;
            int col4 = 8;
            int col5 = 8;

            // create two column headers, can have max of 4
            string ch1 = "Cargo Code".PadRight(col1) +
              "Description".PadRight(col2) +
              "Purchase".PadRight(col3) +
              "Sold".PadLeft(col4) +
              "Profit".PadLeft(col5);
            string ch2 = "   " +
                "sold at";
            int total = 5;

            // generate the detail lines (Dummy Data)
            int totalCreds = 0;
            int profit = 0;
            Traveller.World w = new World();
            foreach (Traveller.Starship.cargoDesc lot in this.sold)
            {
                Match worldMatch = w.worldRegex.Match(lot.origSEC);
                string origName = worldMatch.Groups["name"].Value;
                worldMatch = w.worldRegex.Match(lot.destSEC);
                string destName = worldMatch.Groups["name"].Value;
                profit = lot.sellingPrice - lot.purchasePrice;

                string desc = lot.desc;
                if (desc.Length > 24)
                    desc = desc.Substring(0, 24);
                string origCode = lot.origCode;
                if (origCode.Length > 24)
                    origCode = origCode.Substring(0, 24);

                sb.Append(String.Format("{0,-24} {1,-25} {2,8} {3,8} {4,8}",
                    lot.origCode, desc, lot.purchasePrice, lot.sellingPrice, profit) +
                    Environment.NewLine);
                sb.Append(String.Format("  purch date: {0} at {1} sold date: {2} at {3}", lot.purchaseDate, origName, lot.sellingDate, destName) +
                    Environment.NewLine + Environment.NewLine);
                total += 15;
                totalCreds += profit;
            }

            // now we will pretend to have had a total break and change the
            // subtitle, after forcing a page change
            sb.Append(Environment.NewLine + " ".PadLeft(45) +
              "Profit Cr" + totalCreds.ToString().PadLeft(12) + Environment.NewLine);

            // finally, set up & print the report
            // instantiate the object
            CPrintReportStringDemo.CPrintReportString cprs = new CPrintReportStringDemo.CPrintReportString();

            // set the title font points size and style
            cprs.TitleFontSize = 14;
            cprs.TitleFontStyle = TitleStyle.BoldItalic;
            cprs.Footer = String.Format("Confidential: Cargo History for {0}", this.name);

            // get margin extenders if any
            cprs.LeftMarginExtender = MarginExtender.OneHalfInch;
            cprs.RightMarginExtender = MarginExtender.OneHalfInch;
            cprs.TopMarginExtender = MarginExtender.OneHalfInch;
            cprs.BottomMarginExtender = MarginExtender.OneHalfInch;

            // call the print or preview method
            string title = String.Format("Cargo History for {0}", this.name);
            string subTitle = " ";
            cprs.SubTitle2 = " ";
            cprs.SubTitle3 = "";
            cprs.SubTitle4 = "";
            cprs.PrintOrPreview(CharsPerLine.CPL80,
              sb.ToString(), title, subTitle, PrintPreview.Preview,
              PrintOrientation.Portrait, ch1, ch2);
        }

        #endregion
    }

    #endregion

    #region Trade - trade class

    /// <summary>
    /// T5 trade stuff
    /// </summary>
    /// <remarks>errMsg will contain any error messages</remarks>
    public class Trade
    {
        public List<Traveller.Starship.cargoDesc> cargoes;      // cargo used for ALL versions

        private string errmsg;

        TravUtils util = new TravUtils();
        private Traveller.World world;
        private Traveller.Starship ship;
        private string version;
        public bool useExpandedTradeTables = false;     // use expanded trade tables

        /// <summary>
        /// trade instantiation
        /// </summary>
        /// <param name="ship">ship - Traveller ship object</param>
        /// <param name="world">world - Traveller world object</param>
        /// <remarks>deals with cargo</remarks>
        public Trade(Traveller.Starship ship, Traveller.World w)
        {
            this.errmsg = "";
            this.world = w;
            this.ship = ship;
            this.cargoes = new List<Starship.cargoDesc>();
            this.version = ship.Version;
        }

        /// <summary>
        /// find a cargo
        /// </summary>
        /// <returns>cargoDesc - universal cargo structure</returns>
        /// <remarks>loads appropriate data according to ship.version;
        /// CT and T5 return 1 cargo at a time, MT returns multiple cargoes</remarks>
        public List<Traveller.Starship.cargoDesc> findCargo()
        {
            Traveller.Starship.cargoDesc cargo;
            this.cargoes = new List<Starship.cargoDesc>();

            switch (ship.Version)
            {
                case "T5":
                    cargo = T5Trade();
                    cargoes.Add(cargo);
                    break;
                case "CT":
                    cargo = CTTrade();
                    cargoes.Add(cargo);
                    break;
                case "MT":
                    MTTrade();
                    break;
                default:
                    break;
            }

            return cargoes;
        }

        /// <summary>
        /// generate passengers according to version
        /// </summary>
        /// <returns>int[] - int array, where [0] = low, [1] = mid, [2] = high</returns>
        public int[] passengers()
        {
            int[] passengers = new int[] { 0, 0, 0 };
            switch (this.version)
            {
                case "T5":
                    T5Passengers(passengers);
                    break;
                default:
                    break;
            }

            return passengers;
        }

        #region T5 trade

        /// <summary>
        /// T5 trade process
        /// </summary>
        private Traveller.Starship.cargoDesc T5Trade()
        {
            Traveller.Starship.cargoDesc cargo = new Starship.cargoDesc();

            cargo.basecostbuy = 3000;
            int tcMod = 0;      // trade class mod in final price/tonnage

            // trade code adjustments
            foreach (Traveller.World.stTrade tc in this.world.TradeClass)
            {
                cargo.basecostbuy += tc.buymod;
                if (tc.buymod != 0)
                    tcMod++;
            }

            // TL mod adjustments
            int TLmod = 100 * util.HexToInt(this.world.TechLevel.ToString());
            cargo.basecostbuy += TLmod;

            // generate our code
            StringBuilder sCargo = new StringBuilder(world.TechLevel + " -");
            foreach (Traveller.World.stTrade tc in this.world.TradeClass)
            {
                sCargo.Append(" " + tc.code);
            }

            // and the base cost
            sCargo.Append(" Cr" + cargo.basecostbuy.ToString());

            cargo.origCode = sCargo.ToString();

            // see if we can find a random cargo based on trade classicification
            cargo.desc = this.findCargo(world.TradeClass);

            // and the tonnage: (flux + pop) x (total TCs + 1)  / mod = liason
            cargo.dtons = (util.flux() + util.HexToInt(world.Population.ToString()) * ++tcMod);

            // and our actual cost
            cargo.purchasePrice = cargo.basecostbuy * cargo.dtons;
            cargo.origSEC = world.SEC;

            return cargo;
        }

        /// <summary>
        /// generate T5 passenger counts
        /// </summary>
        /// <param name="passengers">int[] - passengers ([0]/low, [1]/mod, [2]high)</param>
        /// <returns>int[] - passengers by type (low/mid/high)</returns>
        private int[] T5Passengers(int[] passengers)
        {
            int pop = util.HexToInt(this.world.Population.ToString());

            // low
            passengers[0] = util.flux() + pop;
            if (passengers[0] < 0)
                passengers[0] = 0;

            // mid
            passengers[1] = util.flux() + pop;
            if (passengers[1] < 0)
                passengers[1] = 0;

            // high
            passengers[2] = util.flux() + pop;
            if (passengers[2] < 0)
                passengers[2] = 0;

            return passengers;
        }

        /// <summary>
        /// find a cargo item, T5 version
        /// </summary>
        /// <param name="tc">arraylist of trade codes</param>
        /// <returns>string - cargo description</returns>
        /// <remarks>uses T5Goods.csv, used to get as description of the item</remarks>
        private string findCargo(ArrayList tc)
        {
            ArrayList cargos = new ArrayList();
            string cargo = "";
            DataSet T5Goods = loadT5Goods();

            // each table is one of the trade class tables (Ag, etc)
            // geneate a list of cargos (1 per matching trade table)
            // then pick a random cargo from that
            try
            {
                foreach (Traveller.World.stTrade s in tc)
                {
                    foreach (DataTable dt in T5Goods.Tables)
                    {
                        if (dt.TableName == s.code)
                        {
                            // load a random cargo out of this
                            // 1. find the number of rows
                            // 2. pick a random row
                            int rows = dt.Rows.Count;
                            if (rows > 0)
                            {
                                do
                                {
                                    try
                                    {
                                        cargo = "";
                                        rows = util.rollDx(rows);
                                        if (dt.Rows[rows]["goods"].ToString().Length > 0)
                                        {
                                            cargo = dt.Rows[rows]["type"].ToString() + ": " + dt.Rows[rows]["goods"].ToString();
                                        }
                                    }
                                    catch
                                    {
                                        cargo = "";
                                    }
                                } while (cargo == "");
                            }
                            cargos.Add(cargo);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cargo = ex.Message;
                this.errmsg = ex.Message;
            }

            if (cargos.Count == 1)
                cargo = (string)cargos[0];
            else
            {
                if (cargos.Count > 0)
                    cargo = (string)cargos[util.rollDx(cargos.Count - 1)];
            }
            return cargo;
        }

        /// <summary>
        /// load the T5 goods table (T5Goods.csv)
        /// </summary>
        /// <returns>dataset of T5 goods</returns>
        private DataSet loadT5Goods()
        {
            DataSet t5 = new DataSet();
            try
            {
                string prevTC = "";
                StreamReader sr = new StreamReader("T5Goods.CSV");
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length < 2)
                        continue;
                    if (line.Substring(0, 1) == "#" | line.Substring(0, 1) == " ")
                        continue;

                    // new table?
                    if (line.Substring(0, 2) != prevTC)
                    {
                        prevTC = line.Substring(0, 2);
                        t5.Tables.Add(prevTC);
                        t5.Tables[prevTC].Columns.Add("type");
                        t5.Tables[prevTC].Columns.Add("goods");
                    }

                    // now load the goods; 1st two items are the tc & type
                    string[] goods = line.Split(new char[] { ',' });
                    for (int i = 2; i < goods.Length; i++)
                    {
                        DataRow row = t5.Tables[prevTC].NewRow();
                        row["type"] = goods[1];
                        row["goods"] = goods[i];
                        t5.Tables[prevTC].Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = ex.Message;
            }

            return t5;
        }

        /// <summary>
        /// sell a cargo
        /// </summary>
        /// <param name="cargo">cargoDesc structure from Starships</param>
        /// <param name="world">traveller world - place where the cargo is getting sold</param>
        /// <returns>cargoDesc - updated cargo structure with sold details</returns>
        public Traveller.Starship.cargoDesc sell(Traveller.Starship.cargoDesc cargo, Traveller.World world, int DM)
        {
            switch (this.version)
            {
                case "T5":
                    cargo = T5Sell(cargo, world, DM);
                    break;
                case "CT":
                    cargo = CTSell(cargo, world, DM);
                    break;
                case "MT":
                    cargo = MTSell(cargo, world, DM);
                    break;
                default:
                    break;
            }

            return cargo;
        }

        private Traveller.Starship.cargoDesc T5Sell(Traveller.Starship.cargoDesc cargo, Traveller.World world, int DM)
        {
            int baseprice = 5000;

            // get the original trade codes from the origCode
            string[] tc = cargo.origCode.Split(new char[] { ' ' });
            int origTL = util.HexToInt(tc[0]);      // cargo original world tech level

            // get a list of the selling worlds trade codes
            List<string> sellingTC = new List<string>();
            foreach (Traveller.World.stTrade sw in world.TradeClass)
            {
                sellingTC.Add(sw.code);
            }

            // T5 selling matrix
            string[] tcMod = File.ReadAllLines("T5Selling.csv");
            foreach (string line in tcMod)
            {
                for (int i = 2; i < tc.Length - 1; i++)
                {
                    if (line.StartsWith(tc[i]))                         // matching orig code trade code
                    {
                        string[] s = line.Split(new char[] { ',' });    // get the internal trade codes
                        int mod = Convert.ToInt32(s[s.Length - 1]);     // Cr mod
                        for (int j = 1; j < s.Length - 1; j++)          // don't include 1st or Cr mod
                        {
                            if (sellingTC.Contains(s[j]))               // we have a match
                                baseprice += mod;
                        }
                    }
                }
            }

            // and the TL mod: 10% (source TL - selling world TL)
            int tlDiff = origTL - util.HexToInt(world.TechLevel.ToString());    // difference in TL
            int tlPriceMod = (int)((double)baseprice * 0.10 * (double)tlDiff);
            baseprice += tlPriceMod;

            // and now our actual value roll
            // make sure we're within range; if not, reset
            DataTable actualValue = util.loadTable("T5Value.csv");
            int av = util.flux() + DM;
            int lowball = Convert.ToInt32(actualValue.Rows[0][0].ToString());
            if (av < lowball)
                av = lowball;
            int highball = Convert.ToInt32(actualValue.Rows[actualValue.Rows.Count - 1][0].ToString());
            if (av > highball)
                av = highball;

            // now to find that flux roll in our table
            double avMod = 0;
            for (int i = 0; i < actualValue.Rows.Count; i++)
            {
                if (Convert.ToInt32(actualValue.Rows[i][0].ToString()) == av)
                {
                    avMod = Convert.ToDouble(actualValue.Rows[i][2].ToString());
                    break;
                }
            }
            if (avMod == 0)
                avMod = 1;

            cargo.basecostsell = baseprice;
            cargo.sellingPrice = (int)((double)baseprice * avMod) * cargo.dtons;
            cargo.avSell = avMod;
            return cargo;
        }

        #endregion

        #region Classic Traveller Trade

        /// <summary>
        /// Classic Traveller trade - find a cargo
        /// </summary>
        private Traveller.Starship.cargoDesc CTTrade()
        {
            Traveller.Starship.cargoDesc cargo = new Starship.cargoDesc();

            // 1. roll d66
            // 1a. modifier on d1: pop >= 9, +1; pop <=5, -1
            // 1b (<1 = 1, > 6 = 6)
            // 2. find cargo
            // 3. determine quantity
            // 4. determine base cost

            int d1 = util.rollDx(6);
            int d2 = util.rollDx(6);
            int pop = util.HexToInt(this.world.Population.ToString());
            if (pop >= 9)
            {
                d1 += 1;
                if (d1 > 6)
                    d1 = 6;
            }
            if (pop <= 5)
            {
                d1 -= 1;
                if (d1 <= 0)
                    d1 = 1;
            }

            string d66 = d1.ToString() + d2.ToString();

            // find this item in our table
            string[] cargos = File.ReadAllLines("CTGoods.csv");
            List<string> actualValue = util.loadCSV("CTValue.CSV");

            foreach (string line in cargos)
            {
                string[] lot = line.Split(new char[] { ',' });
                if (d66 == lot[0])  // found it!
                {
                    cargo.desc = lot[1];                            // cargo description
                    cargo.basecostbuy = Convert.ToInt32(lot[2]);    // base cost before mods
                    cargo.basecostsell = cargo.basecostbuy;         // same price point in CT
                    cargo.origCode = this.world.TechLevel.ToString() + " - ";
                    foreach (Traveller.World.stTrade tc in this.world.TradeClass)
                    {
                        cargo.origCode += tc.code + " ";
                    }

                    // now figure the price mods for the roll
                    int mods = 0;
                    string[] tccodes = new string[] { "Ag", "Na", "In", "Ni", "Ri", "Po" };
                    for (int i = 4; i <= 9; i++)
                    {
                        foreach (Traveller.World.stTrade tc in world.TradeClass)
                        {
                            if (tc.code == tccodes[i - 4])
                            {
                                int tMod = 0;
                                int.TryParse(lot[i], out tMod);
                                mods += tMod;
                            }
                        }
                    }

                    // roll on the actual value table (-2 for 0=based table)
                    int roll = util.rollDx(6) + util.rollDx(6) + ship.TradeDM - 2;
                    string[] av = actualValue[roll].Split(new char[] { ',' });
                    cargo.avBuy = Convert.ToDouble(av[1]);
                    cargo.purchasePrice = (int)((double)cargo.basecostbuy * cargo.avBuy);
                    cargo.origCode += " Cr" + cargo.purchasePrice.ToString();

                    // and out quantity
                    string[] qty = lot[3].Split(new char[] { 'D', 'd', 'x' });
                    int dice = Convert.ToInt32(qty[0]);
                    for (int i = 0; i < dice; i++)
                    {
                        cargo.dtons += util.rollDx(6);
                    }
                    int multiplier = Convert.ToInt32(qty[2]);
                    cargo.dtons *= multiplier;

                    // and our final purchase price
                    cargo.purchasePrice *= cargo.dtons;

                    break;
                }
            }

            return cargo;
        }

        /// <summary>
        /// sell cargo, CT style
        /// </summary>
        /// <param name="cargo">cargoDesc - cargo item to sell</param>
        /// <param name="world">world object - where we are selling it</param>
        /// <param name="DM">int - any DMs to modifiy the actual value table</param>
        /// <returns></returns>
        private Traveller.Starship.cargoDesc CTSell(Traveller.Starship.cargoDesc cargo, Traveller.World world, int DM)
        {
            // 1. find our cargo in the trade table
            List<string> cargos = util.loadCSV("CTGoods.csv");

            string lot = "";
            foreach (string sCargo in cargos)
            {
                string[] l = sCargo.Split(new char[] { ',' });
                if (l[1] == cargo.desc)
                {
                    lot = sCargo;
                    break;
                }
            }

            if (lot.Length == 0)
            {
                cargo.sellingPrice = cargo.purchasePrice;
                return cargo;
            }

            // ok, we've found the cargo
            string[] tg = lot.Split(new char[] { ',' });
            int.TryParse(tg[2], out cargo.basecostsell);    // base price for selling

            // figure out the mods
            // must match current (selling) world's trade codes
            int mods = DM;
            string[] tccodes = new string[] { "Ag", "Na", "In", "Ni", "Ri", "Po" };
            for (int i = 10; i <= 15; i++)
            {
                foreach (Traveller.World.stTrade tc in world.TradeClass)
                {
                    if (tc.code == tccodes[i - 10])
                    {
                        int tMod = 0;
                        int.TryParse(tg[i], out tMod);
                        mods += tMod;
                    }
                }
            }

            // and roll on the actual value table
            List<string> actualValue = util.loadCSV("CTValue.CSV");
            int roll = util.rollDx(6) + util.rollDx(6) + mods + ship.TradeDM  - 2;  // -2 for 0-based roll
            if (roll < 0)
                roll = 0;
            if (roll > 14)
                roll = 14;
            string[] av = actualValue[roll].Split(new char[] { ',' });
            cargo.avSell = Convert.ToDouble(av[1]);
            cargo.sellingPrice = (int)((double)cargo.basecostsell * cargo.avSell) * cargo.dtons;

            return cargo;
        }

        #endregion

        #region Mongoose Trade

        /// <summary>
        /// generate the cargoes for Mongoose Systems
        /// </summary>
        private void MTTrade()
        {
            // 1. load our goods table, and our actual value table
            List<string> MTGoods = util.loadCSV("MTGoods.csv");
            List<string> avTable = util.loadCSV("MTValue.csv");
            List<string> expandedGoods = new List<string>();
            if (useExpandedTradeTables)
            {
                expandedGoods = util.loadCSV("MTGoodsExp.csv");
            }

            // 2. all common & trade class goods are available, so generate those lots
            foreach (string lot in MTGoods)
            {
                string[] item = lot.Split(new char[] { ',' });
                string available = item[4];
                if (available.ToLower() == "all")
                {
                    Traveller.Starship.cargoDesc cargo = MTCreateCargo(item, avTable, expandedGoods);
                    this.cargoes.Add(cargo);
                }
                else
                {
                    string[] availTC = available.Split(new char[] { ' ' });     // not all, get individual valid codes
                    foreach (string aTC in availTC)                             // for each of the item's valid trade codes
                    {
                        foreach (Traveller.World.stTrade wtc in this.world.TradeClass)  // for each of the world's trade codes
                        {
                            if (wtc.code == aTC)                                        // we have a match!
                            {
                                Traveller.Starship.cargoDesc cargo = MTCreateCargo(item, avTable, expandedGoods);
                                this.cargoes.Add(cargo);
                            }
                        }
                    }
                }
            }

            // 3. and now 1d6 of additional goods
            int randomGoods = util.rollDx(6);
            for (int i = 0; i < randomGoods; i++)
            {
                int d1 = util.rollDx(6);
                int d2 = util.rollDx(6);

                // check to see if illegals are allowed
                if (!this.ship.IllegalCargoAllowed)
                {
                    if (d1 == 6 & d2 < 6)
                        do
                        {
                            d1 = util.rollDx(6);
                        } while (d1 == 6);
                }

                // ok, we have a valid d66
                string d66 = String.Format("{0}{1}", d1, d2);

                foreach (string lot in MTGoods)
                {
                    string[] item = lot.Split(new char[] { ',' });
                    if (item[0] == d66)
                    {
                        Traveller.Starship.cargoDesc cargo = MTCreateCargo(item, avTable, expandedGoods);
                        this.cargoes.Add(cargo);
                    }
                }
            }
        }

        /// <summary>
        /// create a cargo based on the CSV line item
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        private Traveller.Starship.cargoDesc MTCreateCargo(string[] lot, List<string> avTable, List<string> expandedGoods)
        {
            Traveller.Starship.cargoDesc cargo = new Starship.cargoDesc();
            cargo.desc = lot[1];
            int.TryParse(lot[2], out cargo.basecostbuy);
            cargo.basecostsell = cargo.basecostbuy;

            // determine dTons
            string[] qty = lot[3].Split(new char[] { 'D', 'd', 'x' });
            int dice = Convert.ToInt32(qty[0]);
            for (int i = 0; i < dice; i++)
            {
                cargo.dtons += util.rollDx(6);
            }
            int multiplier = Convert.ToInt32(qty[2]);
            cargo.dtons *= multiplier;

            cargo.origCode = this.world.TechLevel.ToString() + " - ";

            // figure out the actual value
            // 1st, any trade class mods
            string[] buymods = lot[5].Split(new char[] { ' ' });
            int buyMax = 0;
            int mods = 0;
            int tmod = 0;

            // find the largest buy modifier
            foreach (Traveller.World.stTrade tc in this.world.TradeClass)
            {
                cargo.origCode += tc.code + " ";
                foreach (string bm in buymods)
                {
                    if (bm.StartsWith(tc.code))     // matching trade code
                    {
                        if (bm[2] == '+')
                        {
                            int.TryParse(bm[3].ToString(), out tmod);
                            if (tmod > buyMax)
                                buyMax = tmod;
                        }
                    }
                }
            }
            mods += buyMax;
            
            // if we're using the expanded trade tables, we'll need to replace
            // the description, base cost & tonnage
            if (this.useExpandedTradeTables)
            {
                List<string> myGoods = new List<string>();
                // 1st find the matching cargo
                foreach (string expanded in expandedGoods)
                {
                    string[] exp = expanded.Split(new char[] { ',' });
                    if (exp[0] == lot[0])
                    {
                        myGoods.Add(expanded);
                    }
                }

                // myGoods now has the list of expanded items; roll 2d6 and get the appropriate range
                // assuming we've actually found anything
                if (myGoods.Count > 0)
                {
                    int myroll = util.rollDx(6);
                    myroll += util.rollDx(6);
                    foreach (string exp in myGoods)
                    {
                        string[] good = exp.Split(new char[] { ',' });
                        int low = 0;
                        int high = 0;
                        int.TryParse(good[1], out low);
                        int.TryParse(good[2], out high);
                        if (myroll >= low & myroll <= high)
                        {
                            cargo.desc = good[3];

                            // tonnage
                            int baseDie = 0;
                            int mult = 0;
                            int tonnage = 0;
                            string[] sTonnage = good[4].Split(new char[] {'*'});
                            int.TryParse(sTonnage[0], out baseDie);
                            int.TryParse(sTonnage[1], out mult);
                            for (int i = 0; i < baseDie; i++)
                            {
                                tonnage += util.rollDx(6);
                            }
                            tonnage *= mult;
                            if (tonnage > 0)
                                cargo.dtons = tonnage;

                            // base cost
                            int myBase = cargo.basecostbuy;
                            int.TryParse(good[5], out myBase);
                            if (myBase > 0)
                                cargo.basecostbuy = myBase;
                            break;
                        }
                    }
                }
            }

            // and now roll on our actual value buy table;
            // this table allows for -1, so -1 = 0 position, 0 = 1 position; so we add 1 to compensate
            int roll = util.rollDx(6) + util.rollDx(6) + util.rollDx(6) + mods + 1;
            string[] av = avTable[roll].Split(new char[] { ',' });
            cargo.avBuy = Convert.ToDouble(av[1]);
            cargo.purchasePrice = (int)((double)cargo.basecostbuy * (double)cargo.dtons * cargo.avBuy);

            // and finalize our origCode
            cargo.origCode += "Cr" + cargo.basecostbuy.ToString();

            return cargo;
        }

        /// <summary>
        /// sell Mongoose Traveller cargo item
        /// </summary>
        /// <param name="cargo">cargo item to sell</param>
        /// <param name="world">world we are selling on</param>
        /// <param name="DM">int - DM for actual price</param>
        /// <returns>cargo - updated cargo (selling price updated)</returns>
        private Traveller.Starship.cargoDesc MTSell(Traveller.Starship.cargoDesc cargo, Traveller.World world, int DM)
        {
            // 1. load our goods table, and our actual value table
            List<string> MTGoods = util.loadCSV("MTGoods.csv");
            List<string> avTable = util.loadCSV("MTValue.csv");

            // 2. find our cargo in that list
            int sellDM = 0;
            bool foundit = false;

            foreach (string lot in MTGoods)
            {
                string[] item = lot.Split(new char[] { ',' });
                if (item[1] == cargo.desc)          // found the item
                {
                    int.TryParse(item[2], out cargo.basecostsell);
                    string[] sellmods = item[5].Split(new char[] { ' ' });
                    foreach (string sellmod in sellmods)
                    {
                        foreach (Traveller.World.stTrade wtc in world.TradeClass)
                        {
                            if (sellmod.StartsWith(wtc.code))
                            {
                                int dm = 0;
                                int.TryParse(sellmod[3].ToString(), out dm);
                                if (sellmod[2] == '+')
                                    sellDM += dm;
                                else
                                    sellDM -= dm;
                            }
                        }
                    }
                    foundit = true;
                    break;
                }
            }

            if (foundit)
            {
                // and now roll on our actual value buy table;
                // this table allows for -1, so -1 = 0 position, 0 = 1 position; so we add 1 to compensate
                int roll = util.rollDx(6) + util.rollDx(6) + util.rollDx(6) + sellDM + DM + 1;
                string[] av = avTable[roll].Split(new char[] { ',' });
                cargo.avSell = Convert.ToDouble(av[2]);
                cargo.sellingPrice = (int)((double)cargo.basecostsell * (double)cargo.dtons * cargo.avSell);
            }
            else
            {
                cargo.avSell = 0;
                cargo.basecostsell = cargo.basecostbuy;
                cargo.sellingPrice = cargo.basecostbuy * cargo.dtons;
            }

            return cargo;
        }

        #endregion
    }

   
    #endregion
}
