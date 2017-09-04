using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using Windows.UI.Popups;
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
        public List<ShipClass> classes;
        public List<ShipLog> logs;
        public ImperialDates date;

        public ShipView(int shipID)
        {
            this.InitializeComponent();
            ship = App.DB.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            date = new ImperialDates(ship.Day, ship.Year);
            try
            {
                classes = App.DB.ShipClasses.ToList();
                cbClasses.ItemsSource = classes;
                ship.Class = classes.Where(x => x.ShipClassID == ship.ShipClassID).FirstOrDefault();
            }
            catch (System.Exception)
            {
                ErrorHandling e = new ErrorHandling();
                e.showError("Ship class does not exist - please add a class");
            }
            refresh();

            this.DataContext = ship;
            App.ship = ship;
        }

        private void refresh()
        {
            try
            {
                logs = App.DB.Logs.Where(x => x.ShipId == ship.ShipId).OrderBy(x => x.Year).OrderBy(x => x.Day).ToList();
                lstLog.DataContext = logs;
            }
            catch (Exception ex)
            {
                var x = ex;
            }
        }

        private void btnSave(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
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
            TextBox txt = new TextBox { Width = 400, Height= 200 };
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

        private void btnSector(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
