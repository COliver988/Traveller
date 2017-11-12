using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;

namespace Traveller.Support
{
    public class LoadCargo
    {
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

        private List<Cargo> findT5Cargo(Ship ship)
        {
            throw new NotImplementedException();
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
            results.Add(c);

            // load up regular cargo; passengers
            // requires the list of planet destinations
            return results;

            // and
        }
    }
}
