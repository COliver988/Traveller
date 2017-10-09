using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller.Models
{
    public class ShipCargo
    {
        public int ShipCargoID { get; set; }
        public int ShipID { get; set; }
        public int CargoID { get; set; }
    }
}
