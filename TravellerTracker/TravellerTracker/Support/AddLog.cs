using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TravellerTracker.Support
{
    public class AddLog
    {
        public async void addLog(Ship ship, ShipCargo cargo, bool isLoading)
        {
            ShipLog log = new ShipLog(ship);
            string process = "loaded";
            if (!isLoading)
                process = "unloaded";
            log.Log = $"Cargo {process}: {cargo.CargoCode} at {ship.theWorld.Name}; destination {cargo.DestinationWorld.Name}";
            TravellerTracker.App.DB.Add(log);
            await TravellerTracker.App.DB.SaveChangesAsync();
        }

        public async void addLog(Ship ship, int? worldID)
        {
            TextBox txt = new TextBox { Width = 400, Height = 200 };
            txt.TextWrapping = TextWrapping.Wrap;
            txt.AcceptsReturn = true;
            var conDlg = new Windows.UI.Xaml.Controls.ContentDialog
            {
                Title = string.Format("Enter new log for {0}-{1}", ship.Day, ship.Year),
                PrimaryButtonText = "Add Log",
                SecondaryButtonText = "Cancel",
                Content = txt
            };
            var content = await conDlg.ShowAsync();
            switch (content)
            {
                case ContentDialogResult.None:
                    break;
                case ContentDialogResult.Primary:
                    ShipLog log = new ShipLog(ship);
                    if (worldID > 0)
                        log.WorldID = (int)worldID;
                    log.Log = txt.Text;
                    TravellerTracker.App.DB.Add(log);
                    await TravellerTracker.App.DB.SaveChangesAsync();
                    break;
                case ContentDialogResult.Secondary:
                    break;
                default:
                    break;
            }
        }
    }
}
