using System.ComponentModel.DataAnnotations.Schema;

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
        public int WorldID { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public string Log { get; set; }
        public byte[] Image { get; set; }

        [NotMapped]
        public string DayDisplay {  get { return string.Format("{0:000}", Day);  } }
        [NotMapped]
        public string DateDisplay {  get { return string.Format("{0:000}-{1:0000}", Day, Year); } }
    }
}
