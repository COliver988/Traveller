using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TravellerTracker.UserControls
{
    public sealed partial class ImageViewer : UserControl
    {
        public int ShipID
        {
            get { return (int)GetValue(ShipIDProperty); }
            set { SetValue(ShipIDProperty, value); }
        }
        public int WorldID
        {
            get { return (int)GetValue(WorldIDProperty); }
            set { SetValue(WorldIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WorldID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorldIDProperty =
            DependencyProperty.Register("WorldID", typeof(int), typeof(ImageViewer), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for ShipID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipIDProperty =
            DependencyProperty.Register("ShipID", typeof(int), typeof(ImageViewer), new PropertyMetadata(0));


        public ImageViewer()
        {
            this.InitializeComponent();
            Loaded += ImageViewer_Loaded;
        }

        private void ImageViewer_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShipID > 0)
                ctlCarousel.ItemsSource = App.DB.ImageLists.Where(x => x.ShipID == ShipID);
            else
                if (WorldID > 0)
                ctlCarousel.ItemsSource = App.DB.ImageLists.Where(x => x.WorldID == WorldID);

            var y = App.DB.ImageLists.Where(x => x.ShipID == ShipID);
        }
    }
}
