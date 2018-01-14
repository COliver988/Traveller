using System;
using Windows.UI.Popups;

namespace TravellerTracker.Models
{
    public class ErrorHandling
    {
        public async void showError(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
