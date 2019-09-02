using System.Linq;
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

        public ShipClassEditor(int classID)
        {
            InitializeComponent();
            Models.ShipClass theClass = App.DB.ShipClass.Where(x => x.id == classID).FirstOrDefault();
            this.DataContext = theClass;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ShipClass newClass = this.DataContext as Models.ShipClass;
            App.DB.Add(newClass);
            App.DB.SaveChangesAsync();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }
    }
}
