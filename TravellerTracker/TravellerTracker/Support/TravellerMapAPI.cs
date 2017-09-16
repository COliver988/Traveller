using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traveller.Models;
using Newtonsoft.Json;

namespace Traveller.Support
{
    public class TravellerMapAPI
    {

        public async Task<TravellerMapUniverse.SectorList> loadUniverse(string milieu)
        {
            Uri uriUniverse = new Uri(string.Format("https://travellermap.com/data?era={0}&requireData=1", milieu));
            TravellerMapUniverse.SectorList results = new TravellerMapUniverse.SectorList();
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(uriUniverse);
                try
                {
                    results = JsonConvert.DeserializeObject<TravellerMapUniverse.SectorList>(result);
                }
                catch (Exception ex)
                {
                    var a = ex;
                    throw;
                }
            }
            return results;
        }
    }
}
