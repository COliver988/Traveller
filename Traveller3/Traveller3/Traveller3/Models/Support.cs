using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace Traveller3.Models
{
    public class Support
    {
        private static string prefix;
        public string[] Atmospheres { get; set; }
        public string[] Governments { get; set; }
        public string[] Systems { get; set; }
        public string Errors;

        public enum Versions
        {
            Classic,
            Traveller5,
            Mongoose
        }

        public async void LoadFiles(Ship ship)
        {
            switch (ship.Version)
            {
                case Versions.Classic:
                    prefix = "CT";
                    break;
                case Versions.Traveller5:
                    prefix = "T5";
                    break;
                case Versions.Mongoose:
                    prefix = "MT";
                    break;
                default:
                    break;
            }
            Atmospheres = await loadFile(@"Data\Atmospheres.txt");
            Governments = await loadFile(@"Data\Governments.txt");
        }

        private async Task<string[]> loadFile(string fname)
        {
            StorageFolder installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile file = await installFolder.GetFileAsync(fname);
            if (File.Exists(file.Path))
                return File.ReadAllLines(file.Path);
            else
                return new string[] { "Error loading " + fname };
        }

        public async Task<string[]> loadFileRoaming(string filename)
        {
            try
            {
                Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
                StorageFile file = await roamingFolder.GetFileAsync(filename);
                if (File.Exists(file.Path))
                    return File.ReadAllLines(file.Path);
            }
            catch (Exception)
            {
                Errors += "Sector file error. ";
                throw;
            }
            return new string[] { "Error loading: " + filename };
        }
    }
}
