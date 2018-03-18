using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using TravellerTracker;

namespace Traveller.Support
{
    public class GenerateTradeCodes
    {
        static public List<TradeClassification> GetCodes(string UWP)
        {
            string Size = UWP[1].ToString();
            string Atmosphere = UWP[2].ToString();
            string Hydro = UWP[3].ToString();
            string Population = UWP[4].ToString();
            string Gov = UWP[5].ToString();
            string Law = UWP[6].ToString();

            List<TradeClassification> results = App.DB.TradeClassifications.Where(x => x.Sizes.Length == 0 || x.Sizes.Contains(Size)).
                Where(x => x.Atmospheres.Length == 0 || x.Atmospheres.Contains(Atmosphere)).
                Where(x => x.Hydro.Length == 0 || x.Hydro.Contains(Hydro)).ToList().
                Where(x => x.Pop != null || x.Pop.Contains(Population)).
                Where(x => x.Gov.Length == 0 || x.Gov.Contains(Gov)).
                Where(x =>x.Law.Length == 0 || x.Law.Contains(Law)).ToList();
            return results;
        }
    }
}
