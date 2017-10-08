using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipView : Page
    {
        public Ship ship { get; set; }
        public ShipClass shipClass { get; set; }
        public Sector sector { get; set; }
        public List<ShipClass> classes;
        public List<ShipLog> logs;
        public ImperialDates date;

        public ShipView(int shipID)
        {
            this.InitializeComponent();
            ship = App.DB.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            sector = App.DB.Sectors.Where(x => x.SectorID == ship.SectorID).FirstOrDefault();
            date = new ImperialDates(ship.Day, ship.Year);
            try
            {
                classes = App.DB.ShipClasses.ToList();
                cbClasses.ItemsSource = classes;
                shipClass = classes.Where(x => x.ShipClassID == ship.ShipClassID).FirstOrDefault();
                cbClasses.SelectedItem = classes.Where(x => x.ShipClassID == ship.ShipClassID).FirstOrDefault();
            }
            catch (System.Exception)
            {
                ErrorHandling e = new ErrorHandling();
                e.showError("Ship class does not exist - please add a class");
            }
            refresh();

            this.DataContext = this;
            App.ship = ship;
        }

        private async void loadEra()
        {
            if (ship.Era != null)
            {
                TravellerMapAPI tu = new TravellerMapAPI();
                App.tmUniverse = await tu.loadUniverse(ship.Era);
                refresh();
            }
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

        private void refresh()
        {
            try
            {
                logs = App.DB.Logs.Where(x => x.ShipId == ship.ShipId).OrderBy(x => x.Year).OrderBy(x => x.Day).ToList();
                lstLog.DataContext = logs;

                // set era
                foreach (ComboBoxItem item in comboEra.Items)
                {
                    if (item.Content.ToString() == ship.Era)
                    {
                        comboEra.SelectedItem = item;
                        break;
                    }
                }
                if (App.tmUniverse.Sectors != null)
                {
                    comboSectors.ItemsSource = App.tmUniverse.Sectors.OrderBy(x => x.FirstName);
                    comboSectors.SelectedItem = App.tmUniverse.Sectors.Where(x => x.FirstName == sector.Name).First();
                }
                if (App.tmWorlds != null)
                {
                    comboWorlds.ItemsSource = App.tmWorlds.OrderBy(x => x.Name);
                    comboWorlds.SelectedItem = App.tmWorlds.Where(x => x.WorldID == ship.WorldID).First();
                }
            }
            catch (Exception ex)
            {
                var x = ex;
            }
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

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void cbSetMilieu(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var era = (cb.SelectedItem as ComboBoxItem).Content;
            if (era.ToString() != ship.Era)
            {
                ship.Era = (string)era;
                loadEra();
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
            TravellerMapUniverse.Sector tu = (TravellerMapUniverse.Sector)cb.SelectedItem;
            if (tu != null)
            {
                sector = App.DB.Sectors.Where(x => x.Name == tu.FirstName && x.Milieu == ship.Era).FirstOrDefault();
                if (sector == null)
                {
                    sector = new Sector();
                    sector.Name = tu.FirstName;
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
            {
                ship.WorldID = w.WorldID;
            }

        }
    }
}
