using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Traveller.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class PrintCargoManifest : UserControl
    {



        public Ship ship
        {
            get { return (Ship)GetValue(shipProperty); }
            set { SetValue(shipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ship.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty shipProperty =
            DependencyProperty.Register("ship", typeof(Ship), typeof(PrintCargoManifest), new PropertyMetadata(0));


        public PrintCargoManifest()
        {
            this.InitializeComponent();
        }
    }
}
