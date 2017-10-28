using System;
using System.Linq;
using Traveller.Models;
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
            refresh();
        }

        private void refresh()
        {
            lstLog.ItemsSource = ship.theLog;
        }
    }
}
