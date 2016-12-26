using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Traveller3.Models
{
    public class Ship
    {
        public Ship() { }

        public string Name { get; set; }
        public int dTons { get; set; }
        public int CargoCapacity { get; set; }
        public int CargoCarried { get; set; }
        public int AvailableCargoSpace { get { return CargoCapacity - CargoCarried;  }  }
        public int FuelPerJump { get; set; }                // fuel per jump
        public World Location { get; set; }                 // current world location
        public ImperialCalendar ShipDate { get; set; }      // current date 

        public async void Save()
        {
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
