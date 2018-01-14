using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TravellerTracker.Support
{
    public class ShowDialog
    {
        public  async Task<string> GetResponse(string title, string primary, string secondary)
        {
            TextBox tb = new TextBox();
            var dialog = new ContentDialog
            {
                Title = title,
                PrimaryButtonText = primary,
                SecondaryButtonText = secondary,
                Content = tb
            };
            var results = await dialog.ShowAsync();
            switch (results)
            {
                case ContentDialogResult.None:
                    break;
                case ContentDialogResult.Primary:
                    return tb.Text;
                    break;
                case ContentDialogResult.Secondary:
                    return secondary;
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
