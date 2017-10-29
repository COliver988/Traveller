using System.ComponentModel.DataAnnotations.Schema;

namespace Traveller.Models
{
    public class TradeClassification
    {
        public int TradeClassificationID { get; set; }

        // 2 character code (Ag)
        public string Classification { get; set; }

        // spelled out name (Agrictulteral)
        public string Name { get; set; }

        // longer description (The world has the climate and conditions that promote...)
        public string Description { get; set; }

        public string Sizes { get; set; }           // list of sizes space delimited (0 1 2)
        public string Atmospheres { get; set; }     // list of atmospheres space delimited (0 1 2)
        public string Hydro { get; set; }           // list of Hydrographics space delimited (0 1 2)
        public string Pop { get; set; }             // list of Pop space delimited (0 1 2)
        public string Gov { get; set; }             // list of Gov space delimited (0 1 2)
        public string Law { get; set; }             // list of Law space delimited (0 1 2)

        [NotMapped]
        public string Codes {  get { return string.Format("Size: {0} Atm: {1} Hydro: {2} Pop: {3} Gov: {4} Law: {5}", Sizes, Atmospheres, Hydro, Pop, Gov, Law); } }

        [NotMapped]
        public string ShortDescription {  get { return string.Format("{0} {1} [{2}]", Classification, Name, Codes);  } }
    }
}
