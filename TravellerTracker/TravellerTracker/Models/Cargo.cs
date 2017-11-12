﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Traveller.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public int dTons { get; set; }
        public int BasePurchasePrice { get; set; }
        public string CargoCode { get; set; }
        public string Description { get; set; }

        // FK to CargoType
        public int CargoTypeId { get; set; }

        public int TravellerVersionId { get; set; }  // which version of Traveller this belongs to; 0 = all

        // version specific items
        // classic d66 table
        public int D1 { get; set; }
        public int D2 { get; set; }
        public int QtyDie { get; set; }
        public int Multiplier { get; set; }

        public string GetCargoType()
        {
            if (CargoTypeId > 0)
                return TravellerTracker.App.DB.CargoTypes.Where(x => x.CargoTypeId == CargoTypeId).FirstOrDefault().Description;
            else
                return "n/a";
        }

        [NotMapped]
        public string ListDesc => string.Format("[{0} {1}] {2}", D1, D2, Description);

        // list of things we need to generate an available list of cargos when searching
        // isSpeculative indicates this is a speculative trade = must pay for
        // the Major, Minor, etc fields are per the destination world

        [NotMapped]
        public World Destination { get; set; }
        [NotMapped]
        public bool isSpeculative { get; set; }
        [NotMapped]
        public int Major { get; set; }
        [NotMapped]
        public int Minor { get; set; }
        [NotMapped]
        public int Incidental { get; set; }
        [NotMapped]
        public int HighPassage { get; set; }
        [NotMapped]
        public int MidPassage { get; set; }
        [NotMapped]
        public int LowPassage { get; set; }
    }
}
