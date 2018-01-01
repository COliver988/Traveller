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
            foreach (TravellerVersion version in App.TravellerVersions)
                if (version.TravellerVersionId == cargo.TravellerVersionId)
                {
                    switch (version.Name)
                    {
                        case "T5":
                            rbT5.IsChecked = true;
                            break;
                        case "Mongoose Traveller":
                            rbMongoose.IsChecked = true;
                            break;
                        default:
                            rbClassic.IsChecked = true;
                            break;
                    }
                    break;
                }
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevious(object sender, RoutedEventArgs e)
        {
            cargo = App.DB.Cargo.Where(x => x.CargoID < this.cargo.CargoID).OrderBy(x => x.CargoID).FirstOrDefault();
            this.DataContext = null;
            this.DataContext = cargo;
        }

        private void btnNext(object sender, RoutedEventArgs e)
        {
            cargo = App.DB.Cargo.Where(x => x.CargoID > this.cargo.CargoID).OrderBy(x => x.CargoID).FirstOrDefault();
            this.DataContext = null;
            this.DataContext = cargo;
        }

        private void rbClassic_Click(object sender, RoutedEventArgs e)
        {
            cargo.TravellerVersionId = App.DB.TravellerVersions.Where(x => x.Name == "Classic").First().TravellerVersionId;
        }

        private void rbT5_Click(object sender, RoutedEventArgs e)
        {
            cargo.TravellerVersionId = App.DB.TravellerVersions.Where(x => x.Name == "T5").First().TravellerVersionId;
        }

        private void rbMongoose_Click(object sender, RoutedEventArgs e)
        {
            cargo.TravellerVersionId = App.DB.TravellerVersions.Where(x => x.Name == "Mongoose").First().TravellerVersionId;
        }
    }
}
