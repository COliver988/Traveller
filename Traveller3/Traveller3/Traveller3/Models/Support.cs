using System;
using System.Collections.Generic;
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
        public static string[] Atmospheres;
        public static string[] Governments;

        public enum Versions
        {
            Classic,
            Traveller5,
            Mongoose
        }

        internal static async void LoadFiles(Versions version)
        {
            switch (version)
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
            Governments = await loadFile( @"Data\Governments.txt");
        }

        private static async Task<string[]> loadFile(string fname)
        {
            StorageFolder installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile file = await installFolder.GetFileAsync(fname);
            if (File.Exists(file.Path))
                return File.ReadAllLines(file.Path);
            else
                return new string[] { "Error loading " + fname };
        }
    }
}
