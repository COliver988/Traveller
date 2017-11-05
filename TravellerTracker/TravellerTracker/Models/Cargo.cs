namespace TravellerTracker.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public int dTons { get; set; }
        public int BasePurchasePrice { get; set; }
        public string CargoCode { get; set; }

        // FK to CargoType
        public int CargoTypeId { get; set; }
    }
}
