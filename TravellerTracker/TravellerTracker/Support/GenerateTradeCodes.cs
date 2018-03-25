using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using TravellerTracker;

namespace Traveller.Support
{
    public class GenerateTradeCodes
    {
        static public List<TradeClassification> GetCodes(World world)
        {
            string Size = world.UWP[1].ToString();
            string Atmosphere = world.UWP[2].ToString();
            string Hydro = world.UWP[3].ToString();
            string Population = world.UWP[4].ToString();
            string Gov = world.UWP[5].ToString();
            string Law = world.UWP[6].ToString();

            List<TradeClassification> results = App.DB.TradeClassifications.Where(x => x.Sizes.Length == 0 || x.Sizes.Contains(Size)).
                Where(x => x.Atmospheres.Length == 0 || x.Atmospheres.Contains(Atmosphere)).
                Where(x => x.Hydro.Length == 0 || x.Hydro.Contains(Hydro)).
                Where(x => x.Pop.Length == 0 || x.Pop.Contains(Population)).
                Where(x => x.Gov.Length == 0 || x.Gov.Contains(Gov)).
                Where(x =>x.Law.Length == 0 || x.Law.Contains(Law)).Where(x => x.IsManuallyAssigned == false).ToList();
            List<int> ids = App.DB.WorldTCs.Where(x => x.WorldID == world.WorldID).Select(y => y.TradeClassificationID).ToList();
            results.AddRange(App.DB.TradeClassifications.Where(x => ids.Contains(x.TradeClassificationID)).ToList());
            return results;
        }

        // manual-only codes
        static public List<TradeClassification> GetManualCodes(string UWP)
        {
            string Size = UWP[1].ToString();
            string Atmosphere = UWP[2].ToString();
            string Hydro = UWP[3].ToString();
            string Population = UWP[4].ToString();
            string Gov = UWP[5].ToString();
            string Law = UWP[6].ToString();

            List<TradeClassification> results = App.DB.TradeClassifications.Where(x => x.Sizes.Length == 0 || x.Sizes.Contains(Size)).
                Where(x => x.Atmospheres.Length == 0 || x.Atmospheres.Contains(Atmosphere)).
                Where(x => x.Hydro.Length == 0 || x.Hydro.Contains(Hydro)).
                Where(x => x.Pop.Length == 0 || x.Pop.Contains(Population)).
                Where(x => x.Gov.Length == 0 || x.Gov.Contains(Gov)).
                Where(x =>x.Law.Length == 0 || x.Law.Contains(Law)).Where(y => y.IsManuallyAssigned == true).ToList();
            return results;
        }
    }
}
