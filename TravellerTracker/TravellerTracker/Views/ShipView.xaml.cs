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
            try
            {
                logs = App.DB.Logs.Where(x => x.ShipId == ship.ShipId).OrderBy(x => x.Year).OrderBy(x => x.Day).ToList();
            }
            catch (System.Exception)
            {
            }
            this.DataContext = ship;
            App.ship = ship;
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

        private void btnNewLog(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShipLog l = new ShipLog(ship);

        }
    }
}
