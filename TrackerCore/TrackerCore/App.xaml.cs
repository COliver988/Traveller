using Microsoft.EntityFrameworkCore;
using System.Windows;
using TrackerCore.Models;

namespace TrackerCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TravellerDBContext DB = new TravellerDBContext();

        public App()
        {
            using (var db = new TravellerDBContext())
            {
                try
                {
                    db.Database.Migrate();
                }
                catch (System.Exception)
                {
                }
            }
        }
    }
}
