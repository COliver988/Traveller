using System.Collections.Generic;
using System.Linq;
using TravellerTracker.Models;
using TravellerTracker.Support;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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

        private async void ImageViewer_Loaded(object sender, RoutedEventArgs e)
        {
            List<MyImageList> myImageList = new List<MyImageList>();
            List<ImageList> imageList = new List<ImageList>();
            if (ShipID > 0) imageList = App.DB.ImageLists.Where(x => x.ShipID == ShipID).ToList();
            else if (WorldID > 0) imageList = App.DB.ImageLists.Where(x => x.WorldID == WorldID).ToList();

            ImageHandler ih = new ImageHandler();
            foreach (ImageList item in imageList)
            {
                BitmapImage i = await ih.bytesToImage(item.theImage);
                myImageList.Add(new MyImageList() { bmp = i, Description = item.Description });
            }
            ctlCarousel.ItemsSource = myImageList;
        }

        private class MyImageList
        {
            public BitmapImage bmp { get; set; }
            public string Description { get; set; }
        }
    }
}
