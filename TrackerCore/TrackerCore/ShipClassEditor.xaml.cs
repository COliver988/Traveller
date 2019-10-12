using System.Linq;
using System.Windows;

namespace TrackerCore
{
    /// <summary>
    /// Interaction logic for ShipClassEditor.xaml
    /// </summary>
    public partial class ShipClassEditor : Window
    {

        Models.ShipClass theClass;
        public ShipClassEditor()
        {
            InitializeComponent();
            theClass = new Models.ShipClass() { ClassName = "New Class" };
            this.DataContext = theClass;
            App.DB.Add(theClass);
            App.DB.SaveChangesAsync();
            Closing += ShipClassEditor_Closing;
        }

        private void ShipClassEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }

        public ShipClassEditor(int classID)
        {
            InitializeComponent();
            theClass = App.DB.ShipClasses.Where(x => x.id == classID).FirstOrDefault();
            this.DataContext = theClass;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Models.ShipClass newClass = this.DataContext as Models.ShipClass;
            App.DB.Add(newClass);
            App.DB.SaveChangesAsync();
        }
    }
}
