using System;

using UnityEngine;

namespace Vanilla.Geocodes
{
    
    [Serializable]
    public abstract class GeoCode
    {

        public const double Min_Latitude = -90;
        public const double Max_Latitude = 90;

        public const double Min_Longitude = -180;
        public const double Max_Longitude = 180;

        [SerializeField]
        protected double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = ValidateLatitude(value);

                _hash = ValidateHash(_hash);
            }
        }

        [SerializeField]
        protected double _longitude;
        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = ValidateLongitude(value);

                _hash = ValidateHash(_hash);
            }
        }

        [SerializeField]
        protected int _precision = 10;
        public int Precision
        { 
            get => _precision;
            set
            {
                _precision = ValidatePrecision(value);

                _hash = ValidateHash(_hash);
            }
        }

        [SerializeField]
        protected string _hash;
        public string Hash
        {
            get => _hash;
            set
            {
                _hash                     = value;
                var (latitude, longitude) = Decode(_hash);
                _latitude                 = latitude;
                _longitude                = longitude;
            }
        }

        public GeoCode(double latitude, double longitude)
        {
            _latitude  = latitude;
            _longitude = longitude;
            _precision = Default_Precision;

            Validate();
        }
        
        public GeoCode(double latitude, double longitude, int precision)
        {
            _latitude  = latitude;
            _longitude = longitude;
            _precision = precision;

            Validate();
        }


        public void Validate()
        {
            _latitude = ValidateLatitude(_latitude);

            _longitude = ValidateLongitude(_longitude);

            _precision = ValidatePrecision(_precision);

            _hash = ValidateHash(_hash);
        }

        public double ValidateLatitude(double value) => Math.Clamp(value: value,
                                                                   min: Min_Latitude,
                                                                   max: Max_Latitude);
        
        public double ValidateLongitude(double value) => Math.Clamp(value: value,
                                                                    min: Min_Longitude,
                                                                    max: Max_Longitude);

        public int ValidatePrecision(int value) => Math.Clamp((int) Math.Round((double) value / Precision_Factor) * Precision_Factor,
                                                              Min_Precision,
                                                              Max_Precision);


        public string ValidateHash(string hash) => Encode(_latitude,
                                                          _longitude,
                                                          _precision);


        public GeoCode(string hash) => Hash = hash;

        protected abstract string Encode(double latitude,
                                         double longitude);

        protected abstract string Encode(double latitude,
                                         double longitude,
                                         int precision);
        protected abstract (double latitude, double longitude) Decode(string hash);

        public abstract int Default_Precision
        {
            get;
        }
        
        public abstract int Min_Precision
        {
            get;
        }

        public abstract int Max_Precision
        {
            get;
        }

        // In some encodings like OpenLocationCode, Precision must be divisible by some amount like 2.
        // We can enforce this by rounding by a factor in the setter.
        public virtual int Precision_Factor => 1;

    }

}
