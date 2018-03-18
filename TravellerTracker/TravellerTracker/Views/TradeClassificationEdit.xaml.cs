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
            tc = new TradeClassification() { Sizes = "", Atmospheres = "", Hydro = "", Pop = "", Gov = "", Law = "" };
            tc.Name = "new";
            App.DB.TradeClassifications.Add(tc);
            App.DB.SaveChanges();
            TradeClassificationEdit p = new TradeClassificationEdit(tc.TradeClassificationID);
            App.mainFrame.Content = p;
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            App.DB.Remove(tc);
            App.DB.SaveChangesAsync();
            TradeCodeList p = new TradeCodeList();
            App.mainFrame.Content = p;
        }
    }
}
