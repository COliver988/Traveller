using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller.Models
{
    public class TravellerMapUniverse
    {
        public class Name
        {
            public string Text { get; set; }
            public string Lang { get; set; }
            public string Source { get; set; }
        }

        public class Sector
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string Milieu { get; set; }
            public string Abbreviation { get; set; }
            public string Tags { get; set; }
            public IList<Name> Names { get; set; }
            public string FirstName { get { return string.Format("{0} [{1}]", Names[0].Text, Tags); } }
        }

        public class SectorList
        {
            public List<Sector> Sectors { get; set; }
        }

        // world API checks
        public class World
        {
            public int HexX { get; set; }
            public int HexY { get; set; }
            public string Sector { get; set; }
            public string Uwp { get; set; }
            public int SectorX { get; set; }
            public int SectorY { get; set; }
            public string Name { get; set; }
            public string SectorTags { get; set; }
        }

        public class Subsector
        {
            public string Sector { get; set; }
            public string Index { get; set; }
            public int SectorX { get; set; }
            public int SectorY { get; set; }
            public string Name { get; set; }
            public string SectorTags { get; set; }
        }

        public class Item
        {
            public World World { get; set; }
            public Sector Sector { get; set; }
            public Subsector Subsector { get; set; }
        }

        public class Results
        {
            public int Count { get; set; }
            public List<Item> Items { get; set; }
        }

        public class RootObject
        {
            public Results Results { get; set; }
        }
    }
}
