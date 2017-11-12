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
    public sealed partial class CargoEditor : Page
    {
        public Cargo cargo;
        public CargoEditor(int cargoID)
        {
            this.InitializeComponent();
            cargo = TravellerTracker.App.DB.Cargo.Where(x => x.CargoID == cargoID).First();
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {

        }
    }
}
