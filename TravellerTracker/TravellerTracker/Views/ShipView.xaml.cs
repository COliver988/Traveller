using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Traveller.Models;
using TravellerTracker.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipView : Page
    {
        private Ship ship;

        public ShipView(int shipID)
        {
            this.InitializeComponent();
            using (var db = new TravellerContext())
            {
                ship = db.Ships.Where(x => x.ShipId == shipID).FirstOrDefault();
            }
            this.DataContext = ship;
            App.ship = ship;
        }
    }
}
