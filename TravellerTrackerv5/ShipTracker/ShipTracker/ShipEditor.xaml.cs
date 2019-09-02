using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShipTracker
{
    /// <summary>
    /// Interaction logic for ShipEditor.xaml
    /// </summary>
    public partial class ShipEditor : Window
    {
        // new ship
        public ShipEditor()
        {
            InitializeComponent();
            Models.Ship theShip = new Models.Ship() { Name = "New Ship" };
            this.DataContext = theShip;
            App.DB.Add(theShip);
            App.DB.SaveChangesAsync();
            setUp();
        }

        public ShipEditor(int id)
        {
            InitializeComponent();
            Models.Ship theShip = App.DB.Ship.Where(x => x.id == id).FirstOrDefault();
            this.DataContext = theShip;
            setUp();
        }

        private void setUp()
        {
            List<Models.ShipClass> classes = App.DB.ShipClass.ToList();
            classList.ItemsSource = classes;
        }
    }
}
