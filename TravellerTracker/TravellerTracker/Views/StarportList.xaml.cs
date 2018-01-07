using System.Collections.Generic;
using System.Linq;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StarportList : Page
    {
        public List<Starport> ports { get; set; }
        public StarportList()
        {
            this.InitializeComponent();
            Loaded += StarportList_Loaded;
        }

        private void StarportList_Loaded(object sender, RoutedEventArgs e)
        {
            ports = TravellerTracker.App.DB.Starports.ToList();
            lstPorts.ItemsSource = ports;
        }

        private void TextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
        {

        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            TravellerTracker.App.DB.SaveChangesAsync();
        }

        private void btnNew(object sender, RoutedEventArgs e)
        {
            App.DB.Add(new Starport());
            App.DB.SaveChanges();
            StarportList_Loaded(null, null);
        }
        private void btnDelete(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Starport port = (Starport)btn.DataContext;
            if (port != null)
            {
                App.DB.Remove(port);
                App.DB.SaveChangesAsync();
                StarportList_Loaded(null, null);
            }
        }
    }
}
