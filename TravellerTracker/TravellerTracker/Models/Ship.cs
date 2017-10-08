using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Traveller.Models
{
    public class Ship
    {
        // primary key
        public int ShipId { get; set; }

        public string Name { get; set; }
        public int CargoCarried { get; set; }
        public int Credits { get; set; }

        public int ShipClassID { get; set; }
        public int WorldID { get; set; }
        public int SectorID { get; set; }

        public string Era { get; set; }

        public int Day { get; set; }
        public int Year { get; set; }

        [NotMapped]
        public ShipClass theclass => TravellerTracker.App.DB.ShipClasses.Where(x => x.ShipClassID == this.ShipClassID).FirstOrDefault();
    }
}
