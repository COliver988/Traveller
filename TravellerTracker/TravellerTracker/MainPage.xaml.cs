using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveller.Models;
using Traveller.Support;
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
            App.mainFrame = this.mainFrame;
            this.DataContext = App.ship;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ShipList();
            TravellerMapAPI tu = new TravellerMapAPI();
            App.tmUniverse = await tu.loadUnivrerse(); 
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            Traveller.Models.Ship ship = new Traveller.Models.Ship()
            {
                Name = "A New Day"
            };
            App.DB.Ships.Add(ship);
            App.DB.SaveChangesAsync();
            mainFrame.Content = new ShipView(ship.ShipId);
        }

        // list of available ships
        private void btnList(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ShipList();
        }

        private void btnNewClass(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ShipClassView();
        }
    }
}
