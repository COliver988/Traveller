﻿using System.Collections.Generic;
using Traveller.Models;
using TravellerTracker.UserControls;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Versions : Page
    {
        IEnumerable<TravellerVersion> versions { get; set; }
        public Versions()
        {
            this.InitializeComponent();
            versions = TravellerTracker.App.DB.TravellerVersions;
            this.DataContext = versions;
        }

        private void btnMisc(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            VersionMisc vm = new VersionMisc();
        }
    }
}
