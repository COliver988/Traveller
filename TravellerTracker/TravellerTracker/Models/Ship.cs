using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Traveller.Support;
using TravellerTracker;

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

        public int HighPaxCarried { get; set; }
        public int MidPaxCarried { get; set; }
        public int LowPaxCarried { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public int AvailableCargo { get { return theClass == null ? 0 : theClass.Cargo - this.CargoCarried; } }

        [NotMapped]
        public ShipClass theClass => TravellerTracker.App.DB.ShipClasses.Where(x => x.ShipClassID == this.ShipClassID).FirstOrDefault();
        [NotMapped]
        public World theWorld => TravellerTracker.App.DB.Worlds.Where(x => x.WorldID == this.WorldID).FirstOrDefault();

        [NotMapped]
        public List<ShipLog> theLog => TravellerTracker.App.DB.Logs.Where(x => x.ShipId == this.ShipId).ToList();

        [NotMapped]
        public TravellerVersion theVersion => TravellerTracker.App.DB.TravellerVersions.Where(x => x.TravellerVersionId == this.TravellerVersionID).FirstOrDefault();

        [NotMapped]
        public int HighPaxAvail { get { return theClass != null ? theClass.HighPassage - HighPaxCarried : 0; } }

        [NotMapped]
        public int MidPaxAvail { get { return theClass != null ? theClass.MidPassage - MidPaxCarried : 0; } }

        [NotMapped]
        public int LowPaxAvail { get { return theClass != null ? theClass.LowPassage - LowPaxCarried : 0; } }

        [NotMapped]
        public List<ShipCargo> theCargo => TravellerTracker.App.DB.ShipCargo.Where(x => x.ShipID == this.ShipId && x.isActive == 1).ToList();

        [NotMapped]
        public Uri theJumpMapURL
        {
            get
            {
                if (theClass != null)
                {
                    TravellerMapAPI api = new TravellerMapAPI();
                    return api.JumpMapURL(theWorld, theClass.Jump);
                }
                else
                    return null;
            }
        }

        public void DeleteCargos()
        {
            App.DB.ShipCargo.RemoveRange(App.DB.ShipCargo.Where(x => x.ShipID == ShipId));
            CargoCarried = 0;
            App.DB.SaveChangesAsync();
        }
        public void DeleteLogs()
        {
            App.DB.Logs.RemoveRange(App.DB.Logs.Where(x => x.ShipId == ShipId));
            App.DB.SaveChangesAsync();
        }
    }
}
