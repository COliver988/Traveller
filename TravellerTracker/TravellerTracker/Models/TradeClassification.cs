using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traveller.Models
{
    public class TradeClassification
    {
        public int TradeClassificationID { get; set; }

        // 2 character code (Ag)
        [Required]
        public string Classification { get; set; }

        // spelled out name (Agrictulteral)
        [Required]
        public string Name { get; set; }

        // longer description (The world has the climate and conditions that promote...)
        public string Description { get; set; }

        [Required]
        public string Sizes { get; set; }           // list of sizes 
        [Required]
        public string Atmospheres { get; set; }     // list of atmospheres
        [Required]
        public string Hydro { get; set; }           // list of Hydrographics
        [Required]
        public string Pop { get; set; }              // list of Pop
        [Required]
        public string Gov { get; set; }             // list of Gov
        [Required]
        public string Law { get; set; }             // list of Law
        [Required]
        public bool IsManuallyAssigned { get; set; }    // not calculated by UWP

        [NotMapped]
        public string Codes {  get { return string.Format("Size: {0} Atm: {1} Hydro: {2} Pop: {3} Gov: {4} Law: {5}", Sizes, Atmospheres, Hydro, Pop, Gov, Law); } }

        [NotMapped]
        public string ShortDescription {  get { return string.Format("{0} {1} [{2}]", Classification, Name, Codes);  } }
    }
}
