using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMap.Models
{
    public class DriverMapViewModel
    {
        public int Id { get; set; } // driverId
        public string Login { get; set; } //  логин водителя
        public int ClassAvto { get; set; } //  класс авто
        public string ClassAvtoString {
            get
            {
                if (ClassAvto == 1)
                {
                    return "Mini";
                }
                if (ClassAvto == 2)
                {
                    return "Sport";
                }
                if (ClassAvto == 3)
                {
                    return "Bus";
                }
                if (ClassAvto == 4)
                {
                    return "Lorry";
                }
                return "";
            }
        }

        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; }  // широта - для карт google
        public double Radius { get; set; }  // Радиус принятия заказа
        public string Vehicle { get; set; } // марка авто
        
    }
}