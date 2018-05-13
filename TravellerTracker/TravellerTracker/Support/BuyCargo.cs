using Traveller.Models;

namespace TravellerTracker.Support
{
    public class BuySellCargo
    {
        public void BuyCargo(Ship ship, ShipCargo cargo)
        {
            AddLog al = new AddLog();
            ship.Credits -= cargo.PurchasePrice;
            ship.CargoCarried += cargo.dTons;
            cargo.DayLoaded = ship.Day;
            cargo.YearLoaded = ship.Year;
            cargo.OriginWorldID = ship.theWorld.WorldID;
            cargo.isActive = 1;
            App.DB.Add(cargo);
            al.addLog(ship, cargo, true);
            App.DB.SaveChangesAsync();
        }

        public void SellCargo(Ship ship, ShipCargo cargo)
        {
            AddLog al = new AddLog();
            ship.CargoCarried -= cargo.dTons;
            ship.Credits += cargo.ResellPrice;
            cargo.isActive = 0;
            cargo.DayUnloaded = ship.Day;
            cargo.YearUnloaded = ship.Year;
            cargo.DestinationID = ship.theWorld.WorldID;
            al.addLog(ship, cargo, false);
        }
    }
}
