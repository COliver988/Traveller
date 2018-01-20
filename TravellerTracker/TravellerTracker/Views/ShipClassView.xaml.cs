using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Traveller.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipClassView : Page
    {
        public ShipClass shipclass;
        public List<ShipClass> classes;

        public ShipClassView()
        {
            this.InitializeComponent();
            showClasses();
        }

        private void showClasses()
        {
            lvClasses.ItemsSource = App.DB.ShipClasses.ToList();
        }

        private void btnNewClass(object sender, RoutedEventArgs e)
        {
            shipclass = new ShipClass();
            shipclass.Name = "new class";
            App.DB.Add(shipclass);
            App.DB.SaveChangesAsync();
            showClasses();
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }

        private void lvClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender ;
            ShipClass tc = lv.SelectedItem as ShipClass;
            Page p = new ShipClassEdit(tc.ShipClassID);
            App.mainFrame.Content = p;
        }
    }
}
