using Microsoft.EntityFrameworkCore;
using ShipTracker.Models;
using System.Windows;

namespace ShipTracker
{

    public partial class App : Application
    {
        public static TravellerDBContext DB = new TravellerDBContext();

        public App()
        {
            using (var db = new TravellerDBContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
