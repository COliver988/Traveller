using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using TravellerTracker.Support;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TravellerTracker.Views
{
    public sealed partial class ShipTracker : Page
    {
        public Ship ship { get; set; }
        List<World> jumpWorlds;
        public List<CargoAvailable> pax { get; set; }
        Utilities util = new Utilities();

        public ShipTracker(int shipID)
        {
            this.InitializeComponent();
            ship = App.DB.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            this.DataContext = this;
            webView.Navigate(ship.theJumpMapURL);
            jumpWorlds = ship.theWorld.JumpRange(ship.theClass.Jump);
            lstJumpList.ItemsSource = jumpWorlds;
            lstWorldLog.ItemsSource = ship.theWorld.theLog;
            refresh();
        }

        private void refresh()
        {
            lstLog.ItemsSource = ship.theLog;
            if (ship.theWorld.WorldImage != null)
                showWorldImage(ship.theWorld.WorldImage);
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
            int fuelNeeded = ship.theClass.Fuel - ship.Fuel;
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
            else if (ship.theWorld.thePort.hasRefinedFuel)
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
                        ship.Fuel = ship.theClass.Fuel;
                        break;
                    case ContentDialogResult.Secondary:
                        ship.Credits -= unrefinedCost;
                        ship.Fuel = ship.theClass.Fuel;
                        break;
                    default:
                        break;
                }
            }
            else if (ship.theWorld.thePort.hasUnrefinedFuel)
            {
                ship.Credits -= unrefinedCost;
                ship.Fuel = ship.theClass.Fuel;
            }
            else
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("Fuel is unavailable at this port.");
            }
            App.DB.SaveChangesAsync();
        }

        private void btnHighPax(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ship.theClass.HighPassage == 0 || ship.HighPaxAvail == 0)
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
                ship.HighPaxCarried = ship.theClass.HighPassage;
            }
            App.DB.SaveChangesAsync();
        }

        private void btnJump(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            World destination = btn.DataContext as World;
            int distance = util.calcDistance(ship.theWorld.Hex, destination.Hex);
            if (ship.Fuel < distance * ship.theClass.FuelPerParsec)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("You do not have sufficient fuel for the jump!");
                return;
            }
            // add a log record
            StringBuilder sb = new StringBuilder($"Jumping from {ship.theWorld.Name} to {destination.Name} carrying {ship.CargoCarried} tons of cargo.");
            if (ship.HighPaxCarried > 0)
                sb.Append($"{ship.HighPaxCarried} high passengers.");
            if (ship.MidPaxCarried > 0)
                sb.Append($" {ship.MidPaxCarried} mid passengers.");
            if (ship.LowPaxCarried > 0)
                sb.Append($" {ship.LowPaxCarried} low passengers.");
            App.DB.Add(new ShipLog() { Day = ship.Day, Year = ship.Year, ShipId = ship.ShipId, Log = sb.ToString().Replace("  ", " ") });
            ImperialDates id = new ImperialDates(ship.Day, ship.Year);
            id.addDays(7);
            ship.Day = id.Day;
            ship.Year = id.Year;
            ship.WorldID = destination.WorldID;
            ship.HighPaxCarried = 0;
            ship.LowPaxCarried = 0;
            ship.MidPaxCarried = 0;
            ship.Fuel -= ship.theClass.FuelPerParsec;
            App.DB.SaveChangesAsync();
        }

        private void btnAddLog(object sender, RoutedEventArgs e)
        {
            AddLog al = new AddLog();
            al.addLog(ship, ship.theWorld.WorldID);
            lstWorldLog.ItemsSource = ship.theWorld.theLog;
        }

        private async void btnAddImage(object sender, RoutedEventArgs e)
        {
            FileOpenPicker op = new FileOpenPicker();
            op.ViewMode = PickerViewMode.Thumbnail;
            op.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            op.FileTypeFilter.Add(".jpg");
            op.FileTypeFilter.Add(".jpeg");
            op.FileTypeFilter.Add(".png");
            op.FileTypeFilter.Add(".bmp");
            StorageFile file = await op.PickSingleFileAsync();
            if (file != null)
            {
                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();
                    var byteArray = new byte[readStream.Length];
                    await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                    ship.theWorld.WorldImage = byteArray;
                }
                TravellerTracker.App.DB.SaveChanges();
                showWorldImage(ship.theWorld.WorldImage);
            }
        }

        private async void showWorldImage(byte[] image)
        {
            BitmapImage returnImage = new BitmapImage();
            using (InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(image);
                    await writer.StoreAsync();
                    await returnImage.SetSourceAsync(randomAccessStream);
                }
            }
            imageWorld.Source = returnImage;
        }

        private void btnBulkCargo(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CargoAvailable cargo = (CargoAvailable)btn.DataContext;
            SelectBulkCargo sbc = new SelectBulkCargo();
            sbc.Select(ship, cargo);
        }
    }
}
