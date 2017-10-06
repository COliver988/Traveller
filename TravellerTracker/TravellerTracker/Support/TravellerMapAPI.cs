using System;
using System.Collections;
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

        public async Task<List<Worlds>> loadWorlds(string milieu, string sector)
        {
            List<Worlds> results = new List<Worlds>();
            Uri uriWorlds = new Uri(string.Format("https://travellermap.com/api/sec?sector={0}&type=SecondSurvey&milieu={1}", sector, milieu));
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(uriWorlds);
                string[] lines = result.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
                try
                {
                    foreach (var line in lines)
                    {
                        if (line.Length > 0 && Char.IsNumber(line[0] ))
                        {
                            results.Add(new Worlds(line));
                        }
                    }
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
