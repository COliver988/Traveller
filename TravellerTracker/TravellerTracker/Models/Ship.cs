using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Traveller.Support;

namespace Traveller.Models
{
    public class Ship
    {
        // primary key
        public int ShipId { get; set; }

        public string Name { get; set; }
        public int CargoCarried { get; set; }
        public int Credits { get; set; }
        public int Fuel { get; set; }  // available fuel

        public int ShipClassID { get; set; }
        public int WorldID { get; set; }
        public int SectorID { get; set; }
        public int TravellerVersionID { get; set; }

        public string Era { get; set; }

        public int Day { get; set; }
        public int Year { get; set; }

        [NotMapped]
        public int AvailableCargo { get { return theclass.Cargo - this.CargoCarried; } }

        [NotMapped]
        public ShipClass theclass => TravellerTracker.App.DB.ShipClasses.Where(x => x.ShipClassID == this.ShipClassID).FirstOrDefault();
        [NotMapped]
        public World theWorld => TravellerTracker.App.DB.Worlds.Where(x => x.WorldID == this.WorldID).FirstOrDefault();

        [NotMapped]
        public List<ShipLog> theLog => TravellerTracker.App.DB.Logs.Where(x => x.ShipId == this.ShipId).ToList();

        [NotMapped]
        public TravellerVersion theVersion => TravellerTracker.App.DB.TravellerVersions.Where(x => x.TravellerVersionId == this.TravellerVersionID).FirstOrDefault();

        [NotMapped]
        public Uri theJumpMapURL
        {
            get
            {
                TravellerMapAPI api = new TravellerMapAPI();
                return api.JumpMapURL(theWorld, theclass.Jump);
            }
        }
    }
}
