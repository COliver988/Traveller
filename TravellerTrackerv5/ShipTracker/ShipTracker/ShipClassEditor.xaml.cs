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
            Models.ShipClass newClass = new Models.ShipClass() { ClassName = "New Class" };
            this.DataContext = newClass;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ShipClass newClass = this.DataContext as Models.ShipClass;
            App.DB.Add(newClass);
            App.DB.SaveChangesAsync();
        }
    }
}
