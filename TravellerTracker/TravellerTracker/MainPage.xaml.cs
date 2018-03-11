using TravellerTracker.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ShipList();
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

        private void btnTradeCodes(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TradeCodeList();
        }

        private void btnCargoList(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new CargoList();
        }

        private void btnStarports(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new StarportList();
        }

        private void btnVersions(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Versions();
        }

        private void btbOptions(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Options();
        }

        private void btnGenericCargo(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TradeGoods();
        }
    }
}
