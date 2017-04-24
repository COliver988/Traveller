using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravellerTracker.Models;
using TravellerTracker.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TravellerTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new TravellerContext())
            {
                lvShips.ItemsSource = db.Ships.ToList();
            }
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            using (var db = new TravellerContext())
            {
                Traveller.Models.Ship ship = new Traveller.Models.Ship();
                ship.Name = "A New Day";
                db.Ships.Add(ship);
                db.SaveChangesAsync();
            }
        }

        private void btnLoadShip(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new ShipView((int) btn.Tag);
            mainFrame.Content = p;
        }
    }
}
