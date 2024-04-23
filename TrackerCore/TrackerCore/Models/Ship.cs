using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrackerCore.Models
{
    public class Ship
    {
        public int id { get; set; }

        [ForeignKey("ShipClass")]
        public int ShipClassID { get; set; }

        [ForeignKey("World")]
        public int WorldID { get; set; }

        public string Name { get; set; }
        public int currentCargo { get; set; }
        public int currentFuel { get; set; }
        public int currentHighPassengers { get; set; }
        public int currentMidPassengers { get; set; }
        public int currentLowPassengers { get; set; }
        public int currentCredits { get; set; }
        public string currentWorld { get; set; }




        [NotMapped]
        public ShipClass shipClass => App.DB.ShipClasses.Where(x => x.id == ShipClassID).FirstOrDefault();
    }
}
