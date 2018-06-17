using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TravellerTracker;

namespace Traveller.Models
{
    public class TradeGood
    {
        public int ID { get; set; }
        public int CargoTypeID { get; set; }   // FK to Cargo Type
        public string TradeCode { get; set; }  // trade code
        public string Description { get; set; }

        [NotMapped]
        public string theCargoType {  get { return App.DB.CargoTypes.Where(x => x.CargoTypeId == this.CargoTypeID).FirstOrDefault().Description;  } }
    }
}
