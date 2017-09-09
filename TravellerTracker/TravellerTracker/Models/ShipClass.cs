using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller.Models
{
    public class ShipClass
    {
        public int ShipClassID { get; set; }
        public string Name { get; set; }
        public int dTons { get; set; }
        public int Jump { get; set; }
        public int Man { get; set; }
        public int Power { get; set; }
        public int Cargo { get; set; }

        public int Fuel { get; set; }
        public string Description
        {
            get { return string.Format("{0} {1} tons, {2}/{3}/{4} jump/man/power", Name, dTons, Jump, Man, Power); }
        }
    }
}
