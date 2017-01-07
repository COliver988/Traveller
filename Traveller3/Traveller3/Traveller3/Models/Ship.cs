using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using static Traveller3.Models.Support;

namespace Traveller3.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Ship
    {
        public Ship() { }

        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int dTons { get; set; }
        [JsonProperty]
        public int CargoCapacity { get; set; }
        [JsonProperty]
        public int CargoCarried { get; set; }
        [JsonProperty]
        public int AvailableCargoSpace { get { return CargoCapacity - CargoCarried;  }  }
        [JsonProperty]
        public int FuelPerJump { get; set; }                // fuel per jump
        [JsonProperty]
        public Versions Version { get; set; }                 // version: CT / T5 / MT (Mongoose)
        [JsonProperty]
        public ImperialCalendar ShipDate { get; set; }
        [JsonProperty]
        public string SectorFile { get; set; }
        [JsonProperty]
        public string CurrentSystem { get { if (Location != null) return string.Format("{0} {1}", Location.hex, Location.name); else return "";  } }
        [JsonProperty]
        public string CurrentSEC { get; set; }
        public World Location { get; set; }

        public async void Save()
        {
            if (ShipDate == null)
                ShipDate = new ImperialCalendar(1, 1105);
            string json = JsonConvert.SerializeObject(this);
            FileSavePicker picker = new FileSavePicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = Name + ".json";
            picker.FileTypeChoices.Add("JSON", new List<string>() {  ".json"});
            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
            }
        }

        public async Task<Ship> load()
        {
            Ship ship = new Ship();
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".json");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string data = await Windows.Storage.FileIO.ReadTextAsync(file);
                try
                {
                    ship = JsonConvert.DeserializeObject<Ship>(data);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return ship;
        }
    }
}
