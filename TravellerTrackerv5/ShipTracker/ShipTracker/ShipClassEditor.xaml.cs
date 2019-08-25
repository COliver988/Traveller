using System.Windows;

namespace ShipTracker
{
    /// <summary>
    /// Interaction logic for ShipClassEditor.xaml
    /// </summary>
    public partial class ShipClassEditor : Window
    {
        public ShipClassEditor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }
    }
}
