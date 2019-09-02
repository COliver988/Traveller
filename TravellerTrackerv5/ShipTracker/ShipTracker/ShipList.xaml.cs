using System.Windows;
using System.Windows.Controls;

namespace ShipTracker
{
    /// <summary>
    /// Interaction logic for ShipList.xaml
    /// </summary>
    public partial class ShipList : Window
    {
        public ShipList()
        {
            InitializeComponent();
        }

        private void newShip(object sender, RoutedEventArgs e)
        {
            ShipEditor editor = new ShipEditor();
            editor.Show();
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int? id = b.Tag as int?;
            if (id != null)
            {
                int shipID = id.GetValueOrDefault();
                ShipEditor editor = new ShipEditor(shipID);
                editor.Show();
            }
        }
    }
}
