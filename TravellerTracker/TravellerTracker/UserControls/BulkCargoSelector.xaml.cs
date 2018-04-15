using System;
using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Traveller.Support;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class BulkCargoSelector : UserControl
    {
        public static readonly DependencyProperty CargoListProperty = DependencyProperty.Register("CargoList", typeof(List<BulkCargo>), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public IEnumerable<BulkCargo> CargoList { get; set; }
        public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(World), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public World Destination { get; set; }
        public static readonly DependencyProperty ShipProperty = DependencyProperty.Register("Ship", typeof(Ship), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public Ship Ship { get; set; }

        public BulkCargoSelector()
        {
            this.InitializeComponent();
            if (CargoList == null)
            {
                //lstCargo.Visibility = Visibility.Collapsed;
                //txtNoCargo.Visibility = Visibility.Visible;
            }
        }

        private void btnLoad(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            BulkCargo bulk = (BulkCargo)btn.DataContext;
            if (Ship.AvailableCargo >= bulk.dTons)
                loadShip(bulk);
        }

        private void loadShip(BulkCargo bulk)
        {
            Utilities util = new Utilities();
            int distance = util.calcDistance(Ship.theWorld.Hex, Destination.Hex);
            Ship.CargoCarried += bulk.dTons;
            switch (distance)
            {
                case 1:
                    Ship.Credits += Ship.theVersion.Cost1Jump;
                    break;
                case 2:
                    Ship.Credits += Ship.theVersion.Cost2Jump;
                    break;
                case 3:
                    Ship.Credits += Ship.theVersion.Cost3Jump;
                    break;
                case 4:
                    Ship.Credits += Ship.theVersion.Cost4Jump;
                    break;
                case 5:
                    Ship.Credits += Ship.theVersion.Cost5Jump;
                    break;
                case 6:
                    Ship.Credits += Ship.theVersion.Cost6Jump;
                    break;
                default:
                    break;
            }
            Random rnd = new Random();
            int skipTo = rnd.Next(1, App.DB.Cargo.Count());
            int cargoID = App.DB.Cargo.Skip(skipTo).Take(1).First().CargoID;
            Enum.TryParse(bulk.CargoType, out ShipCargo.CargoTypes cargotype);
            TravellerTracker.App.DB.Add(new ShipCargo() { CargoCode = bulk.CargoCode, ShipID = Ship.ShipId, CargoType = cargotype, OriginWorldID = Ship.theWorld.WorldID, dTons = bulk.dTons, DestinationID = Destination.WorldID, isActive = 1, CargoID = cargoID });
            TravellerTracker.App.DB.SaveChangesAsync();
        }
    }
}
