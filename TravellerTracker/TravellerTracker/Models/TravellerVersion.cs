namespace Traveller.Models
{
    public class TravellerVersion
    {
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
    }
}
