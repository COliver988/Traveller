using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrackerCore
{
    /// <summary>
    /// Interaction logic for ShipEditor.xaml
    /// </summary>
    public partial class ShipEditor : Window
    {
        Models.Ship theShip;

        public ShipEditor()
        {
            InitializeComponent();
            theShip = new Models.Ship() { Name = "New Ship" };
            this.DataContext = theShip;
            App.DB.Add(theShip);
            App.DB.SaveChangesAsync();
            setUp();
            Closing += Window_Closing;
        }
        public ShipEditor(int id)
        {
            InitializeComponent();
            theShip = App.DB.Ships.Where(x => x.id == id).FirstOrDefault();
            this.DataContext = theShip;
            setUp();
        }

        private void setUp()
        {
            List<Models.ShipClass> classes = App.DB.ShipClasses.ToList();
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
