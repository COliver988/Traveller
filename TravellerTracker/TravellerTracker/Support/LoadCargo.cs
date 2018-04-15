using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using TravellerTracker;

namespace Traveller.Support
{
    public class LoadCargo
    {
        Random rnd = new Random();
        Utilities util = new Utilities();
        public List<Cargo> findCargo(Ship ship)
        {
            List<Cargo> results = new List<Cargo>();

            switch (ship.theVersion.Name)
            {
                case "Classic":
                    results = findClassicCargo(ship);
                    break;
                case "Mongoose":
                    results = findMongooseCargo(ship);
                    break;
                default:
                    results = findT5Cargo(ship);
                    break;
            }

            return results;
        }

        // T5 code = TL - trade codes Cr<cost>
        private List<Cargo> findT5Cargo(Ship ship)
        {
            List<Cargo> results = new List<Cargo>();
            Cargo c = new Cargo() { isSpeculative = true, CargoCode = $"{ship.theWorld.Tech} - ", BasePurchasePrice = 3000 };
            foreach (TradeClassification tc in ship.theWorld.TradeCodes)
            {
                c.CargoCode += tc.Classification + " ";
                c.BasePurchasePrice += tc.BuyingAdjustment;
            }
            if (c.Description == null)
            {
                if (ship.theWorld.TradeCodes.Count() > 0)
                {
                    c.Description = T5LoadDescription(ship.theWorld.TradeCodes[rnd.Next(0, ship.theWorld.TradeCodes.Count())].Classification);
                }
                else
                    c.Description = App.DB.TradeGoods.First().Description;
            }
            c.BasePurchasePrice += ship.theWorld.Tech * 100;
            c.CargoCode += $" Cr{c.BasePurchasePrice}";
            results.Add(c);
            return results;
        }

        private string T5LoadDescription(string tradecode)
        {
            TradeGood[] tradegoods = App.DB.TradeGoods.Where(x => x.TradeCode == tradecode).ToArray();
            if (tradegoods.Length > 0)
                return tradegoods[rnd.Next(0, tradegoods.Length)].Description;
            else
                return "N/A";
        }

        private List<Cargo> findMongooseCargo(Ship ship)
        {
            throw new NotImplementedException();
        }

        private List<Cargo> findClassicCargo(Ship ship)
        {
            int d1 = util.d6();
            if (ship.theWorld.Pop >= 9) d1 = d1 + 1;
            if (ship.theWorld.Pop <= 5) d1 = d1 - 1;
            if (d1 > 6) d1 = 6;
            if (d1 < 1) d1 = 1;
            int d2 = util.d6();
            List<Cargo> results = new List<Cargo>();
            Cargo c = TravellerTracker.App.DB.Cargo.Where(x => x.D1 == d1 && x.D2 == d2).First();
            c.dTons = c.QtyDie * util.d6();
            c.dTons *= c.Multiplier;
            c.isSpeculative = true;
            if (c.CargoCode == null)
            {
                BITSCargo bc = new BITSCargo();
                c.CargoCode = bc.GenerateBITS(c.dTons);
            }
            results.Add(c);

            // load up regular cargo; passengers
            // requires the list of planet destinations
            return results;
        }
    }
}
