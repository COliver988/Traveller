using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using Windows.UI.Xaml.Controls;

namespace TravellerTracker.Views
{
    public sealed partial class ShipTracker : Page
    {
        public Ship ship { get; set; }
        List<World> jumpWorlds;
        public List<CargoAvailable> pax { get; set; }

        public ShipTracker(int shipID)
        {
            this.InitializeComponent();
            ship = App.DB.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            this.DataContext = this;
            webView.Navigate(ship.theJumpMapURL);
            jumpWorlds = ship.theWorld.JumpRange(ship.theclass.Jump);
            lstJumpList.ItemsSource = jumpWorlds;
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
            App.DB.SaveChangesAsync();
            LoadCargo load = new LoadCargo();
            List<Cargo> cargo = load.findCargo(ship);
            spSpecTrade.DataContext = cargo.Where(x => x.isSpeculative == true).FirstOrDefault();
            pax = new List<CargoAvailable>();
            foreach (World dest in jumpWorlds)
                pax.Add(new CargoAvailable(ship.theWorld, dest));
            lstPax.DataContext = pax;
        }

        private void btnPrice(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private async void btnRefuel(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int fuelNeeded = ship.theclass.Fuel - ship.Fuel;
            int unrefinedCost = fuelNeeded * 100;
            int refinedCost = fuelNeeded * 500;
            if (ship.Credits <= unrefinedCost)
            {
                TextBlock tb = new TextBlock() { Text = string.Format("Insufficient funds to purchase fuel: Cr{0} required for unrefined fuel, \nCr{1} required for refined fuel.\nYou only have Cr{2}", unrefinedCost, refinedCost, ship.Credits) };
                var dialog = new Windows.UI.Xaml.Controls.ContentDialog
                {
                    Title = "Warning",
                    Content = tb,
                    PrimaryButtonText = "OK"
                };
                var x = await dialog.ShowAsync();
            }
            else if (ship.theWorld.Starport == 'A' || ship.theWorld.Starport == 'B')
            {
                var conDlg = new Windows.UI.Xaml.Controls.ContentDialog
                {
                    Title = "Please select a fuel grade",
                    PrimaryButtonText = string.Format("Refined Fuel ({0} credits)", refinedCost),
                    SecondaryButtonText = string.Format("Unrefined Fuel ({0} credits)", unrefinedCost)
                };
                var content = await conDlg.ShowAsync();
                switch (content)
                {
                    case ContentDialogResult.None:
                        break;
                    case ContentDialogResult.Primary:
                        ship.Credits -= refinedCost;
                        ship.Fuel = ship.theclass.Fuel;
                        break;
                    case ContentDialogResult.Secondary:
                        ship.Credits -= unrefinedCost;
                        ship.Fuel = ship.theclass.Fuel;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ship.Credits -= unrefinedCost;
                ship.Fuel = ship.theclass.Fuel;
            }
            App.DB.SaveChangesAsync();
        }

        private void btnHighPax(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ship.theclass.HighPassage == 0 || ship.HighPaxAvail == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("You do not have any high passage capacity.");
                return;
            }
            Button btn = sender as Button;
            CargoAvailable ca = btn.DataContext as CargoAvailable;
            if (ca.HighPassage == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("There are no high passage passengers available.");
                return;
            }
            if (ship.HighPaxAvail >= ca.HighPassage)
            {
                ship.HighPaxCarried = ca.HighPassage;
                ship.Credits += ca.HighPassage * 10000;
            }
            else
            {
                ship.Credits += ship.HighPaxAvail * 10000;
                ship.HighPaxCarried = ship.theclass.HighPassage;
            }
            App.DB.SaveChangesAsync();
        }
    }
}
