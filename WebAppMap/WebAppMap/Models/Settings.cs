using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMap.Models
{
    public class Settings
    {
        public Settings()
        {
            // TODO заглушка, заменить из базы

            CenterRadius = 5;

            EcoCenterRadius = 1;
            SportCenterRadius = 1.5;
            BusCenterRadius = 2;
            TruckCenterRadius = 3;

            EcoRadius = EcoCenterRadius * 1.5;
            SportRadius = SportCenterRadius * 1.5;
            BusRadius = BusCenterRadius * 1.5;
            TruckRadius = TruckCenterRadius * 1.5;
        }

        public double CenterRadius { get; set; }

        public double EcoCenterRadius { get; set; }
        public double SportCenterRadius { get; set; }
        public double BusCenterRadius { get; set; }
        public double TruckCenterRadius { get; set; }


        public double EcoRadius { get; set; }
        public double SportRadius { get; set; }
        public double BusRadius { get; set; }
        public double TruckRadius { get; set; }
    }
}