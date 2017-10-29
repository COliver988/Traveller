using System.Linq;
using Traveller.Models;
using TravellerTracker.Models;

namespace TravellerTracker.Support
{
    public class Seed
    {
        public void SeedDB(TravellerContext db)
        {
            if (db.TradeClassifications.Count() == 0)
            {
                TradeClassification tc = new TradeClassification();
                tc.Classification = "Ag";
                tc.Name = "Agriculteral";
                tc.Description = "The world has climate and conditions that promote farming and ranching. It is a producer of inexpensive food stuffs. It also is a source of unusual, exotic or strange delicacies";
                tc.Sizes = "";
                tc.Atmospheres = "4 5 6 7 8 9";
                tc.Hydro = "4 5 6 7 8";
                tc.Pop = "5 6 7";
                tc.Gov = "";
                tc.Law = "";

                db.Add(tc);
                db.SaveChangesAsync();
            }
        }
    }
}
