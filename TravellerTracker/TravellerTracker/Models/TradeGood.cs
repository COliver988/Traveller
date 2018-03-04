namespace Traveller.Models
{
    public class TradeGood
    {
        public int ID { get; set; }
        public int CargoTypeID { get; set; }   // FK to Cargo Type
        public string TradeCode { get; set; }  // trade code
        public string Description { get; set; }
    }
}
