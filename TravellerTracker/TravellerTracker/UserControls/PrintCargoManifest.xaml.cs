using System;
using Traveller.Models;
using TravellerTracker.Support;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class PrintCargoManifest : UserControl
    {
        public Ship TheShip
        {
            get { return (Ship)GetValue(TheShipProperty); }
            set { SetValue(TheShipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheShip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheShipProperty =
            DependencyProperty.Register("TheShip", typeof(Ship), typeof(PrintCargoManifest), new PropertyMetadata(0));

        public PrintCargoManifest()
        {
            this.InitializeComponent();
        }
    }
}
