using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveller.Models;

namespace TravellerTracker.Support
{
    public class LoadCargo
    {
            Utilities util = new Utilities();
        public List <Cargo> findCargo(World origin, Ship ship)
        {
            List<Cargo> results = new List<Cargo>();

            switch (ship.TravellerVersionID)
            {
                case 1:
                    results = findClassicCargo(origin, ship);
                    break;
                case 2:
                    results = findMongooseCargo(origin, ship);
                    break;
                default:
                    results = findT5Cargo(origin, ship);
                    break;
            }

            return results;
        }

        private List<Cargo> findT5Cargo(World origin, Ship ship)
        {
            throw new NotImplementedException();
        }

        private List<Cargo> findMongooseCargo(World origin, Ship ship)
        {
            throw new NotImplementedException();
        }

        private List<Cargo> findClassicCargo(World origin, Ship ship)
        {
            int d1 = util.d6();
            if (origin.Pop >= 9) d1 = d1 + 1;
            if (origin.Pop <= 5) d1 = d1 - 1;
            if (d1 > 6) d1 = 6;
            if (d1 < 1) d1 = 1;
            int d2 = util.d6();
            List<Cargo> results = new List<Cargo>();
            results.Add(App.DB.Cargo.Where(x => x.D1 == d1 && x.D2 == d2).First());
            return results;

            // and
        }
    }
}
