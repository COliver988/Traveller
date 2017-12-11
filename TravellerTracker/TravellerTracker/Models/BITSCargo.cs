using System;
using System.Linq;

namespace Traveller.Models
{
    public class BITSCargo
    {
            Random rnd = new Random();
        public BTContainerType container { get; set; }
        public BTCargoType cargo { get; set; }

        public string GenerateBITS(int dTons)
        {
            BITSCargo cargo = new BITSCargo();
            cargo.container = new BTContainerType();
            cargo.cargo = new BTCargoType();

            // determine container type
            cargo.container.ContainerType = rnd.Next(0, 4).ToString("X").First();
            cargo.container.ContainerSize = loadSize(dTons);
            cargo.container.ContainerMass = loadMass(cargo.container.ContainerType, dTons);
            cargo.container.AtmRange = loadAtm(cargo.container.ContainerType, dTons);
            cargo.container.TempRange = loadTemp(cargo.container.ContainerType);
            return cargo.ToString();
        }

        private char loadTemp(char containerType)
        {
            int temp = 0;
            if (containerType == '0') temp = rnd.Next(5, 8);
            if (containerType == '1' || containerType == '2')
                temp = rnd.Next(2, 10);
            else
                temp = rnd.Next(1, 17);
            if (temp < 10) return (char)temp;
            if (temp == 17) return 'Y';
            return temp.ToString("X").ToCharArray().FirstOrDefault();
        }

        private char loadAtm(char containerType, int dTons)
        {
            if (containerType == '0') return '9';
            if (containerType == '1' || containerType == '2')
                return (char)rnd.Next(0, 6);
            return (char)rnd.Next(4, 9);
        }

        private char loadMass(char containerType, int dTons)
        {
            int massMultiplier = ((int)containerType + 1) * 3;
            int mass = dTons * massMultiplier;
            if (mass <= 3) return '2';
            if (mass <= 20) return '3';
            return '4';
        }

        private char loadSize(int dTons)
        {
            if (dTons <= 2) return '4';
            if (dTons <= 4) return '5';
            if (dTons <= 6) return '6';
            if (dTons <= 8) return '7';
            if (dTons <= 12) return '8';
            return '8';  // go with the max
        }

        public override string ToString()
        {
            return $"{container.ContainerType}{container.ContainerSize}{container.ContainerMass}{container.AtmRange}{container.TempRange}{container.HumidityRange}{container.GravityRange}-{cargo.CargoType}{cargo.ItemCount}{cargo.ItemMass}{cargo.AtmRange}{cargo.TempRange}{cargo.HumidityRange}{cargo.GravityRange}{cargo.EMSpectrumRange}-[p]-(q)";
        }
    }

    public class BTContainerType
    {
        public char ContainerType { get; set; }
        public char ContainerSize { get; set; }
        public char ContainerMass { get; set; }
        public char AtmRange { get; set; }
        public char TempRange { get; set; }
        public char HumidityRange { get; set; }
        public char GravityRange { get; set; }
    }

    public class BTCargoType
    {
        public char CargoType { get; set; }
        public char ItemCount { get; set; }
        public char ItemMass { get; set; }
        public char AtmRange { get; set; }
        public char TempRange { get; set; }
        public char HumidityRange { get; set; }
        public char GravityRange { get; set; }
        public char EMSpectrumRange { get; set; }
    }
}
