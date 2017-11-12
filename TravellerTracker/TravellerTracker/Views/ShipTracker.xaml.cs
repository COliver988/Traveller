using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using Windows.UI.Xaml.Controls;

namespace TravellerTracker.Views
{
    public sealed partial class ShipTracker : Page
    {
        public Ship ship { get; set; } 

        public ShipTracker(int shipID)
        {
            this.InitializeComponent();
            ship = App.DB.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            this.DataContext = this;
            webView.Navigate (ship.theJumpMapURL );
            refresh();
        }

        private void refresh()
        {
            lstLog.ItemsSource = ship.theLog;
        }

        private void btnLoadCargo(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ImperialDates id = new ImperialDates(ship.Day, ship.Year);
            id.addDays(7);
            ship.Year = id.Year;
            ship.Day = id.Day;
            LoadCargo load = new LoadCargo();
            List<Cargo> cargo = load.findCargo(ship);
            spSpecTrade.DataContext = cargo.Where(x => x.isSpeculative == true).FirstOrDefault();
        }

        private void btnPrice(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
