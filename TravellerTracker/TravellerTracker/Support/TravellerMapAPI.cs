using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Traveller.Models;
using TravellerTracker;
using TravellerTracker.Models;

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

        public async Task<List<World>> loadWorlds(string milieu, string sector)
        {
            // if we've already got this sector we should have loaded the worlds as well
            Sector DBsector;
            using (var db = new TravellerContext())
            {
                DBsector = db.Sectors.Where(x => x.Name == sector && x.Milieu == milieu).First();
                if (DBsector != null)
                {
                    List<World> worlds = db.Worlds.Where(x => x.SectorID == DBsector.SectorID).ToList();
                    if (worlds.Count > 0)
                        return worlds;
                }
                else
                {
                    DBsector = new Sector();
                    DBsector.Name = sector;
                    DBsector.Milieu = milieu;
                    await db.SaveChangesAsync();
                }
            }


            List<World> results = new List<World>();
            Uri uriWorlds = new Uri(string.Format("https://travellermap.com/api/sec?sector={0}&type=SecondSurvey&milieu={1}", sector, milieu));
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(uriWorlds);
                string[] lines = result.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
                try
                {
                    foreach (var line in lines)
                    {
                        if (line.Length > 0 && Char.IsNumber(line[0]))
                        {
                            results.Add(new World(line, DBsector.SectorID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    var a = ex;
                    throw;
                }
            }
            SaveToDB(results, DBsector.SectorID);
            return results;
        }

        private async void SaveToDB(List<World> results, int sectorID)
        {
            foreach (World world in results)
            {
                world.SectorID = sectorID;
                App.DB.Add(world);
            }
            await App.DB.SaveChangesAsync();
        }

        public Uri JumpMapURL(World w, int jump)
        {
            string sector = App.DB.Sectors.Where(x => x.SectorID == w.SectorID).FirstOrDefault().Name.Replace(" ", "%20");
            return new Uri(string.Format("Https://travellermap.com/api/jumpmap?sector={0}&hex={1}&jump={2}", sector, w.Hex, jump));
        }
    }
}
