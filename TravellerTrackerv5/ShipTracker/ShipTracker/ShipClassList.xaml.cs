using System.Windows;

namespace ShipTracker
{
    /// <summary>
    /// Interaction logic for ShipClassList.xaml
    /// </summary>
    public partial class ShipClassList : Window
    {
        public ShipClassList()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new ShipClassEditor();
            editor.Show();
        }
    }
}
