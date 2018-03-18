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

            List<TradeClassification> results = App.DB.TradeClassifications.Where(x => x.Sizes.Length == 0 || x.Sizes.Contains(Size) && x.IsManuallyAssigned == false).
                Where(x => x.Atmospheres.Length == 0 || x.Atmospheres.Contains(Atmosphere) && x.IsManuallyAssigned == false).
                Where(x => x.Hydro.Length == 0 || x.Hydro.Contains(Hydro) && x.IsManuallyAssigned == false).
                Where(x => x.Pop != null || x.Pop.Contains(Population) && x.IsManuallyAssigned == false).
                Where(x => x.Gov.Length == 0 || x.Gov.Contains(Gov) && x.IsManuallyAssigned == false).
                Where(x =>x.Law.Length == 0 || x.Law.Contains(Law) && x.IsManuallyAssigned == false).ToList();
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

            List<TradeClassification> results = App.DB.TradeClassifications.Where(x => x.Sizes.Length == 0 || x.Sizes.Contains(Size) && x.IsManuallyAssigned == true).
                Where(x => x.Atmospheres.Length == 0 || x.Atmospheres.Contains(Atmosphere) && x.IsManuallyAssigned == true).
                Where(x => x.Hydro.Length == 0 || x.Hydro.Contains(Hydro) && x.IsManuallyAssigned == true).
                Where(x => x.Pop != null || x.Pop.Contains(Population) && x.IsManuallyAssigned == true).
                Where(x => x.Gov.Length == 0 || x.Gov.Contains(Gov) && x.IsManuallyAssigned == true).
                Where(x =>x.Law.Length == 0 || x.Law.Contains(Law) && x.IsManuallyAssigned == true).ToList();
            return results;
        }
    }
}
