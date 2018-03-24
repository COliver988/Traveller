using System.Linq;
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
            lstManualCodes.ItemsSource = App.DB.TradeClassifications.Where(x => x.IsManuallyAssigned == true);
            if (WorldItem != null)
            {
                lstWorldLog.ItemsSource = WorldItem.theLog;
                lstTradeCodes.ItemsSource = WorldItem.TradeCodes;
                if (WorldItem.WorldImage != null && WorldItem.WorldImage.Length > 0)
                {
                    try
                    {
                        ImageHandler ih = new ImageHandler();
                        imageWorld.Source = await ih.bytesToImage(WorldItem.WorldImage);
                    }
                    catch (System.Exception)
                    {
                        //TODO: log errors?
                    }
                }
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
            if (image.Length > 0)
            {
                WorldItem.WorldImage = image;
                App.DB.SaveChangesAsync();
                BitmapImage img = await ih.bytesToImage(image);
                imageWorld.Source = img;
            }
        }

        private async void btnAddGeneralImage(object sender, RoutedEventArgs e)
        {
            ImageHandler ih = new ImageHandler();
            byte[] image = await ih.openImage();
            if (image.Length > 0)
            {
                ShowDialog sd = new ShowDialog();
                string desc = await sd.GetResponse("Image Description", "OK", "Cancel");
                if (desc != "Cancel")
                {
                    App.DB.ImageLists.Add(new Models.ImageList() { theImage = image, WorldID = WorldItem.WorldID, Description = desc });
                    App.DB.SaveChanges();
                }
            }
        }

        private void cbTCChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox s = sender as ComboBox;
            TradeClassification tc = s.SelectedItem as TradeClassification;
            App.DB.Add(new WorldTC { TradeClassificationID = tc.TradeClassificationID, WorldID = WorldItem.WorldID });
            App.DB.SaveChangesAsync();
            lstTradeCodes.ItemsSource = WorldItem.TradeCodes;
        }
    }
}
