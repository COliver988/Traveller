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
            public string FirstName { get { return string.Format("{0} [{1}]", Names[0].Text, Names[0].Lang); } }
        }

        public class SectorList
        {
            public List<Sector> Sectors { get; set; }
        }
    }
}
