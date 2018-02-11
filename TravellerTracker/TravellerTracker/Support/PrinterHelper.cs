using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TravellerTracker.Support;

namespace TravellerTracker.Support
{
    public class PrinterHelper
    {
        private PrintHelper _printHelper;

        public async void PrintGrid(Grid grid)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 1);
            Canvas ContainerItem = new Canvas();

            _printHelper = new PrintHelper(ContainerItem);
            Grid g = grid.DeepClone<Grid>();
            _printHelper.AddFrameworkElementToPrint(g);
            _printHelper.AddFrameworkElementToPrint(ContainerItem);
            _printHelper.OnPrintCanceled += PrintHelper_OnPrintCanceled;
            _printHelper.OnPrintFailed += PrintHelper_OnPrintFailed;
            _printHelper.OnPrintSucceeded += PrintHelper_OnPrintSucceeded;

            await _printHelper.ShowPrintUIAsync("UWP Community Toolkit Sample App");
        }

        private void ReleasePrintHelper()
        {
            _printHelper.Dispose();
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private async void PrintHelper_OnPrintSucceeded()
        {
            ReleasePrintHelper();
            var dialog = new MessageDialog("Printing done.");
            await dialog.ShowAsync();
        }

        private async void PrintHelper_OnPrintFailed()
        {
            ReleasePrintHelper();
            var dialog = new MessageDialog("Printing failed.");
            await dialog.ShowAsync();
        }

        private void PrintHelper_OnPrintCanceled()
        {
            ReleasePrintHelper();
        }
    }
}
