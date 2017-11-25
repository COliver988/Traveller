using System.ComponentModel.DataAnnotations.Schema;

namespace Traveller.Models
{
    public class WorldLog
    {
        public WorldLog (Ship ship)
        {
            WorldId = ship.theWorld.WorldID;
            ShipId = ship.ShipId;
            Year = ship.Year;
            Day = ship.Day;
        }
        public int WorldLogId { get; set; }
        public int WorldId { get; set; }
        public int ShipId { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public string Log { get; set; }

        [NotMapped]
        public string DayDisplay {  get { return string.Format("{0:000}", Day);  } }
    }
}
