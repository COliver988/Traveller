using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using TravellerTracker.Models;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipView : Page
    {
        public Ship ship { get; set; }
        public List<ShipClass> classes;

        public ShipView(int shipID)
        {
            this.InitializeComponent();
            using (var db = new TravellerContext())
            {
                ship = db.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
                classes = db.ShipClasses.ToList();
                ship.Class = classes.Where(x => x.ShipClassID == ship.ShipClassID).FirstOrDefault();
                this.DataContext = ship;
            }
            App.ship = ship;
        }
    }
}
