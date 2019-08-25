﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShipTracker.Models
{
    public class Ships
    {
        public int id { get; set; }

        [ForeignKey("ShipClass")]
        public int ShipClassID { get; set; }
        public string Name { get; set; }
        public int currentCargo { get; set; }
        public int currentFuel { get; set; }
        public int currentHighPassengers { get; set; }
        public int currentMidPassengers { get; set; }
        public int currentLowPassengers { get; set; }
        public int currentCredits { get; set; }

        [NotMapped]
        public ShipClasses ShipCLass => App.DB.ShipClass.Where(x => x.id == this.ShipClassID).FirstOrDefault();
    }
}
