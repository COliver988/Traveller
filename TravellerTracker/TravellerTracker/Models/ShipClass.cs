namespace Traveller.Models
{
    public class ShipClass
    {
        public int ShipClassID { get; set; }
        public string Name { get; set; }
        public int dTons { get; set; }
        public int Jump { get; set; }
        public int Man { get; set; }
        public int Power { get; set; }
        public int Cargo { get; set; }
        public int Fuel { get; set; }
        public int HighPassage { get; set; }
        public int MidPassage { get; set; }
        public int LowPassage { get; set; }
        public string HGClass { get; set; } // see High Guard
        public int FuelPerParsec { get; set; }  // fuel per jump (Classic = 10% of dTon)
        public int WeeksEndurance { get; set; }

        public string Description
        {
            get { return string.Format("{0} {1} tons, {2}/{3}/{4} jump/man/power", Name, dTons, Jump, Man, Power); }
        }
    }
}
