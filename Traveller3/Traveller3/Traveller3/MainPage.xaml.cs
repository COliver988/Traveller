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

        // load up a ship profile
        private async void btnLoad(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).support = new Support();
            ship = await ship.load();
            comboVersion.SelectedIndex = (int)ship.Version;
            ((App)Application.Current).support.TLModifier = await ((App)Application.Current).support.loadFile(@"Data\TLModifier.txt");
            ((App)Application.Current).support.PortModifier = await ((App)Application.Current).support.loadFile(@"Data\PortModifier.txt");
            ((App)Application.Current).support.LoadFiles(ship);
            ((App)Application.Current).support.Systems = await ((App)Application.Current).support.loadFileRoaming(ship.SectorFile);
            ((App)Application.Current).support.Worlds = await ((App)Application.Current).support.loadWorlds();
            listSystems.ItemsSource = ((App)Application.Current).support.Worlds;
            if (ship.CurrentSEC != null)
                ship.Location = new World(ship.CurrentSEC, ship.Version);
            this.DataContext = ship;
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

        private void listSystems_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ship.CurrentSystem == null || ship.CurrentSystem.Length < 5)
            {
                ListBox lb = sender as ListBox;
                World w = lb.SelectedItem as World;
                ship.CurrentSEC = w.SEC;
                ship.Location = w;
            }
        }
    }
}
