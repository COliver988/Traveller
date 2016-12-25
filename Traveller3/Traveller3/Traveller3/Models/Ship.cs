using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Traveller3.Models
{
    public class Ship
    {
        public Ship() { }

        public string Name { get; set; }
        public int dTons { get; set; }
        public int CargoCapacity { get; set; }
        public int CargoCarried { get; set; }
        public int AvailableCargoSpace { get { return CargoCapacity - CargoCarried;  }  }
        public int FuelPerJump { get; set; }                // fuel per jump
        public World Location { get; set; }                 // current world location
        public ImperialCalendar ShipDate { get; set; }      // current date 

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
        }
    }
}
