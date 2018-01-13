using System.Linq;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipList : Page
    {
        public ShipList()
        {
            this.InitializeComponent();
            lvShips.ItemsSource = App.DB.Ships.ToList();
        }

        private void btnLoadShipInfo(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new ShipTracker((int)btn.Tag);
            App.mainFrame.Content = p;
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            Ship ship = new Ship() { Name = "new ship" };
            App.DB.Add(ship);
            App.DB.SaveChanges();
            Page p = new ShipView(ship.ShipId);
            App.mainFrame.Content = p;
        }
    }
}
