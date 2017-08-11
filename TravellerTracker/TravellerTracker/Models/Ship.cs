using System.ComponentModel.DataAnnotations.Schema;
using Traveller.Support;

namespace Traveller.Models
{
    public class Ship
    {
        // primary key
        public int ShipId { get; set; }

        public string Name { get; set; }
        public int dTons { get; set; }
        public int CargoCapacity { get; set; }
        public int CargoCarried { get; set; }
        public int AvailableCargoSpace { get { return CargoCapacity - CargoCarried;  }  }
        public int FuelPerJump { get; set; }                // fuel per jump

        public int ShipClassID { get; set; }
        public ShipClass Class { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
    }
}
