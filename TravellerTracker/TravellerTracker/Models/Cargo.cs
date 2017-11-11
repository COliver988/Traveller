namespace Traveller.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public int dTons { get; set; }
        public int BasePurchasePrice { get; set; }
        public string CargoCode { get; set; }
        public string Desciption { get; set; }

        // FK to CargoType
        public int CargoTypeId { get; set; }

        public int TravellerVersionId { get; set; }  // which version of Traveller this belongs to; 0 = all

        // version specific items
        // classic d66 table
        public int D1 { get; set; }
        public int D2 { get; set; }
        public int QtyDie { get; set; }
        public int Multiplier { get; set; }
    }
}
