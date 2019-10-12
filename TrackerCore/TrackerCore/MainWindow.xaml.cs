using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TrackerCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showShips(object sender, RoutedEventArgs e)
        {
            Window shipList = new ShipList();
            List<Models.Ship> ships = App.DB.Ships.ToList();
            shipList.DataContext = ships;
            shipList.Show();
        }

        private void showShipCLasses(object sender, RoutedEventArgs e)
        {
            Window shipClasses = new ShipClassList();
            List<Models.ShipClass> classes = App.DB.ShipClasses.ToList();
            shipClasses.DataContext = classes;
            shipClasses.Show();
        }

    }
}
