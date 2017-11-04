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
    public sealed partial class TradeClassificationEdit : Page
    {
        public TradeClassification tc { get; set; }
        public TradeClassificationEdit(int id)
        {
            this.InitializeComponent();
            tc = TravellerTracker.App.DB.TradeClassifications.Where(x => x.TradeClassificationID == id).First();
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            tc = new TradeClassification();
            tc.Name = "new trade classification";
            App.DB.TradeClassifications.Add(tc);
            this.DataContext = tc;
        }
    }
}
