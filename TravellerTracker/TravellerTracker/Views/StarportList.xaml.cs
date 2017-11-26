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
            ports = TravellerTracker.App.DB.Starports.ToList();
            lstPorts.ItemsSource = ports;
        }

        private void TextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
        {

        }
    }
}
