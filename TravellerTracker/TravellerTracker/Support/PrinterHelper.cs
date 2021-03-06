﻿using Microsoft.Toolkit.Uwp.Helpers;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TravellerTracker.Support
{
    public class PrinterHelper
    {
        private PrintHelper _printHelper;

        public void PrintRTF(Grid Container, RichTextBlock item)
        {
            _printHelper = new PrintHelper(Container);
            _printHelper.AddFrameworkElementToPrint(item);
            setupPrinter();
        }
        public void PrintList(Grid Container, ListBox list)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 1);

            _printHelper = new PrintHelper(Container);
            _printHelper.AddFrameworkElementToPrint(list);
            setupPrinter();
        }

        private async void setupPrinter()
        { 
            _printHelper.OnPrintCanceled += PrintHelper_OnPrintCanceled;
            _printHelper.OnPrintFailed += PrintHelper_OnPrintFailed;
            _printHelper.OnPrintSucceeded += PrintHelper_OnPrintSucceeded;

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            await _printHelper.ShowPrintUIAsync($"{App.AppName}");
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
