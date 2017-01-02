using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Traveller3.Models;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Traveller3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Ship ship = new Ship();
        public Support support;

        Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = comboVersion.SelectedItem as ComboBoxItem;
            switch (temp.Tag as string)
            {
                case "CT":
                    ship.Version = Support.Versions.Classic;
                    break;
                case "T5":
                    ship.Version = Support.Versions.Traveller5;
                    break;
                default:
                    ship.Version = Support.Versions.Mongoose;
                    break;
            }
            ship.Save();
        }

        private async void btnLoad(object sender, RoutedEventArgs e)
        {
            support = new Support();
            ship = await ship.load();
            comboVersion.SelectedIndex = (int)ship.Version;
            this.DataContext = ship;
            support.LoadFiles(ship);
            support.Systems = await support.loadFileRoaming(ship.SectorFile);
            listSystems.DataContext = support.Systems;
        }

        private async void btnLoadSector(object sender, RoutedEventArgs e)
        {
            StorageFolder installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".sec");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                await file.CopyAsync(roamingFolder);
                ship.SectorFile = file.Name;
            }
        }
    }
}
