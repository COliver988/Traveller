using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
               //db.Database.Migrate();
            }
        }
    }
}
