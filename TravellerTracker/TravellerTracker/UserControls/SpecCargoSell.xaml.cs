using System;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
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
        public int totalDMS { get; set; }
        public int tradeCodeDMs { get; set; }

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
                txtCurrentWorld.Text = theShip.theWorld.Name.Trim() + " Codes: " + theShip.theWorld.Remarks;
                txtApplicableDMs.Text = findMatchingCodes();
                txtTradeCodeDMs.Text = tradeCodeDMs.ToString();
                txtTotalDMs.Text = totalDMS.ToString();
            }
        }

        // find the matching resell trade codes with the current world
        private string findMatchingCodes()
        {
            string results = "";
            string[] worldCodes = theShip.theWorld.Remarks.Trim().Split(new char[] { ' ', ',' });
            string[] cargoCodes = shipCargo.theCargo.ResaleDMs.Trim().Split(new char[] { ' ', ',' });
            foreach (string wc in worldCodes)
                foreach (string cs in cargoCodes)
                    if (wc.ToLower().Substring(0, 2) == cs.ToLower().Substring(0, 2))
                    {
                        results += cs + " ";
                        int dm = Convert.ToInt32(cs.Substring(3));
                        if (cs.Substring(2, 1) == "+")
                            tradeCodeDMs += dm;
                        else
                            tradeCodeDMs -= dm;
                    }

            totalDMS += tradeCodeDMs;
            return results;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            try
            {
                int dm = Convert.ToInt32(tb.Text);
                totalDMS += dm;
            }
            catch (Exception)
            {
            }
            txtTotalDMs.Text = totalDMS.ToString();
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            Utilities util = new Utilities();
            int roll = util.d6(2) + totalDMS;
            if (roll > 15) roll = 15;
            if (roll < 2) roll = 2;
            txtRoll.Text = roll.ToString();
            int price = theShip.theVersion.ActualValues.Where(x => x.DiceRoll == roll).First().PercentageValue;
            int totalPrice = (price * shipCargo.dTons * shipCargo.theCargo.BasePurchasePrice) / 100;
            txtPrice.Text = totalPrice.ToString();
        }
    }
}
