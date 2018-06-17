using System.Linq;
using Traveller.Models;
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
            lstCargo.ItemsSource = App.DB.Cargo.Where(x => x.D1 != 0).ToList().OrderBy(x => x.D1).ThenBy(x => x.D2);
        }

        private void lstCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            Cargo c = lv.SelectedItem as Cargo;
            Page p = new CargoEditor(c.CargoID);
            App.mainFrame.Content = p;
        }
    }
}
