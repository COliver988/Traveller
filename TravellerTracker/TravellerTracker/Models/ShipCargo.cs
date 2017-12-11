using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Traveller.Models
{
    public class ShipCargo
    {
        public enum CargoTypes
        {
            Speculative,
            Major,
            Minor,
            Incidental,
            Mail,
            HighPassage,
            MidPassage, 
            LowPassage
        }

        public int ShipCargoID { get; set; }
        public int ShipID { get; set; }
        public int CargoID { get; set; }
        public CargoTypes CargoType { get; set; }

        public int dTons { get; set;}           // how many actual tons are carried      
        public string CargoCode { get; set; }  // BITS or other cargo code specific to this
        public int OriginWorldID { get; set; }    // where it originates from
        public int DestinationID { get; set; }  // where it is going (if bulk)

        [NotMapped]
        public World OriginWorld => TravellerTracker.App.DB.Worlds.Where(x => x.WorldID == OriginWorldID).FirstOrDefault();
        [NotMapped]
        public World DestinationWorld => TravellerTracker.App.DB.Worlds.Where(x => x.WorldID == DestinationID).FirstOrDefault();
    }
}
