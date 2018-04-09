namespace TravellerTracker.Models
{
    public class TradeClassEffects
    {
        public int TradeClassEffectsID { get; set; }
        public string Source { get; set; }          // source world trade code
        public string Destination { get; set; }     //selling on
        public int Adjustment { get; set; }         // add to the selling price
    }
}
