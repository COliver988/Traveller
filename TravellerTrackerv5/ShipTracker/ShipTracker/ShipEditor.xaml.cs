using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShipTracker
{
    /// <summary>
    /// Interaction logic for ShipEditor.xaml
    /// </summary>
    public partial class ShipEditor : Window
    {
        Models.Ship theShip;

        // new ship
        public ShipEditor()
        {
            InitializeComponent();
            theShip = new Models.Ship() { Name = "New Ship" };
            this.DataContext = theShip;
            App.DB.Add(theShip);
            App.DB.SaveChangesAsync();
            setUp();
        }

        public ShipEditor(int id)
        {
            InitializeComponent();
            theShip = App.DB.Ship.Where(x => x.id == id).FirstOrDefault();
            this.DataContext = theShip;
            setUp();
        }

        private void setUp()
        {
            List<Models.ShipClass> classes = App.DB.ShipClass.ToList();
            classList.ItemsSource = classes;
            if (theShip.ShipClassID > 0)
                classList.SelectedItem = classes.Where(x => x.id == theShip.ShipClassID).FirstOrDefault();
        }

        private void ClassList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Models.ShipClass shipClass = (Models.ShipClass)cb.SelectedItem;
            if (shipClass != null)
            {
                theShip.ShipClassID = shipClass.id;
                App.DB.SaveChangesAsync();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.DB.SaveChangesAsync();
        }
    }
}
