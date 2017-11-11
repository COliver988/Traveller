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

            if (db.Cargo.Count() == 0)
            {
                // classic
                db.Add(new Cargo() { D1 = 1, D2 = 1, BasePurchasePrice = 3000, Desciption = "Textiles", QtyDie = 3, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 1, D2 = 2, BasePurchasePrice = 7000, Desciption = "Polymers", QtyDie = 4, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 1, D2 = 3, BasePurchasePrice = 10000, Desciption = "Liquor", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 1, D2 = 4, BasePurchasePrice = 1000, Desciption = "Wood", QtyDie = 2, Multiplier = 10 });
                db.Add(new Cargo() { D1 = 1, D2 = 5, BasePurchasePrice = 20000, Desciption = "Crystals", QtyDie = 1, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 1, D2 = 6, BasePurchasePrice = 1000000, Desciption = "Radioactives", QtyDie = 1, Multiplier = 1 });

                db.Add(new Cargo() { D1 = 2, D2 = 1, BasePurchasePrice = 500, Desciption = "Steel", QtyDie = 4, Multiplier = 10 });
                db.Add(new Cargo() { D1 = 2, D2 = 2, BasePurchasePrice = 2000, Desciption = "Copper", QtyDie = 2, Multiplier = 10 });
                db.Add(new Cargo() { D1 = 2, D2 = 3, BasePurchasePrice = 1000, Desciption = "Aluminum", QtyDie = 5, Multiplier = 10 });
                db.Add(new Cargo() { D1 = 2, D2 = 4, BasePurchasePrice = 9000, Desciption = "Tin", QtyDie = 3, Multiplier = 10 });
                db.Add(new Cargo() { D1 = 2, D2 = 5, BasePurchasePrice = 70000, Desciption = "Silver", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 2, D2 = 6, BasePurchasePrice = 200000, Desciption = "Special ALloys", QtyDie = 1, Multiplier = 1 });

                db.Add(new Cargo() { D1 = 3, D2 = 1, BasePurchasePrice = 10000, Desciption = "Petrochemicals", QtyDie = 1, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 3, D2 = 2, BasePurchasePrice = 300, Desciption = "Grain", QtyDie = 8, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 3, D2 = 3, BasePurchasePrice = 1500, Desciption = "Meat", QtyDie = 4, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 3, D2 = 4, BasePurchasePrice = 6000, Desciption = "Spices", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 3, D2 = 5, BasePurchasePrice = 1000, Desciption = "Fruit", QtyDie = 2, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 3, D2 = 6, BasePurchasePrice = 100000, Desciption = "Pharmaceuticals", QtyDie = 1, Multiplier = 1 });

                db.Add(new Cargo() { D1 = 4, D2 = 1, BasePurchasePrice = 1000000, Desciption = "Gems", QtyDie = 1, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 4, D2 = 2, BasePurchasePrice = 30000, Desciption = "Firearms", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 4, D2 = 3, BasePurchasePrice = 30000, Desciption = "Ammunition", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 4, D2 = 4, BasePurchasePrice = 10000, Desciption = "Blades", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 4, D2 = 5, BasePurchasePrice = 10000, Desciption = "Tools", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 4, D2 = 6, BasePurchasePrice = 50000, Desciption = "Body Armor", QtyDie = 2, Multiplier = 1 });

                db.Add(new Cargo() { D1 = 5, D2 = 1, BasePurchasePrice = 1000000, Desciption = "Aircraft", QtyDie = 1, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 5, D2 = 2, BasePurchasePrice = 6000000, Desciption = "Air/raft", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 5, D2 = 3, BasePurchasePrice = 10000000, Desciption = "Computers", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 5, D2 = 4, BasePurchasePrice = 3000000, Desciption = "All Terrain Vehicles", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 5, D2 = 5, BasePurchasePrice = 7000000, Desciption = "Armored Vehicles", QtyDie = 2, Multiplier = 1 });
                db.Add(new Cargo() { D1 = 5, D2 = 6, BasePurchasePrice = 150000, Desciption = "Farm Machinery", QtyDie = 2, Multiplier = 1 });

                db.Add(new Cargo() { D1 = 6, D2 = 1, BasePurchasePrice = 100000, Desciption = "Electronic Parts", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 6, D2 = 2, BasePurchasePrice = 70000, Desciption = "Mechanical Parts", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 6, D2 = 3, BasePurchasePrice = 250000, Desciption = "Cybernetic Parts", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 6, D2 = 4, BasePurchasePrice = 150000, Desciption = "Computer Parts", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 6, D2 = 5, BasePurchasePrice = 750000, Desciption = "Machine Tools", QtyDie = 1, Multiplier = 5 });
                db.Add(new Cargo() { D1 = 6, D2 = 6, BasePurchasePrice = 400000, Desciption = "Vacc Suites", QtyDie = 1, Multiplier = 5 });
            }

            db.SaveChangesAsync();
        }
    }
}
