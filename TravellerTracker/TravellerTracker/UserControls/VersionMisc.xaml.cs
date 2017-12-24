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
    public sealed partial class VersionMisc : UserControl
    {



        public TravellerVersion Version
        {
            get { return (TravellerVersion)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Version.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register("Version", typeof(TravellerVersion), typeof(VersionMisc), new PropertyMetadata(0));


        public VersionMisc()
        {
            this.InitializeComponent();

            if (Version.ActualValues.Count == 0)
                for (int i = 2; i < 16; i++)
                    TravellerTracker.App.DB.Add(new ActualValue() { DiceRoll = i, TravellerVersionId = Version.TravellerVersionId, PercentageValue = 100 });
            lstValues.ItemsSource = Version.ActualValues;
            cbCodes.ItemsSource = Enum.GetValues(typeof(ShipCargo.CargoTypes)).Cast<ShipCargo.CargoTypes>().ToList();
        }
    }
}
