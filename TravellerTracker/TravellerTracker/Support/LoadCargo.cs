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
            throw new NotImplementedException();
        }
    }
}
