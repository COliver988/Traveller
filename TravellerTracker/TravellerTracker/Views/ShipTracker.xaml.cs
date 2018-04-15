using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using TravellerTracker.Support;
using TravellerTracker.UserControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace TravellerTracker.Views
{
    public sealed partial class ShipTracker : Page
    {
        public Ship ship { get; set; }
        List<World> jumpWorlds;
        public List<CargoAvailable> pax { get; set; }
        Utilities util = new Utilities();
        public ImperialDates date;
        public Sector sector { get; set; }
        public List<ShipClass> classes;
        int ID;

        public ShipTracker(int shipID)
        {
            this.InitializeComponent();
            ID = shipID;
            refresh();
        }

        private void refresh()
        {
            ship = null;
            ship = App.DB.Ships.Where(x => x.ShipId == ID).FirstOrDefault();
            if (ship.theJumpMapURL != null)
                webView.Navigate(ship.theJumpMapURL);
            comboVersions.ItemsSource = App.DB.TravellerVersions.ToList();
            if (ship.SectorID > 0)
            {
                sector = App.DB.Sectors.Where(x => x.SectorID == ship.SectorID).FirstOrDefault();
                loadWorlds();
                jumpWorlds = ship.theWorld.JumpRange(ship.theClass.Jump);
                lstJumpList.ItemsSource = jumpWorlds;
            }
            this.DataContext = this;
            lstLog.ItemsSource = ship.theLog;
            lstCargoCarried.ItemsSource = ship.theCargo;
            date = new ImperialDates(ship.Day, ship.Year);
            try
            {
                classes = App.DB.ShipClasses.ToList();
                cbClasses.ItemsSource = classes;
                cbClasses.SelectedItem = classes.Where(x => x.ShipClassID == ship.ShipClassID).FirstOrDefault();
            }
            catch (System.Exception)
            {
                ErrorHandling e = new ErrorHandling();
                e.showError("Ship class does not exist - please add a class");
            }
            if (ship.Era != null)
                loadSectorsForEra(ship.Era);
            else
                loadSectorsForEra("M1105");
            if (App.tmWorlds != null)
            {
                comboWorlds.ItemsSource = App.tmWorlds.OrderBy(x => x.Name);
                if (ship.WorldID > 0)
                    comboWorlds.SelectedItem = App.tmWorlds.Where(x => x.WorldID == ship.WorldID).First();
            }
            if (ship.theVersion != null)
                comboVersions.SelectedItem = ship.theVersion;
            comboEra.ItemsSource = App.Eras;
            comboEra.SelectedValue = ship.Era;
        }

        private void loadSectorsForEra(string era)
        {
            if (App.DB.Sectors.Where(x => x.Milieu == era).Count() == 0)
                if (era != null)
                {
                    TravellerMapAPI api = new TravellerMapAPI();
                    api.loadAppDB(era);
                }
            comboSectors.ItemsSource = App.DB.Sectors.Where(x => x.Milieu == ship.Era).OrderBy(o => o.Name).ToList();
            if (sector != null && sector.Name != null)
                comboSectors.SelectedItem = App.DB.Sectors.Where(x => x.Name == sector.Name).First();
        }

        private void btnLoadCargo(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ImperialDates id = new ImperialDates(ship.Day, ship.Year);
            id.addDays(ship.theVersion.DaysForCargoSearch);
            ship.Year = id.Year;
            ship.Day = id.Day;
            App.DB.SaveChangesAsync();
            LoadCargo load = new LoadCargo();
            List<Cargo> cargo = load.findCargo(ship);
            spSpecTrade.DataContext = cargo.Where(x => x.isSpeculative == true).FirstOrDefault();
            pax = new List<CargoAvailable>();
            foreach (World dest in jumpWorlds)
                pax.Add(new CargoAvailable(ship, dest));
            lstPax.DataContext = pax;
            refresh();
        }

        private void btnPrice(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Cargo cargo = spSpecTrade.DataContext as Cargo;
            SpecCargoSell sc = new SpecCargoSell() { IsSelling = false, shipCargo = new ShipCargo() { ShipID = ship.ShipId, CargoID = cargo.CargoID, dTons = cargo.dTons } };
            popCargo.Child = sc;
            showPopup();
        }

        private async void btnRefuel(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int fuelNeeded = ship.theClass.Fuel - ship.Fuel;
            if (fuelNeeded == 0) return;
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
            await App.DB.SaveChangesAsync();
            refresh();
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
            ship.Fuel -= ship.theClass.FuelPerParsec * distance;
            App.DB.Add(new ShipLog() { Day = ship.Day, Year = ship.Year, ShipId = ship.ShipId, Log = $"Arrived at {destination.Name.Trim()}" });
            foreach (ShipCargo item in ship.theCargo)
                if (item.DestinationWorld != null)
                    if (item.DestinationWorld.WorldID == destination.WorldID)
                        unloadCargo(item);
            App.DB.SaveChangesAsync();
            refresh();
        }

        private void btnBulkCargo(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CargoAvailable cargo = (CargoAvailable)btn.DataContext;
            SelectBulkCargo sbc = new SelectBulkCargo();
            BulkCargoSelector bulkCargo = new BulkCargoSelector();
            bulkCargo.Destination = cargo.world;
            bulkCargo.CargoList = sbc.Select(ship, cargo);
            bulkCargo.Ship = ship;
            popCargo.Child = bulkCargo;
            showPopup();
        }

        private void showPopup()
        {
            popCargo.HorizontalOffset = 50;
            popCargo.VerticalOffset = 50;
            popCargo.IsLightDismissEnabled = true;
            popCargo.IsOpen = true;
        }

        private void ptrShowWorld(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            showWorldPopup(ship.theWorld);
        }

        private void ptrShowWorldList(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            TextBlock lbi = sender as TextBlock;
            World w = lbi.DataContext as World;
            if (w == null)
            {
                CargoAvailable ca = lbi.DataContext as CargoAvailable;
                w = ca.world;
            }
            showWorldPopup(w);
        }

        private void showWorldPopup(World w)
        {
            WorldDisplay wd = new WorldDisplay();
            wd.Day = ship.Day;
            wd.Year = ship.Year;
            wd.WorldItem = w;
            popCargo.Child = wd;
            showPopup();
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
            int passengers = 0;
            if (ship.HighPaxAvail >= ca.HighPassage)
                passengers = ca.HighPassage;
            else
                passengers = ship.HighPaxAvail;
            ship.HighPaxCarried += passengers;
            ship.Credits += passengers * calcPaxCost(ShipCargo.CargoTypes.HighPassage, ship.theWorld, ca.world, ship.theVersion);
            ShipCargo shipCargo = new ShipCargo()
            {
                CargoCode = $"High Passage: {passengers}",
                ShipID = ship.ShipId,
                CargoType = ShipCargo.CargoTypes.HighPassage,
                OriginWorldID = ship.theWorld.WorldID,
                DestinationID = ca.world.WorldID,
                dTons = passengers
            };
            App.DB.Add(shipCargo);
            AddToShipLog(shipCargo);
            App.DB.SaveChangesAsync();
        }

        private int calcPaxCost(ShipCargo.CargoTypes PaxType, World Origin, World Destination, TravellerVersion theVersion)
        {
            Utilities util = new Utilities();
            int distance = util.calcDistance(Origin.Hex, Destination.Hex);
            if (distance < 1 || distance > 6) distance = 1;
            int cost = 0;
            switch (PaxType)
            {
                case ShipCargo.CargoTypes.HighPassage:
                    cost = theVersion.HighPax[distance - 1];
                    break;
                case ShipCargo.CargoTypes.MidPassage:
                    cost = theVersion.MidPax[distance - 1];
                    break;
                case ShipCargo.CargoTypes.LowPassage:
                    cost = theVersion.LowPax[distance - 1];
                    break;
                default:
                    break;
            }
            return cost;
        }

        private void btnMidPax(object sender, RoutedEventArgs e)
        {
            if (ship.theClass.MidPassage == 0 || ship.MidPaxAvail == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("You do not have any mid passage capacity.");
                return;
            }
            Button btn = sender as Button;
            CargoAvailable ca = btn.DataContext as CargoAvailable;
            if (ca.LowPassage == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("There are no mid passage passengers available.");
                return;
            }
            int passengers = 0;
            if (ship.MidPaxAvail >= ca.MidPassage)
                passengers = ca.MidPassage;
            else
                passengers = ship.MidPaxAvail;
            ship.Credits += passengers * calcPaxCost(ShipCargo.CargoTypes.MidPassage, ship.theWorld, ca.world, ship.theVersion);
            ship.MidPaxCarried += passengers;
            ShipCargo shipCargo = new ShipCargo() { CargoCode = $"Mid Passage: {passengers}", ShipID = ship.ShipId, CargoType = ShipCargo.CargoTypes.MidPassage, OriginWorldID = ship.theWorld.WorldID, DestinationID = ca.world.WorldID, dTons = passengers };
            App.DB.Add(shipCargo);
            AddToShipLog(shipCargo);
            App.DB.SaveChangesAsync();
        }

        private void btnLowPax(object sender, RoutedEventArgs e)
        {
            if (ship.theClass.LowPassage == 0 || ship.LowPaxAvail == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("You do not have any low passage capacity.");
                return;
            }
            Button btn = sender as Button;
            CargoAvailable ca = btn.DataContext as CargoAvailable;
            if (ca.LowPassage == 0)
            {
                ErrorHandling eh = new ErrorHandling();
                eh.showError("There are no low passage passengers available.");
                return;
            }
            int passengers = 0;
            if (ship.LowPaxAvail >= ca.LowPassage)
                passengers = ca.LowPassage;
            else
                passengers = ship.LowPaxAvail;
            ship.Credits += passengers * calcPaxCost(ShipCargo.CargoTypes.LowPassage, ship.theWorld, ca.world, ship.theVersion);
            ship.LowPaxCarried += passengers;
            ShipCargo shipCargo = new ShipCargo() { CargoCode = $"Low Passage: {ca.LowPassage}", ShipID = ship.ShipId, CargoType = ShipCargo.CargoTypes.LowPassage, OriginWorldID = ship.theWorld.WorldID, DestinationID = ca.world.WorldID, dTons = passengers };
            App.DB.Add(shipCargo);
            AddToShipLog(shipCargo);
            App.DB.SaveChangesAsync();
        }

        private void AddToShipLog(ShipCargo shipCargo)
        {
            AddLog al = new AddLog();
            shipCargo.isActive = 1;
            al.addLog(ship, shipCargo, true);
        }

        private async void btnRemoveCargo(object sender, RoutedEventArgs e)
        {
            Button tb = sender as Button;
            ShipCargo sc = tb.DataContext as ShipCargo;
            if (sc.CargoType == ShipCargo.CargoTypes.Speculative)
                sellSpeculative(sc);
            else
                unloadCargo(sc);
            await App.DB.SaveChangesAsync();
            refresh();
        }

        private void sellSpeculative(ShipCargo sc)
        {
            SpecCargoSell spec = new SpecCargoSell();
            spec.shipCargo = sc;
            spec.IsSelling = true;
            popCargo.Child = spec;
            showPopup();
        }

        private void unloadCargo(ShipCargo sc)
        {
            sc.isActive = 0;
            sc.DayUnloaded = ship.Day;
            sc.YearUnloaded = ship.Year;
            sc.DestinationID = ship.theWorld.WorldID;
            switch (sc.CargoType)
            {
                case ShipCargo.CargoTypes.Speculative:
                    break;
                case ShipCargo.CargoTypes.Major:
                case ShipCargo.CargoTypes.Minor:
                case ShipCargo.CargoTypes.Incidental:
                    ship.CargoCarried -= sc.dTons;
                    break;
                case ShipCargo.CargoTypes.Mail:
                    break;
                case ShipCargo.CargoTypes.HighPassage:
                    ship.HighPaxCarried -= sc.dTons;
                    break;
                case ShipCargo.CargoTypes.MidPassage:
                    ship.MidPaxCarried -= sc.dTons;
                    break;
                case ShipCargo.CargoTypes.LowPassage:
                    ship.LowPaxCarried -= sc.dTons;
                    break;
                default:
                    break;
            }
            AddLog al = new AddLog();
            al.addLog(ship, sc, false);
        }

        private void comboVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            TravellerVersion tv = (TravellerVersion)cb.SelectedItem;
            if (tv != null)
            {
                ship.TravellerVersionID = tv.TravellerVersionId;
                App.DB.SaveChangesAsync();
            }
        }

        private void btnClearCargo(object sender, RoutedEventArgs e)
        {
            ship.DeleteCargos();
        }

        private void btnClearLogs(object sender, RoutedEventArgs e)
        {
            ship.DeleteLogs();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            ship.DeleteCargos();
            ship.DeleteLogs();
            App.DB.Ships.Remove(ship);
            App.DB.SaveChangesAsync();
            App.mainFrame.Content = new ShipList();
        }

        private void btnSave(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ship.SectorID == 0 || ship.WorldID == 0 || ship.Era is null)
            {
                ErrorHandling err = new ErrorHandling();
                err.showError("Please enter a valid milieu, sector and planet");
                return;
            }
            App.DB.SaveChangesAsync();
            refresh();
        }

        private void cbClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShipClass c = cbClasses.SelectedItem as ShipClass;
            if (c != null)
                ship.ShipClassID = c.ShipClassID;
        }

        private void btn_AddDay(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            date.addDays(1);
            ship.Day = date.Day;
            ship.Year = date.Year;
            App.DB.SaveChangesAsync();
        }

        private void btn_AddWeek(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            date.addDays(7);
            ship.Day = date.Day;
            ship.Year = date.Year;
            App.DB.SaveChangesAsync();
        }

        private async void btnNewLog(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
                    using (TravellerContext db = new TravellerContext())
                    {
                        ShipLog log = new ShipLog(ship);
                        log.Log = txt.Text;
                        db.Add(log);
                        await db.SaveChangesAsync();
                        refresh();
                    }
                    break;
                case ContentDialogResult.Secondary:
                    break;
                default:
                    break;
            }
        }

        private void cbSetMilieu(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string era = cb.SelectedItem.ToString();
            if (era != ship.Era)
            {
                ship.Era = era;
                loadSectorsForEra(era);
            }
        }

        private void cbSetSector(object sender, SelectionChangedEventArgs e)
        {
            if (ship.Era == null)
            {
                ErrorHandling err = new ErrorHandling();
                err.showError("You must enter an era before selecting a sector");
                return;
            }
            ComboBox cb = (ComboBox)sender;
            Sector tu = (Sector)cb.SelectedItem;
            if (tu != null)
            {
                sector = App.DB.Sectors.Where(x => x.Name == tu.Name && x.Milieu == ship.Era).FirstOrDefault();
                if (sector == null)
                {
                    sector = new Sector();
                    sector.Name = tu.Name;
                    sector.Milieu = ship.Era;
                    sector.Tags = tu.Tags;
                    App.DB.Add(sector);
                    App.DB.SaveChangesAsync();
                }
                ship.SectorID = sector.SectorID;
                loadWorlds();
            }
        }

        private void cbSetWorld(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            World w = (World)cb.SelectedItem;
            if (w != null)
                ship.WorldID = w.WorldID;
        }

        // load the worlds when the sector changes
        private async void loadWorlds()
        {
            if (ship.SectorID > 0)
            {
                TravellerMapAPI tu = new TravellerMapAPI();
                App.tmWorlds = await tu.loadWorlds(sector.Milieu, sector.Name);
                comboWorlds.ItemsSource = App.tmWorlds.OrderBy(x => x.Name);
            }
        }

        private async void btnAdd(object sender, RoutedEventArgs e)
        {
            ImageHandler ih = new ImageHandler();
            byte[] newImage = await ih.openImage();
            if (newImage.Length > 0)
            {
                ShowDialog sd = new ShowDialog();
                string desc = await sd.GetResponse("Image Description", "OK", "Cancel");
                if (desc != "Cancel")
                {
                    App.DB.Add(new ImageList() { theImage = newImage, Description = desc, ShipID = ship.ShipId });
                    App.DB.SaveChangesAsync();
                }
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            RichTextBlock rtf = new RichTextBlock();
            Run headText = new Run();
            headText.Text = $"Cargo Manifest for {ship.Name} Date: {ship.Day} - {ship.Year}";
            headText.FontSize = 22;
            Paragraph header = new Paragraph();
            header.Inlines.Add(headText);
            rtf.Blocks.Add(header);
            Paragraph cargo = new Paragraph();
            cargo.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Courier New");
            cargo.Inlines.Add(new Run() { Text = string.Format("\n{0, -20} {1, -10} {2, -8} {3, -15} {4} \n\n", "Description", "Tons/Ppl", "Load Date", "Origin", "Destination") });
            foreach (ShipCargo item in ship.theCargo)
            {
                switch (item.CargoType)
                {
                    case ShipCargo.CargoTypes.Speculative:
                        cargo.Inlines.Add(new Run() { Text = $"{item.theCargo.Description,-20} {item.dTons,-10} {item.DayLoaded,-4}-{ item.YearLoaded,-4} {item.OriginWorld.Name,-15}\n" });
                        break;
                    case ShipCargo.CargoTypes.Major:
                    case ShipCargo.CargoTypes.Minor:
                    case ShipCargo.CargoTypes.Incidental:
                    case ShipCargo.CargoTypes.Mail:
                        break;
                    case ShipCargo.CargoTypes.HighPassage:
                    case ShipCargo.CargoTypes.MidPassage:
                    case ShipCargo.CargoTypes.LowPassage:
                        cargo.Inlines.Add(new Run() { Text = $"{item.CargoCode,-20} {item.DayLoaded,15}-{ item.YearLoaded,-4} {item.OriginWorld.Name,-15} {item.DestinationWorld.Name}\n" });
                        break;
                    default:
                        break;
                }
            }
            rtf.Blocks.Add(cargo);
            PrinterHelper ph = new PrinterHelper();
            ph.PrintRTF(Container, rtf);
            /*
            gridCargoManist.Children.Remove(lstCargoCarried);
            PrinterHelper ph = new PrinterHelper();
            ph.PrintList(Container, lstCargoCarried);
            gridCargoManist.Children.Add(lstCargoCarried);
            */
        }
    }
}
