using Traveller.Models;

namespace Traveller.Models
{
    public class ShipLog
    {
        public ShipLog() { }

        public ShipLog (Ship ship)
        {
            ShipId = ship.ShipId;
            Year = ship.Year;
            Day = ship.Day;
        }
        public int ShipLogId { get; set; }
        public int ShipId { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public string Log { get; set; }
    }
}
