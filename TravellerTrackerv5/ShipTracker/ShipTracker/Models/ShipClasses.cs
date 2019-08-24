using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTracker.Models
{
    public class ShipClasses
    {
        public int id { get; set; }
        public string ClassName { get; set; }
        public int Tonnage { get; set; }
        public string Classification { get; set; }
        public int HighPassengers { get; set; }
        public int MidPassengers { get; set; }
        public int LowPassengers { get; set; }
        public int Fuel { get; set; }
        public int Cargo { get; set; }
        public int Jump { get; set; }
        public int Maneuver { get; set; }
        public int Power { get; set; }
    }
}
