using System.Collections.Generic;
using Traveller.Models;

namespace TravellerTracker.Support
{
    public class SelectBulkCargo
    {
        public List<BulkCargo> Select(Ship ship, CargoAvailable cargo)
        {
            List<BulkCargo> bulkCargoes = new List<BulkCargo>();
            if (cargo.CargoMajor.Count > 0)
                bulkCargoes.AddRange(AddList("Major", cargo.CargoMajor));
            if (cargo.CargoMinor.Count > 0)
                bulkCargoes.AddRange(AddList("Minor", cargo.CargoMinor));
            if (cargo.CargoIncidental.Count > 0)
                bulkCargoes.AddRange(AddList("Incidental", cargo.CargoIncidental));
            return bulkCargoes;
        }

        private List<BulkCargo> AddList(string type, List<int> tons)
        {
            BITSCargo bc = new BITSCargo();
            List<BulkCargo> results = new List<BulkCargo>();
            for (int i = 0; i < tons.Count; i++)
                results.Add(new BulkCargo() { CargoType = type, CargoCode = bc.GenerateBITS(tons[i]), dTons = tons[i] });
            return results;
        }
    }
}
