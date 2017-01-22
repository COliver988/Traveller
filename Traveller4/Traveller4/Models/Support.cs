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

namespace Traveller4.Models
{
    public class Support
    {
        public enum Versions
        {
            Classic,
            Traveller5,
            Mongoose
        }
        public static string HexValues = "0123456789ABCDEFGHJK";
        public static int HexToInt(char hexvalue)
        {
            return HexValues.IndexOf(hexvalue);
        }
    }
}
