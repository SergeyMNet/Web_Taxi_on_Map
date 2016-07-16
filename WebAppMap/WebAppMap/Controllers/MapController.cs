using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMap.Models;

namespace WebAppMap.Controllers
{
    public class MapController : Controller
    {
        //Координаты центра Киева
        private static double _centerLat = 50.260091;
        private static double _centerLon = 30.413415;

        private static List<DriverMapViewModel> _drivers;
        private static Settings _setting = new Settings();

        // fake divers
        private static readonly int _countFakeDrivers = 10;

        // GET: Map
        public ActionResult Index()
        {
            if (_drivers == null || !_drivers.Any())
            {
                PrepareDrivers();
            }

            return View();
        }

        

        public JsonResult GetData()
        {
            return Json(_drivers, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        
        private double[] GetCurrentDriverRadiuses(int type, double latitude, double longitude)
        {
            // радиус отличается в городе и за городом
            bool isInCenter = IsInCenter(latitude, longitude);
            
            long vt = type;
            double[] radiuses = GetAllRadiuses(isInCenter);
            List<double> radiusesList = new List<double>();
            radiusesList.Add(radiuses[vt - 1]);
            return radiusesList.ToArray();
        }

        private double[] GetAllRadiuses(bool IsInCenter)
        {
            if (_setting != null)
            {
                if (IsInCenter)
                {
                    return new[]{
                        _setting.EcoCenterRadius,
                        _setting.SportCenterRadius,
                        _setting.BusCenterRadius,
                        _setting.TruckCenterRadius
                    };
                }

                {
                    return new[]{
                        _setting.EcoRadius,
                        _setting.SportRadius,
                        _setting.BusRadius,
                        _setting.TruckRadius
                    };
                }
            }
            //По умолчанию
            return new double[] { 4, 5, 6, 7 };

        }

        private bool IsInCenter(double latitude, double longitude)
        {
            var distToCenter = GetDistance(latitude, longitude, _centerLat, _centerLon);

            //если клиент в центре
            if (distToCenter < _setting.CenterRadius)
                //вернуть CenterSearchRadius
                return true;
            else
                //иначе вернуть SearchRadius
                return false;
        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dist = (6371*
                           Math.Acos(Math.Cos(DegreeToRadian(lat1))*
                                     Math.Cos(DegreeToRadian(lat2))*
                                     Math.Cos(DegreeToRadian(lon2) - DegreeToRadian(lon1)) +
                                     Math.Sin(DegreeToRadian(lat1))*
                                     Math.Sin(DegreeToRadian(lat2))));
            
            return dist;
        }


        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }


        #endregion

        #region PrepareFakeData // TODO заглушка, заполнять из БД

        private void PrepareDrivers()
        {
            try
            {
                Random r = new Random();
                _drivers = new List<DriverMapViewModel>();

                //Формируем результат в цикле по каждому водителю в радиусе
                for (int i = 1; i <= _countFakeDrivers; i++)
                {
                    double GeoLat = (50.34 + (Double.Parse(r.Next(30, 150).ToString()) / 1000));
                    double GeoLong = (30.35 + (Double.Parse(r.Next(20, 250).ToString()) / 1000));

                    Debug.WriteLine("----------------------Geo");
                    Debug.WriteLine("GeoLat = " + GeoLat);
                    Debug.WriteLine("GeoLong = " + GeoLong);

                    int classVehicle = r.Next(1, 5);

                    _drivers.Add(new DriverMapViewModel()
                    {
                        Id = i,
                        Login = "driver " + i,
                        ClassAvto = classVehicle,
                        GeoLat = GeoLat,
                        GeoLong = GeoLong,
                        Radius = GetCurrentDriverRadiuses(classVehicle, GeoLong, GeoLat).FirstOrDefault(),
                        Vehicle = "type " + classVehicle
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("------Exception------" + ex.Message);
            }
        }
        
        #endregion


        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
    }
}