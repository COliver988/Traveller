using System.Collections.Generic;

namespace TravellerTracker.Support
{
    public class BITSCargoCode
    {
        public Dictionary<char, string> ContainerType = new Dictionary<char, string>()
        {
            {'0', "Open Frame" },
            {'1', "Enclosed, unsecured" },
            {'2', "Enclosed, secured" },
            {'3', "Hermetically sealed, secure" },
            {'4', "Hermetically sealed, pressurized" }
        };
        public Dictionary<char, string> ContainerSize = new Dictionary<char, string>()
        {
            {'0', "C0 (small container up to 0.5x0.5x05 m" },
            {'1', "C1 (1 cubic meter 1x1x1 meter" },
            {'2', "C3 (3 cubic meter, 3x1x1 meter" },
            {'3', "C9 (9 cubic meter, 3x3x1 meter" },
            {'4', "C27 (27 cubic meter, 3x3x3 meter" },
            {'5', "C54 (54 cubic meter, 3x3x6 meter" },
            {'6', "C81 (81 cubic meter, 3x3x9 meter" },
            {'7', "C108 (108 cubic meter, 3x6x6 meter" },
            {'8', "C162 (162 cubic meter, 3x6x9 meter" },
            {'9', "non-standard configuration" }
        };
        public Dictionary<char, string> ContainerMass = new Dictionary<char, string>()
        {
            {'0', "Under 1 kg" },
            {'1', "1 - 10 kg" },
            {'2', "10 - 100 kg" },
            {'3', "100 kg - 1 metric ton" },
            {'4', "1 - 10 tonnes" },
            {'5', "10 - 100 tonnes" },
            {'6', "100 - 200 tonnes" },
            {'7', "200 - 500 tonnes" },
            {'8', "500 - 1000 tonnes" },
            {'9', "1000+ tonnes" }
        };
        public Dictionary<char, string> AtmosphericRange = new Dictionary<char, string>()
        {
            {'0', "Vaccum" },
            {'1', "Trace (0.0 - 0.1 atm)" },
            {'2', "Very thin (0.1 - 0.4 atm" },
            {'3', "Thin (0.4 - 0.75 atm" },
            {'4', "Standard (0.75 - 1.4 atm" },
            {'5', "Dense (1.4 - 2.5 atm" },
            {'6', "High Pressure ( (2.5 - 5.0 atm" },
            {'7', "Very High Pressure (5.0 - 25 atm" },
            {'8', "Extreme (25+ atm" }
        };
        public Dictionary<char, string> TemperatureRange = new Dictionary<char, string>()
        {
            {'X', "Below 50C" },
            {'0', "-50C" },
            {'1', "-40C" },
            {'2', "-30C" },
            {'3', "-20C" },
            {'4', "-10C" },
            {'5', "0C" },
            {'6', "10C" },
            {'7', "20C" },
            {'8', "30C" },
            {'9', "40C" },
            {'A', "50C" },
            {'B', "60C" },
            {'C', "70C" },
            {'D', "80C" },
            {'E', "90C" },
            {'F', "100C" },
            {'Y', "100+C" }
        };
        public Dictionary<char, string> HumidityRange = new Dictionary<char, string>()
        {
            {'0', "0%" },
            {'1', "1 - 10%" },
            {'2', "11 - 20%" },
            {'3', "21 - 30%" },
            {'4', "31 - 40%" },
            {'5', "41 - 50%" },
            {'6', "51 - 60%" },
            {'7', "61 - 70%" },
            {'8', "71 - 80%" },
            {'9', "81 - 90%" },
            {'A', "Any" },
        };
        public Dictionary<char, string> MaxGravityRange = new Dictionary<char, string>()
        {
            {'0', "0G" },
            {'1', "1G" },
            {'2', "2G" },
            {'3', "3G" },
            {'4', "4G" },
            {'5', "5G" },
            {'6', "6G" },
            {'7', "7G" },
            {'8', "8G" },
            {'9', "9G" },
            {'A', "9G+" },
            {'X', "Do not store in 0G" }
        };
        public Dictionary<char, string> CargoType = new Dictionary<char, string>()
        {
            {'0', "Solid" },
            {'1', "Powder" },
            {'2', "Solid/liquid mixture" },
            {'3', "Solid/gas mixture" },
            {'4', "Liquid" },
            {'5', "Gas/Liquid mixture" },
            {'6', "Solidified gas" },
            {'7', "Liquified gas" },
            {'8', "Compressed gas" },
            {'9', "Rarified gas" },
            {'A', "Gas plasma" },
            {'B', "Assorted forms (mixed)" },
            {'C', "Live flora" },
            {'D', "Live fauna" },
            {'E', "Unusual" },
        };
        public Dictionary<char, string> ItemCount = new Dictionary<char, string>()
        {
            { '0', "empty" },
            { '1', "1 - 5" },
            { '2', "6 - 10" },
            { '3', "11 - 20" },
            { '4', "21 - 50" },
            { '5', "51 - 100" },
            { '6', "101 - 200" },
            { '7', "201 - 500" },
            { '8', "501 - 1000" },
            { '9', "1001 - 5000" },
            { 'A', "5001 - 10000" },
            { 'B', "10000+" },
        };
        public Dictionary<char, string> MassPerItem = new Dictionary<char, string>()
        {
            {'0', "Under 1 kg" },
            {'1', "1 - 10 kg" },
            {'2', "10 - 100 kg" },
            {'3', "100 kg - 1 metric ton" },
            {'4', "1 - 10 tonnes" },
            {'5', "10 - 100 tonnes" },
            {'6', "100 - 200 tonnes" },
            {'7', "200 - 500 tonnes" },
            {'8', "500 - 1000 tonnes" },
            {'9', "1000+ tonnes" }
        };
        public Dictionary<char, string> EMSRange = new Dictionary<char, string>()
        {
            {'0',  "Do not expose to visible light" },
            {'1',  "Do not expose to UV" },
            {'2',  "Do not expose to IR" },
            {'3',  "Do not expose to low EM fields" },
            {'4',  "Do not expose to RF fields" },
            {'5',  "Do not expose to X-Rays" },
            {'6',  "Do not expose to sunlight equivelant" },
            {'7',  "Do not expose to specific EMS range (supply documentation" },
            {'8',  "Must expose to specific EMS range (supply documentation" }
        };
    }
}
