using System.ComponentModel.DataAnnotations.Schema;
using Traveller.Support;

namespace TrackerCore.Models
{
    public class World
    {
        public int Id { get; set; }
        public string Hex { get; set; }
        public string Name { get; set; }
        public string UWP { get; set; }

        // UWP in A123456-7 format
        [NotMapped]
        public char Starport { get { return UWP[0]; } }
        [NotMapped]
        public int Size { get { return Utilities.HexToInt(UWP[1]); } }
        [NotMapped]
        public int Atmosphere { get { return Utilities.HexToInt(UWP[2]); } }
        [NotMapped]
        public int Hydro { get { return Utilities.HexToInt(UWP[3]); } }
        [NotMapped]
        public int Pop { get { return Utilities.HexToInt(UWP[4]); } }
        [NotMapped]
        public int Gov { get { return Utilities.HexToInt(UWP[5]); } }
        [NotMapped]
        public int Law { get { return Utilities.HexToInt(UWP[6]); } }
        [NotMapped]
        public int Tech { get { return Utilities.HexToInt(UWP[8]); } }
    }
}
