using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Traveller.Support;

namespace Traveller.Models
{
    public class World
    {
        public World() { }

        public int WorldID { get; set; }
        public string Hex { get; set; }
        public string Name { get; set; }
        public string UWP { get; set; }
        public string Remarks { get; set; }
        public string Importance { get; set; }
        public string Ex { get; set; }
        public string CulturalExt { get; set; }
        public string PBG { get; set; }
        public string Alliance { get; set; }
        public string Stellar { get; set; }
        public int WorldCount { get; set; }
        public string Bases { get; set; }
        public char Zone { get; set; }
        public int SectorID { get; set; }

        public World(string line, int sectorID)
        {
            this.Hex = line.Substring(0, 4);
            this.Name = line.Substring(5, 20);
            this.UWP = line.Substring(26, 9);
            this.Remarks = line.Substring(36, 40);
            this.Importance = line.Substring(77, 6);
            this.Ex = line.Substring(84, 6);
            this.CulturalExt = line.Substring(92, 6);
            this.Bases = line.Substring(106, 2);
            this.Zone = line[109];
            this.PBG = line.Substring(110, 3);
            this.Alliance = line.Substring(117, 4);
            this.Stellar = line.Substring(122);
        }

        [NotMapped]
        public Sector theSector => TravellerTracker.App.DB.Sectors.Where(x => x.SectorID == this.SectorID).FirstOrDefault();
        [NotMapped]
        public Starport thePort => TravellerTracker.App.DB.Starports.Where(x => x.Class == this.Starport).FirstOrDefault();
        [NotMapped]
        public List<ShipLog> theLog => TravellerTracker.App.DB.Logs.Where(x => x.WorldID == this.WorldID).ToList();

        [NotMapped]
        public int PopMultiplier {  get { return  Utilities.HexToInt(PBG[0]); } }
        [NotMapped]
        public int Belts {  get { return  Utilities.HexToInt(PBG[1]); } }
        [NotMapped]
        public int GasGiants {  get { return  Utilities.HexToInt(PBG[2]); } }

        [NotMapped]
        public string Description { get { return string.Format("{0} {1} {2}", Hex, Name, UWP).Replace("  ", " "); } }

        // UWP in A123456-7 format
        [NotMapped]
        public char Starport { get { return UWP[0]; } }
        [NotMapped]
        public int Size { get { return Utilities.HexToInt(UWP[1]); } }
        [NotMapped]
        public int Atmosphere { get { return Utilities.HexToInt(UWP[2]); } }
        [NotMapped]
        public int Hydro { get { return Utilities.HexToInt(UWP[3]); } }
        [NotMapped]
        public int Pop { get { return Utilities.HexToInt(UWP[4]); } }
        [NotMapped]
        public int Gov { get { return Utilities.HexToInt(UWP[5]); } }
        [NotMapped]
        public int Law { get { return Utilities.HexToInt(UWP[6]); } }
        [NotMapped]
        public int Tech { get { return Utilities.HexToInt(UWP[8]); } }

        public List<World> JumpRange(int jump)
        {
            Utilities util = new Utilities();
            List<World> results = new List<World>();
            foreach (World world in TravellerTracker.App.DB.Worlds.Where(x => x.SectorID == this.SectorID))
            {
                if (this.Hex != world.Hex)
                    if (util.calcDistance(this.Hex, world.Hex) <= jump)
                        results.Add(world);
            }

            return results;
        }
    }
}
