using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class TradeGoodsEditor : UserControl
    {
        public TradeGood tradeGood
        {
            get { return (TradeGood)GetValue(tradeGoodProperty); }
            set { SetValue(tradeGoodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty tradeGoodProperty =
            DependencyProperty.Register("tradeGood", typeof(TradeGood), typeof(TradeGoodsEditor), new PropertyMetadata(0));

        public List<string> tcs; 

        public TradeGoodsEditor()
        {
            this.InitializeComponent();
            tcs = App.DB.TradeClassifications.Select(x => x.Classification).ToList();
        }
    }
}
