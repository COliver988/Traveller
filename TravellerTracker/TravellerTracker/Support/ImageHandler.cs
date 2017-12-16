using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace TravellerTracker.Support
{
    public class ImageHandler
    {
        public async Task<byte[]> openImage()
        {
            byte[] byteArray = new byte[0];
            FileOpenPicker op = new FileOpenPicker();
            op.ViewMode = PickerViewMode.Thumbnail;
            op.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            op.FileTypeFilter.Add(".jpg");
            op.FileTypeFilter.Add(".jpeg");
            op.FileTypeFilter.Add(".png");
            op.FileTypeFilter.Add(".bmp");
            StorageFile file = await op.PickSingleFileAsync();
            if (file != null)
            {
                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();
                    byteArray = new byte[readStream.Length];
                    await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                }
            }
            return byteArray;
        }

        public async Task<BitmapImage> bytesToImage(byte[] image)
        {
            BitmapImage returnImage = new BitmapImage();
            using (InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(image);
                    await writer.StoreAsync();
                    await returnImage.SetSourceAsync(randomAccessStream);
                }
            }
            return  returnImage;
        }
    }
}
