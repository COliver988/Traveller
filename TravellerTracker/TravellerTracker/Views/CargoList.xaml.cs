using System.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CargoList : Page
    {
        public CargoList()
        {
            this.InitializeComponent();
            lstCargo.ItemsSource = App.DB.Cargo.ToList().OrderBy(x => x.D1).ThenBy(x => x.D2);
        }

        private void btnLoadTrade(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new CargoEditor((int) btn.Tag);
            App.mainFrame.Content = p;

        }
    }
}
