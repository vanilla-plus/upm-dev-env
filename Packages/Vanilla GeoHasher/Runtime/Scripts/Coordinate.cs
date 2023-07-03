using System;

using UnityEngine;

namespace Vanilla.Geocodes
{
    
    /// <summary>
    ///     A coordinate is a basic class unit for all geocode, sharing the common latitude/longitude double pairing.
    ///
    ///     It acts as a universal point of conversion between different kinds of geocode.
    /// </summary>
    [Serializable]
    public class Coordinate : ISerializationCallbackReceiver
    {
        
        // Dynamic

        [SerializeField]
        protected double _latitude = 0.0f;
        public double Latitude
        {
            get => _latitude;
            set => _latitude = Math.Clamp(value: value,
                                          min: Min_Latitude,
                                          max: Max_Latitude);
        }
        
        [SerializeField]
        protected double _longitude = 0.0f;
        public double Longitude
        {
            get => _longitude;
            set => _longitude = Math.Clamp(value: value,
                                           min: Min_Longitude,
                                           max: Max_Longitude);
        }
        
        public Coordinate(double latitude,
                          double longitude)
        {
            Latitude  = latitude;
            Longitude = longitude;
        }


        public Coordinate() { }


//        public virtual void OnValidate()
//        {
//            Longitude = _longitude;
//            Latitude  = _latitude;
//        }
        
        // Static

        public const double Min_Latitude = -90;
        public const double Max_Latitude = 90;

        public const double Min_Longitude = -180;
        public const double Max_Longitude = 180;

        public void OnBeforeSerialize() { }
        
        public virtual void OnAfterDeserialize()
        {
            #if UNITY_EDITOR
            Longitude = _longitude;
            Latitude  = _latitude;
            #endif
        }

    }
}
