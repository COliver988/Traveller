namespace Traveller.Models
{
    public class ShipCargo
    {
        public enum CargoTypes
        {
            Speculative,
            Major,
            Minor,
            Incidental,
            Mail
        }

        public int ShipCargoID { get; set; }
        public int ShipID { get; set; }
        public int CargoID { get; set; }
        public CargoTypes CargoType { get; set; }

        public int dTons { get; set;}           // how many actual tons are carried      
        public string CargoCode { get; set; }  // BITS or other cargo code specific to this
        public int OriginWorldID { get; set; }    // where it originates from
    }
}
