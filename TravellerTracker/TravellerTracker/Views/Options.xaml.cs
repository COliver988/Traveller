using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveller.Support;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Traveller.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }

        private async void btnReloadSectors(object sender, RoutedEventArgs e)
        {
            var allSectors = from s in App.DB.Sectors select s;
            App.DB.Sectors.RemoveRange(allSectors);
            TravellerMapAPI api = new TravellerMapAPI();
            foreach (string era in App.Eras)
            {
                TravellerMapUniverse.SectorList sectors = await api.loadUniverse(era);
                foreach (var sector in sectors.Sectors)
                    App.DB.Add(new Sector() { Name = sector.FirstName, Milieu = sector.Milieu, Tags = sector.Tags });
            }

            // clear out the worlds
            var allWorlds = from s in App.DB.Worlds select s;
            App.DB.RemoveRange(allWorlds);
            var allShips = from s in App.DB.Ships select s;
            // clear out the ship world IDs
            foreach (Ship ship in allShips)
            {
                ship.WorldID = 0;
                ship.SectorID = 0;
            }
            App.DB.SaveChangesAsync();
        }
    }
}
