using System;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using TravellerTracker.Models;
using TravellerTracker.Support;
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



        public bool IsSelling
        {
            get { return (bool)GetValue(IsSellingProperty); }
            set { SetValue(IsSellingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSellingProperty =
            DependencyProperty.Register("IsSelling", typeof(bool), typeof(SpecCargoSell), new PropertyMetadata(0));


        public Ship theShip { get; set; }
        public int totalDMS { get; set; }
        public int tradeCodeDMs { get; set; }
        private int totalPrice;

        public SpecCargoSell()
        {
            this.InitializeComponent();
            Loaded += SpecCargoSell_Loaded;
        }

        private void SpecCargoSell_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsSelling)
                txtBuyOrSell.Text = "Sell";
            else
                txtBuyOrSell.Text = "Buy!";
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
            string[] cargoCodes = new string[] { " " };
            if (IsSelling)
            {
                if (shipCargo.theCargo.ResaleDMs != null)
                    cargoCodes = shipCargo.theCargo.ResaleDMs.Trim().Split(new char[] { ' ', ',' });
            }
            else
            {
                if (shipCargo.theCargo.PurchaseDMs != null)
                    cargoCodes = shipCargo.theCargo.PurchaseDMs.Trim().Split(new char[] { ' ', ',' });
            }
            if (cargoCodes[0] != " ")
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
            totalPrice = (price * shipCargo.dTons * shipCargo.theCargo.BasePurchasePrice) / 100;
            txtPrice.Text = totalPrice.ToString();
        }

        private void btnTransaction(object sender, RoutedEventArgs e)
        {
            AddLog al = new AddLog();
            if (IsSelling)
            {
                theShip.CargoCarried -= shipCargo.dTons;
                theShip.Credits += totalPrice;
                shipCargo.isActive = 0;
                shipCargo.DayUnloaded = theShip.Day;
                shipCargo.YearUnloaded = theShip.Year;
                shipCargo.DestinationID = theShip.theWorld.WorldID;
                al.addLog(theShip, shipCargo, false);
            }
            else
            {
                if (theShip.Credits < totalPrice)
                {
                    ErrorHandling eh = new ErrorHandling();
                    eh.showError("You have insufficient credits!");
                    return;
                }
                if (theShip.AvailableCargo <= shipCargo.dTons)
                {
                    ErrorHandling eh = new ErrorHandling();
                    eh.showError("You don't have the cargo space available!");
                    return;
                }
                theShip.Credits -= totalPrice;
                theShip.CargoCarried += shipCargo.dTons;
                shipCargo.DayLoaded = theShip.Day;
                shipCargo.YearLoaded = theShip.Year;
                shipCargo.OriginWorldID = theShip.theWorld.WorldID;
                shipCargo.isActive = 1;
                App.DB.Add(shipCargo);
                al.addLog(theShip, shipCargo, true);
                App.DB.SaveChangesAsync();
            }
        }
    }
}
