using System.IO;
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
    public sealed partial class ShipClassEdit : Page
    {
        public ShipClass shipClass;

        public ShipClassEdit(int shipClassID)
        {
            this.InitializeComponent();
            shipClass = App.DB.ShipClasses.Where(x => x.ShipClassID == shipClassID).FirstOrDefault();
            txtTypePrimary.Text = File.ReadAllText("Resources/ShipTypeCodesPrimary.txt");
            txtTypeSecondary.Text = File.ReadAllText("Resources/ShipTypeCodesSecondary.txt");
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }
    }
}
