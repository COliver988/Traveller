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

        // how many actual tons are carried
       
        public int dTons { get; set;}

        public string CargoCode { get; set; }  // BITS or other cargo code specific to this

        public int OriginWorldID { get; set; }    // where it originates from
    }
}
