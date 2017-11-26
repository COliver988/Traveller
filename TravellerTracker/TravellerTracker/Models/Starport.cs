namespace Traveller.Models
{
    public class Starport
    {
        public int StarportId { get; set; }
        public char Class { get; set; }
        public string Quality { get; set; }
        public bool hasRefinedFuel { get; set; }
        public bool hasUnrefinedFuel { get; set; }
        public bool isStarport { get; set; }
        public string Yards { get; set; }
        public string Repairs { get; set; }
        public string Downport { get; set; }
    }
}
