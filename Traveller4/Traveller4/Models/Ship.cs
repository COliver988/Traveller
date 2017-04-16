using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using Traveller4.Support;
using Microsoft.EntityFrameworkCore;
using static Traveller4.Support.Support;

namespace Traveller4.Models
{
    public class Ship
    {
        private int id;
        public string Name { get; set; }
        public int dTons { get; set; }
        public int CargoCapacity { get; set; }
        public int CargoCarried { get; set; }
        public int AvailableCargoSpace { get { return CargoCapacity - CargoCarried;  }  }
        public int FuelPerJump { get; set; }                // fuel per jump
        public Versions Version { get; set; }                 // version: CT / T5 / MT (Mongoose)
        public ImperialCalendar ShipDate { get; set; }

        // primary key
        public int ID {  get { return id; } }
    }
}
