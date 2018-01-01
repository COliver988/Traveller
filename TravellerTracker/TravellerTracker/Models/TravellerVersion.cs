using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Traveller.Models
{
    public class TravellerVersion
    {
        public enum CargoCodeTypes
        {
            BITS,
            T5,
            None
        }
        public int TravellerVersionId { get; set; }
        public string Name { get; set; }
        public int HighPassageCost { get; set; }
        public int MidPassageCost { get; set; }
        public int LowPassageCost { get; set; }
        public int Cost1Jump { get; set; }
        public int Cost2Jump { get; set; }
        public int Cost3Jump { get; set; }
        public int Cost4Jump { get; set; }
        public int Cost5Jump { get; set; }
        public int Cost6Jump { get; set; }
        public int DaysForCargoSearch { get; set; } 
        public CargoCodeTypes CargoCodeType { get; set; }
        public int D1TopRange { get; set; }
        public int D2TopRange { get; set; }

        [NotMapped]
        public CargoCodeTypeEnums CargoTypeEnums => new CargoCodeTypeEnums();
        [NotMapped]
        public List<ActualValue> ActualValues => TravellerTracker.App.DB.ActualValues.Where(x => x.TravellerVersionId == TravellerVersionId).ToList();
    }
}
