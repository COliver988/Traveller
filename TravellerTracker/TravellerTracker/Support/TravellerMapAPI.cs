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
        Uri uriUniverse = new Uri("https://travellermap.com/data?era=M1105");

        public async Task<TravellerMapUniverse.SectorList> loadUnivrerse()
        {
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
