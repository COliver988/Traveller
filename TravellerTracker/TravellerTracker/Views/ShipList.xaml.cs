using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveller.Models;
using TravellerTracker.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            using (var db = new TravellerContext())
            {
                lvShips.ItemsSource = db.Ships.ToList();
            }
        }

        private void btnLoadShip(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new ShipView((int) btn.Tag);
            App.mainFrame.Content = p;
        }

        private void btnLoadShipInfo(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new ShipTracker((int) btn.Tag);
            App.mainFrame.Content = p;
        }

        private void btnRemove(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Ship ship = btn.DataContext as Ship;
        }
    }
}
