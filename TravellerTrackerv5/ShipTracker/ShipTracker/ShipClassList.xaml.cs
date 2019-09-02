using System.Windows;
using System.Windows.Controls;

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

        private void edit(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int? id = b.Tag as int?;
            if (id != null)
            {
                int classID = id.GetValueOrDefault();
                ShipClassEditor editor = new ShipClassEditor(classID);
                editor.Show();
            }
        }
    }
}
