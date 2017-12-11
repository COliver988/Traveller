using System.Collections.Generic;
using Traveller.Models;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectCargo : Page
    {
        public string WorldName { get; set; }
        public List<BulkCargo> BulkCargo { get; set; }

        public SelectCargo()
        {
            this.InitializeComponent();
        }
    }
}
