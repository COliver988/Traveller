using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using System.ComponentModel;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class BulkCargoSelector : UserControl
    {
        public static readonly DependencyProperty CargoListProperty = DependencyProperty.Register("CargoList", typeof(List<BulkCargo>), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public List<BulkCargo> CargoList { get; set; }
        public static readonly DependencyProperty WorldNameProperty = DependencyProperty.Register("WorldName", typeof(string), typeof(BulkCargoSelector), new PropertyMetadata(null));
        public string WorldName { get; set; }

        public BulkCargoSelector()
        {
            this.InitializeComponent();
            lstCargo.ItemsSource = CargoList;
        }
    }
}
