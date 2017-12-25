using System.Linq;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class SpecCargoSell : UserControl
    {


        public ShipCargo shipCargo
        {
            get { return (ShipCargo)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(ShipCargo), typeof(SpecCargoSell), new PropertyMetadata(0));

        public Ship theShip { get; set; }

        public SpecCargoSell()
        {
            this.InitializeComponent();
            Loaded += SpecCargoSell_Loaded;
        }

        private void SpecCargoSell_Loaded(object sender, RoutedEventArgs e)
        {
            if (shipCargo != null)
            {
                theShip = App.DB.Ships.Where(x => x.ShipId == shipCargo.ShipID).First();
                lstActualValues.ItemsSource = theShip.theVersion.ActualValues;
                txtCurrentWorld.Text = theShip.theWorld.Name;
            }
        }
    }
}
