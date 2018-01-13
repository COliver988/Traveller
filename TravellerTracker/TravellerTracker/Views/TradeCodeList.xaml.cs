﻿using System.Linq;
using Traveller.Models;
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
            loadEdit((int)btn.Tag);
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            TradeClassification code = new TradeClassification() { Classification = "New" };
            App.DB.Add(code);
            App.DB.SaveChanges();
            loadEdit(code.TradeClassificationID);
        }

        private void loadEdit(int id)
        {
            Page p = new TradeClassificationEdit(id);
            App.mainFrame.Content = p;
        }
    }
}
