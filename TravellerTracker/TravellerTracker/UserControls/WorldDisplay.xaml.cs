using Traveller.Models;
using TravellerTracker.Support;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class WorldDisplay : UserControl
    {


        public World WorldItem
        {
            get { return (World)GetValue(WorldProperty); }
            set { SetValue(WorldProperty, value); }
        }
        public static readonly DependencyProperty WorldProperty =
            DependencyProperty.Register("WorldProperty", typeof(World), typeof(WorldDisplay), new PropertyMetadata(0));


        public int Day
        {
            get { return (int)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(int), typeof(WorldDisplay), new PropertyMetadata(0));

        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(WorldDisplay), new PropertyMetadata(0));




        public WorldDisplay()
        {
            this.InitializeComponent();
            Loaded += WorldDisplay_Loaded;
        }

        private async void WorldDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = WorldItem;
            lstWorldLog.ItemsSource = WorldItem.theLog;
            if (WorldItem.WorldImage != null)
            {
                ImageHandler ih = new ImageHandler();
                imageWorld.Source = await ih.bytesToImage(WorldItem.WorldImage);
            }
        }

        private void btnAddLog(object sender, RoutedEventArgs e)
        {
            Ship ship = new Ship();
            ship.Day = Day;
            ship.Year = Year;
            AddLog al = new AddLog();
            al.addLog(ship, WorldItem.WorldID);
        }

        private async void btnAddImage(object sender, RoutedEventArgs e)
        {
            ImageHandler ih = new ImageHandler();
            byte[] image = await ih.openImage();
            WorldItem.WorldImage = image;
            App.DB.SaveChangesAsync();
            BitmapImage img = await ih.bytesToImage(image);
            imageWorld.Source = img;
        }
    }
}
