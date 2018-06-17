using System.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TradeGoods : Page
    {
        public TradeGoods()
        {
            this.InitializeComponent();
            lvTradeGoods.ItemsSource = App.DB.TradeGoods.OrderBy(x => x.TradeCode);
        }

        private void lvTradeGoods_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
