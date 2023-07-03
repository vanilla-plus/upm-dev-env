using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Geocodes
{
    public interface ICoordinate
    {
        
        double Latitude
        {
            get;
            set; 
        }

        double Longitude
        {
            get;
            set;
        }
        
        public static void Clamp(double latitude, double longitude)
        {
            ClampLatitude(latitude);
            ClampLongitude(longitude);
        }
        
        public static void ClampLatitude(double latitude)
        {
            if (latitude < Min_Latitude) latitude = Min_Latitude;
            if (latitude > Max_Latitude) latitude = Max_Latitude;
        }


        public static void ClampLongitude(double longitude)
        {
            if (longitude < Min_Longitude) longitude = Min_Longitude;
            if (longitude > Max_Longitude) longitude = Max_Longitude;
        }

        public const double Min_Latitude = -90;
        public const double Max_Latitude = 90;

        public const double Min_Longitude = -180;
        public const double Max_Longitude = 180;

    }
}
