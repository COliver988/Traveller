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
                tc.Atmospheres = "456789";
                tc.Hydro = "45678";
                tc.Pop = "567";
                tc.Gov = "";
                tc.Law = "";

                db.Add(tc);
                db.Add(new TradeClassification() { Classification = "As", Name = "Asteroid", Sizes = "0", Atmospheres = "0", Hydro = "0", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "De", Name = "Desert", Sizes = "", Atmospheres = "23456789", Hydro = "0", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Fl", Name = "Fluid",  Sizes = "", Atmospheres = "ABC", Hydro = "123456789A", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ga", Name = "Garden World", Sizes = "678", Atmospheres = "568", Hydro = "567", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "He", Name = "Hell World", Sizes = "3459ABC", Atmospheres = "2479ABC", Hydro = "012", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ic", Name = "Ice-Capped", Sizes = "", Atmospheres = "01", Hydro = "123456789A", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Oc", Name = "Ocean World", Sizes = "ABC", Atmospheres = "", Hydro = "A", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Va", Name = "Vacuum", Sizes = "", Atmospheres = "0", Hydro = "", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false });
                db.Add(new TradeClassification() { Classification = "Wa", Name = "Water World", Sizes = "56789", Atmospheres = "", Hydro = "A", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});

                db.Add(new TradeClassification() { Classification = "Di", Name = "Dieback", Sizes = "", Atmospheres = "", Hydro = "", Pop = "0", Gov = "0", Law = "0", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Ba", Name = "Barren", Sizes = "", Atmospheres = "", Hydro = "", Pop = "0", Gov = "0", Law = "0", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Lo", Name = "Low Population", Sizes = "", Atmospheres = "", Hydro = "", Pop = "123 ", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ni", Name = "Non-Industrial", Sizes = "", Atmospheres = "", Hydro = "", Pop = "456", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ph", Name = "Pre-High", Sizes = "", Atmospheres = "", Hydro = "", Pop = "8", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Hi", Name = "High Population", Sizes = "", Atmospheres = "", Hydro = "", Pop = "9ABC", Gov = "", Law = "", IsManuallyAssigned = false});

                db.Add(new TradeClassification() { Classification = "Pa", Name = "Pre-Agricultural", Sizes = "", Atmospheres = "45678", Hydro = "45678", Pop = "48", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ag", Name = "Agricultural", Sizes = "", Atmospheres = "45678", Hydro = "45678", Pop = "567", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Na", Name = "Non-Agricultural", Sizes = "", Atmospheres = "0123", Hydro = "0123", Pop = "6789ABC", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Pi", Name = "Pre-Industrial", Sizes = "", Atmospheres = "012479", Hydro = "", Pop = "78", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "In", Name = "Industrial", Sizes = "", Atmospheres = "012479", Hydro = "", Pop = "9ABC", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Po", Name = "Poor", Sizes = "", Atmospheres = "2345", Hydro = "0123", Pop = "", Gov = "", Law = "", IsManuallyAssigned = false});
                db.Add(new TradeClassification() { Classification = "Ri", Name = "Rich", Sizes = "", Atmospheres = "68", Hydro = "",  Pop = "678", Gov = "", Law = "", IsManuallyAssigned = false});

                db.Add(new TradeClassification() { Classification = "Fr", Name = "Frozen", Sizes = "23456789", Atmospheres = "", Hydro = "123456789A", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Ho", Name = "Hot", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Co", Name = "Cold", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Lk", Name = "Locked", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Tr", Name = "Tropic", Sizes = "6789", Atmospheres = "456789", Hydro = "34567", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Tu", Name = "Tundra", Sizes = "6789", Atmospheres = "456789", Hydro = "34567", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Tz", Name = "Twilight Zone", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});

                db.Add(new TradeClassification() { Classification = "Fa", Name = "Farming", Sizes = "456789", Atmospheres = "45678", Hydro = "", Pop = "23456", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Mi", Name = "Mining", Sizes = "", Atmospheres = "", Hydro = "", Pop = "23456", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Mr", Name = "Military Rule", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Px", Name = "Prison Exile", Sizes = "23AB", Atmospheres = "12345", Hydro = "", Pop = "3456", Gov = "", Law = "6789", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Pe", Name = "Penal Colony", Sizes = "23AB", Atmospheres = "12345", Hydro = "", Pop = "3456", Gov = "6", Law = "6789", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Re", Name = "Reserve", Sizes = "", Atmospheres = "", Hydro = "", Pop = "1234", Gov = "6", Law = "45", IsManuallyAssigned = true});

                db.Add(new TradeClassification() { Classification = "Cp", Name = "Subsector Capital", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Cs", Name = "Sector Capital", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "",  IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Cx", Name = "Capital", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "",  IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Cy", Name = "Colony", Sizes = "", Atmospheres = "", Hydro = "", Pop = "56789A", Gov = "6", Law = "0123", IsManuallyAssigned = true});

                db.Add(new TradeClassification() { Classification = "Sa", Name = "Satellite", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Fo", Name = "Forbidden", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Pz", Name = "Puzzle", Sizes = "", Atmospheres = "", Hydro = "", Law = "", Gov = "", Pop = "789ABC", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Da", Name = "Dangerous", Sizes = "", Atmospheres = "", Hydro = "", Law = "", Gov = "", Pop = "0123456", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "Ab", Name = "Data Repository", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
                db.Add(new TradeClassification() { Classification = "An", Name = "Ancient Site", Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Law = "", Gov = "", IsManuallyAssigned = true});
            }

            if (db.TravellerVersions.Count() == 0)
            {
                db.Add(new TravellerVersion() { Name = "Mongoose Traveller" });
                db.Add(new TravellerVersion() { Name = "T5" });
                db.Add(new TravellerVersion() { Name = "Classic" });
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
                db.Add(new CargoType() { Type = "Red Tape", Description = "Because there are interstellar governments, the products of their bureac racy must be distributed throughout their area of authority." });
                db.Add(new CargoType() { Type = "Samples", Description = "Newly discovered, created or manufactured items may be distributed to other workds for analysis or evaluation." });
                db.Add(new CargoType() { Type = "Scrap/Waste", Description = "The trash of some worlds can be a valued commodity on other worlds." });
                db.Add(new CargoType() { Type = "Uniques", Description = "Some items cannot be reproduced, adding value to the product." });
                db.Add(new CargoType() { Type = "Valuta", Description = "Sometimes shipments between worlds consist of money itself." });
            }

            if (db.Cargo.Count() == 0)
            {
                // classic
                db.Add(new Cargo() { D1 = 1, D2 = 1, BasePurchasePrice = 3000, Description = "Textiles", QtyDie = 3, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 1, D2 = 2, BasePurchasePrice = 7000, Description = "Polymers", QtyDie = 4, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 1, D2 = 3, BasePurchasePrice = 10000, Description = "Liquor", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 1, D2 = 4, BasePurchasePrice = 1000, Description = "Wood", QtyDie = 2, Multiplier = 10, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 1, D2 = 5, BasePurchasePrice = 20000, Description = "Crystals", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 1, D2 = 6, BasePurchasePrice = 1000000, Description = "Radioactives", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });

                db.Add(new Cargo() { D1 = 2, D2 = 1, BasePurchasePrice = 500, Description = "Steel", QtyDie = 4, Multiplier = 10, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 2, D2 = 2, BasePurchasePrice = 2000, Description = "Copper", QtyDie = 2, Multiplier = 10, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 2, D2 = 3, BasePurchasePrice = 1000, Description = "Aluminum", QtyDie = 5, Multiplier = 10, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 2, D2 = 4, BasePurchasePrice = 9000, Description = "Tin", QtyDie = 3, Multiplier = 10, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 2, D2 = 5, BasePurchasePrice = 70000, Description = "Silver", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 2, D2 = 6, BasePurchasePrice = 200000, Description = "Special ALloys", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });

                db.Add(new Cargo() { D1 = 3, D2 = 1, BasePurchasePrice = 10000, Description = "Petrochemicals", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 3, D2 = 2, BasePurchasePrice = 300, Description = "Grain", QtyDie = 8, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 3, D2 = 3, BasePurchasePrice = 1500, Description = "Meat", QtyDie = 4, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 3, D2 = 4, BasePurchasePrice = 6000, Description = "Spices", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 3, D2 = 5, BasePurchasePrice = 1000, Description = "Fruit", QtyDie = 2, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 3, D2 = 6, BasePurchasePrice = 100000, Description = "Pharmaceuticals", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });

                db.Add(new Cargo() { D1 = 4, D2 = 1, BasePurchasePrice = 1000000, Description = "Gems", QtyDie = 1, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 4, D2 = 2, BasePurchasePrice = 30000, Description = "Firearms", QtyDie = 2, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 4, D2 = 3, BasePurchasePrice = 30000, Description = "Ammunition", QtyDie = 2, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 4, D2 = 4, BasePurchasePrice = 10000, Description = "Blades", QtyDie = 2, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 4, D2 = 5, BasePurchasePrice = 10000, Description = "Tools", QtyDie = 2, Multiplier = 1, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 4, D2 = 6, BasePurchasePrice = 50000, Description = "Body Armor", QtyDie = 2, Multiplier = 1, IsSingleUnits = false });

                db.Add(new Cargo() { D1 = 5, D2 = 1, BasePurchasePrice = 1000000, Description = "Aircraft", QtyDie = 1, Multiplier = 1, IsSingleUnits = true });
                db.Add(new Cargo() { D1 = 5, D2 = 2, BasePurchasePrice = 6000000, Description = "Air/raft", QtyDie = 2, Multiplier = 1, IsSingleUnits = true });
                db.Add(new Cargo() { D1 = 5, D2 = 3, BasePurchasePrice = 10000000, Description = "Computers", QtyDie = 2, Multiplier = 1, IsSingleUnits = true });
                db.Add(new Cargo() { D1 = 5, D2 = 4, BasePurchasePrice = 3000000, Description = "All Terrain Vehicles", QtyDie = 2, Multiplier = 1, IsSingleUnits = true });
                db.Add(new Cargo() { D1 = 5, D2 = 5, BasePurchasePrice = 7000000, Description = "Armored Vehicles", QtyDie = 2, Multiplier = 1, IsSingleUnits = true });
                db.Add(new Cargo() { D1 = 5, D2 = 6, BasePurchasePrice = 150000, Description = "Farm Machinery", QtyDie = 2, Multiplier = 1, IsSingleUnits = true });

                db.Add(new Cargo() { D1 = 6, D2 = 1, BasePurchasePrice = 100000, Description = "Electronic Parts", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 6, D2 = 2, BasePurchasePrice = 70000, Description = "Mechanical Parts", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 6, D2 = 3, BasePurchasePrice = 250000, Description = "Cybernetic Parts", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 6, D2 = 4, BasePurchasePrice = 150000, Description = "Computer Parts", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 6, D2 = 5, BasePurchasePrice = 750000, Description = "Machine Tools", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
                db.Add(new Cargo() { D1 = 6, D2 = 6, BasePurchasePrice = 400000, Description = "Vacc Suites", QtyDie = 1, Multiplier = 5, IsSingleUnits = false });
            }

            if (db.ShipClasses.Count() == 0)
            {
                db.Add(new ShipClass() { Cargo = 200, dTons = 400, Fuel = 50, HGClass = "R", HighPassage = 0, Jump = 1, LowPassage = 9, Man = 1, MidPassage = 8, Name = "Subsidized Merchant", Power = 1});
                db.Add(new ShipClass() { Cargo = 46, dTons = 200, Fuel = 50, HGClass = "A2", HighPassage = 0, Jump = 2, LowPassage = 4, Man = 2, MidPassage = 7, Name = "Far Trader", Power = 2});
                db.Add(new ShipClass() { Cargo = 23, dTons = 100, Fuel = 30, HGClass = "J", HighPassage = 0, Jump = 2, LowPassage = 0, Man = 2, MidPassage = 0, Name = "Seeker", Power = 2});
                db.Add(new ShipClass() { Cargo = 82, dTons = 200, Fuel = 42, HGClass = "A1", HighPassage = 0, Jump = 1, LowPassage = 10, Man = 1, MidPassage = 6, Name = "Beowulf", Power = 1});
            }

            if (db.Starports.Count() == 0)
            {
                db.Add(new Starport() { Class = 'A', Quality = "Excellent", Repairs = "Overhaul", hasRefinedFuel = true, hasUnrefinedFuel = true, Downport = "Yes", Yards = "Can build starships", isStarport = true });
                db.Add(new Starport() { Class = 'B', Quality = "Good", Repairs = "Overhaul", hasRefinedFuel = true, hasUnrefinedFuel = true, Downport = "Yes", Yards = "Can build spaceships", isStarport = true });
                db.Add(new Starport() { Class = 'C', Quality = "Routine", Repairs = "Major Damage", hasRefinedFuel = false, hasUnrefinedFuel = true, Downport = "Yes", Yards = "None", isStarport = true });
                db.Add(new Starport() { Class = 'D', Quality = "Poor", Repairs = "Minor Damage", hasRefinedFuel = false, hasUnrefinedFuel = true, Downport = "Yes", Yards = "None", isStarport = true });
                db.Add(new Starport() { Class = 'E', Quality = "Frontier", Repairs = "No", hasRefinedFuel = false, hasUnrefinedFuel = false, Downport = "Beacon", Yards = "None", isStarport = true });
                db.Add(new Starport() { Class = 'X', Quality = "None", Repairs = "No", hasRefinedFuel = false, hasUnrefinedFuel = false, Downport = "No", Yards = "None", isStarport = true });
                db.Add(new Starport() { Class = 'F', Quality = "Good", Repairs = "Minor Damage", hasRefinedFuel = false, hasUnrefinedFuel = true, Downport = "Yes", Yards = "None", isStarport = false });
                db.Add(new Starport() { Class = 'G', Quality = "Poor", Repairs = "Superficial Damage", hasRefinedFuel = false, hasUnrefinedFuel = true, Downport = "Yes", Yards = "None", isStarport = false });
                db.Add(new Starport() { Class = 'H', Quality = "Basic", Repairs = "None", hasRefinedFuel = false, hasUnrefinedFuel = false, Downport = "Beacon", Yards = "None", isStarport = false });
                db.Add(new Starport() { Class = 'Y', Quality = "None", Repairs = "None", hasRefinedFuel = false, hasUnrefinedFuel = false, Downport = "None", Yards = "None", isStarport = false });
            }

            if (db.TradeGoods.Count() == 0)
            {
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Bulk Protein" });
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Bulk Carbs" });
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Bulk Fats" });
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Bulk Pharma" });
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Livestock" });
                db.Add(new TradeGood() { TradeCode = "Ag", Description = "Feedstock" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "Bulk nitrarates" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "Bulk carbon" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "Bulk iron" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "Bulk copper" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "radiactive ores" });
                db.Add(new TradeGood() { TradeCode = "As", Description = "Bulk ices" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Bulk Abarasives" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Bulk Gases" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Bulk Minerals" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Bulk Precipitates" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Exotic Fauna" });
                db.Add(new TradeGood() { TradeCode = "Na", Description = "Exotic Flora" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Electronics" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Photonics" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Magnetics" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Fluidics" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Polymerics" });
                db.Add(new TradeGood() { TradeCode = "In", Description = "Gravatics" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Bulk Nutrients" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Bulk Fibers" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Bulk Organics" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Bulk Minerals" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Bulk Textiles" });
                db.Add(new TradeGood() { TradeCode = "Po", Description = "Exotic Flora" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Bulk Foodstuffs" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Bulk Protein" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Bulk Carbs" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Bulk Fats" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Exotic Flora" });
                db.Add(new TradeGood() { TradeCode = "Ri", Description = "Exotic Fauna" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Bulk Dusts" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Bulk Minerals" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Bulk Metals" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Radioactive Ores" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Bulk Particulates" });
                db.Add(new TradeGood() { TradeCode = "Va", Description = "Emphemerals" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Coinage" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Currency" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Money Cards" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Gold" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Silver" });
                db.Add(new TradeGood() { TradeCode = "Cp", Description = "Platinum" });
            }
            db.SaveChanges();
            //db.SaveChangesAsync();
        }
    }
}
