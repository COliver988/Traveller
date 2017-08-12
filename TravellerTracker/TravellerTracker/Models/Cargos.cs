namespace TravellerTracker.Models
{
    public class Cargoes
    {
        public int CargoesId { get; set; }
        public int ShipID { get; set; }
        public string Cargo { get; set; }
        public int dTons { get; set; }
        public int PurchasePrice { get; set; }
        public string CargoCode { get; set; }
        public string OriginationSystem { get; set; }
    }
}
