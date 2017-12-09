using System.Collections.Generic;
using Traveller.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class BulkCargoSelector : UserControl
    {
        public static readonly DependencyProperty CargoListProperty = DependencyProperty.Register("CargoList", typeof(List<BulkCargo>), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public IEnumerable<BulkCargo> CargoList { get; set; }
        public static readonly DependencyProperty WorldNameProperty = DependencyProperty.Register("WorldName", typeof(string), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public string WorldName { get; set; }

        public BulkCargoSelector()
        {
            this.InitializeComponent();
        }
    }
}
