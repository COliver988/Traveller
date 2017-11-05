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
            }

            if (db.TravellerVersions.Count() == 0)
            {
                db.Add(new TravellerVersion() { Name = "Classic" });
                db.Add(new TravellerVersion() { Name = "Mongoose Traveller" });
                db.Add(new TravellerVersion() { Name = "T5" });
            }

            if (db.CargoTypes.Count() == 0)
            {
                db.Add(new CargoType() { Type = "Consumables", Description = "Consumables are food and drink, and may also include aromatics. Consumable foods are gourmet items, common flavorings or staples necessary on worlds where it cannot be produced." });
                db.Add(new CargoType() { Type = "Data", Description = "Data is information that can be consumed, reproduced or processed on the target workd. It includes books, tapes, wafers, software, creative works and scientific data." });
                db.Add(new CargoType() { Type = "Entertainments", Description = "Creative workss and diversionsa are always in demand." });
                db.Add(new CargoType() { Type = "Imbalances", Description = "When the cost of producing a trade item is very low, then it can be shipped between the stara and sold at a market for less than it can be produced locally. Worlds with low labor costs can often produce goods that can be sold elsewhere at a profit." });
                db.Add(new CargoType() { Type = "Manufactureds", Description = "Worlds with establisged factories export their products to worlds that cannot produce them." });
                db.Add(new CargoType() { Type = "Novelties", Description = "New products never before seen." });
                db.Add(new CargoType() { Type = "Pharma", Description = "Pharmaceuticals and medicine for treatment of all manner of illness or disability." });
                db.Add(new CargoType() { Type = "Rares", Description = "Many trade goods are in demand because of their rarity or relative scarcity." });
                db.Add(new CargoType() { Type = "Raws", Description = "One of the basic trade goos in interstellar trade is raw materials." });
                db.Add(new CargoType() { Type = "Red Tape", Description = "Because there are interstellar governments, the products of their bureacracy must be distributed throughout their area of authority." });
                db.Add(new CargoType() { Type = "Samples", Description = "Newly discovered, created or manufactured items may be distributed to other workds for analysis or evaluation." });
                db.Add(new CargoType() { Type = "Scrap/Waste", Description = "The trash of some worlds can be a valued commodity on other worlds." });
                db.Add(new CargoType() { Type = "Uniques", Description = "Some items cannot be reproduced, adding value to the product." });
                db.Add(new CargoType() { Type = "Valuta", Description = "Sometimes shipments between worlds consist of money itself." });
            }

            db.SaveChangesAsync();
        }
    }
}
