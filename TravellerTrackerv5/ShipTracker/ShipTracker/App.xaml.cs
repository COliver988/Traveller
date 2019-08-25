using ShipTracker.Models;
using System.Windows;

namespace ShipTracker
{

    public partial class App : Application
    {
        public static TravellerDBContext DB = new TravellerDBContext();
    }
}
