using System.Windows;

namespace ShipTracker
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
            shipList.Show();
        }

        private void showShipCLasses(object sender, RoutedEventArgs e)
        {
            Window shipClasses = new ShipClassList();
            shipClasses.Show();
        }
    }
}
