using System;
using System.Linq;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            Loaded += VersionMisc_Loaded;
        }

        private void VersionMisc_Loaded(object sender, RoutedEventArgs e)
        {
            if (Version != null)
            {
                if (Version.ActualValues.Count > 14)   // in case we screwed up on previous versions
                {
                    App.DB.RemoveRange(App.DB.ActualValues.Where(x => x.TravellerVersionId == Version.TravellerVersionId));
                    App.DB.SaveChanges();
                }
                if (Version.ActualValues.Count == 0)
                    for (int i = 2; i < 16; i++)
                        TravellerTracker.App.DB.Add(new ActualValue() { DiceRoll = i, TravellerVersionId = Version.TravellerVersionId, PercentageValue = 100 });
                App.DB.SaveChanges();
                lstValues.ItemsSource = Version.ActualValues.OrderBy(x => x.DiceRoll);
                switch (Version.CargoCodeType)
                {
                    case TravellerVersion.CargoCodeTypes.BITS:
                        rbBITS.IsChecked = true;
                        break;
                    case TravellerVersion.CargoCodeTypes.T5:
                        rbT5.IsChecked = true;
                        break;
                    case TravellerVersion.CargoCodeTypes.None:
                        rbNone.IsChecked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void rbNone_Click(object sender, RoutedEventArgs e)
        {
            Version.CargoCodeType = TravellerVersion.CargoCodeTypes.None;
        }

        private void rbT5_Click(object sender, RoutedEventArgs e)
        {
            Version.CargoCodeType = TravellerVersion.CargoCodeTypes.T5;
        }

        private void rbBITS_Click(object sender, RoutedEventArgs e)
        {
            Version.CargoCodeType = TravellerVersion.CargoCodeTypes.BITS;
        }
    }
}
