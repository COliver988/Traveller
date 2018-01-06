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
        public int Cost1HighPax { get; set; }
        public int Cost2HighPax { get; set; }
        public int Cost3HighPax { get; set; }
        public int Cost4HighPax { get; set; }
        public int Cost5HighPax { get; set; }
        public int Cost6HighPax { get; set; }
        public int Cost1MidPax { get; set; }
        public int Cost2MidPax { get; set; }
        public int Cost3MidPax { get; set; }
        public int Cost4MidPax { get; set; }
        public int Cost5MidPax { get; set; }
        public int Cost6MidPax { get; set; }
        public int Cost1LowPax { get; set; }
        public int Cost2LowPax { get; set; }
        public int Cost3LowPax { get; set; }
        public int Cost4LowPax { get; set; }
        public int Cost5LowPax { get; set; }
        public int Cost6LowPax { get; set; }
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

        [NotMapped]
        public int[] HighPax => new int[6] { Cost1HighPax, Cost2HighPax, Cost3HighPax, Cost4HighPax, Cost5HighPax, Cost6HighPax };

        [NotMapped]
        public int[] MidPax => new int[6]
           { Cost1MidPax, Cost2MidPax, Cost3MidPax, Cost4MidPax, Cost5MidPax, Cost6MidPax };

        [NotMapped]
        public int[] LowPax => new int[6]
            { Cost1LowPax, Cost2LowPax, Cost3LowPax, Cost4LowPax, Cost5LowPax, Cost6LowPax };
    }
}
