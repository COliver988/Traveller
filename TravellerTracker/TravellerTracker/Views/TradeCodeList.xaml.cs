﻿using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TradeCodeList : Page
    {
        public TradeCodeList()
        {
            this.InitializeComponent();
            lstTradeCodes.ItemsSource = TravellerTracker.App.DB.TradeClassifications.ToList().OrderBy(x => x.Classification);
        }

        private void btnLoadTrade(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Page p = new TradeClassificationEdit((int) btn.Tag);
            App.mainFrame.Content = p;
        }
    }
}