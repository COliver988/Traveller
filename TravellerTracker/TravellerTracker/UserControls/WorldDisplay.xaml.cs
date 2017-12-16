using Traveller.Models;
using TravellerTracker.Support;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class WorldDisplay : UserControl
    {


        public World world
        {
            get { return (World)GetValue(WorldProperty); }
            set { SetValue(WorldProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorldProperty =
            DependencyProperty.Register("WorldProperty", typeof(World), typeof(WorldDisplay), new PropertyMetadata(0));


        public int Day
        {
            get { return (int)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Day  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(int), typeof(WorldDisplay), new PropertyMetadata(0));
        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(WorldDisplay), new PropertyMetadata(0));




        public WorldDisplay()
        {
            this.InitializeComponent();
        }

        private void btnAddLog(object sender, RoutedEventArgs e)
        {
            Ship ship = new Ship();
            ship.Day = Day;
            ship.Year = Year;
            AddLog al = new AddLog();
            al.addLog(ship, world.WorldID);
            lstWorldLog.ItemsSource = ship.theWorld.theLog;
        }

        private async void btnAddImage(object sender, RoutedEventArgs e)
        {
            ImageHandler ih = new ImageHandler();
            imageWorld.Source = await ih.bytesToImage(world.WorldImage);
        }
    }
}
