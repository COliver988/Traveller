using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerTracker.Models;

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

        public World (string line, int sectorID)
        {
            this.Hex = line.Substring(0, 4);
            this.Name = line.Substring(5, 20);
            this.UWP = line.Substring(26, 9);
            this.Remarks = line.Substring(36, 40);
            this.Importance = line.Substring(78, 6);
            this.Ex = line.Substring(84, 6);
            this.CulturalExt = line.Substring(93, 6);
            this.Bases = line.Substring(106, 2);
            this.Zone = line[109];
            this.Alliance = line.Substring(118, 4);
            this.Stellar = line.Substring(122);

        }
    }
}
