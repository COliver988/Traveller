using System.Windows;

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
    }
}
