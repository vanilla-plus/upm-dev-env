using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using static UnityEngine.Debug;

namespace Vanilla.Geocodes
{

    /// <summary>
    ///     Class representation of a <see href="https://en.wikipedia.org/wiki/Geohash">Geohash</see>
    /// </summary>
    [Serializable]
    public class GeoHash : Coordinate, IGeocode<GeoHash>
    {

        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- Dynamic
        
        [SerializeField]
        private int _precision = Default_Precision;
        public int Precision
        {
            get => _precision;
            set => _precision = Math.Clamp(value: value,
                                           min: Min_Precision,
                                           max: Max_Precision);
        }
        
        [SerializeField]
        private string _hash;
        public string Hash
        {
            get => _hash;
            set
            {
                var (latitude, longitude) = HashToCoordinate(value);

                if (InvalidLatLong(latitude: latitude,
                                   longitude: longitude))
                {
                    LogError("Invalid geohash!");

                    return;
                }

                Precision = value.Length;
            
                Latitude  = latitude;
                Longitude = longitude;
            
                _hash = value;
            }
        }


        public string Encode(double latitude,
                             double longitude)
        {
            _latitude  = latitude;
            _longitude = longitude;
            
            return CoordinateToHash(_latitude,
                                    _longitude,
                                    Precision);
        }


        public (double, double) Decode(string hash)
        {
            _hash = hash;

            return HashToCoordinate(_hash);
        }


        public void UpdateNeighbours(GeoHash northWest,
                                     GeoHash north,
                                     GeoHash northEast,
                                     GeoHash west,
                                     GeoHash east,
                                     GeoHash southWest,
                                     GeoHash south,
                                     GeoHash southEast)
        {
            var hashLength = _hash.Length;

            var lastCh = _hash[index: hashLength - 1];

            var parent = _hash.Substring(startIndex: 0,
                                            length: hashLength - 1);

            string n; // cache the north result for later use with north-west and north-east

            string s; // cache the south result for later use with south-west and south-east
            
            // Per-direction

            if (hashLength % 2 == 0)
            {
                // Even

                if (parent.Length == 0)
                {
                    // North

                    n = north.Hash = Base32[index: Neighbour_A.IndexOf(value: lastCh)].ToString();
                
                    // West
                
                    west.Hash = Base32[index: Neighbour_D.IndexOf(value: lastCh)].ToString();
                
                    // East

                    east.Hash = Base32[index: Neighbour_C.IndexOf(value: lastCh)].ToString();
                
                    // South

                    s = south.Hash = Base32[index: Neighbour_B.IndexOf(value: lastCh)].ToString();
                }
                else
                {
                    // North

                    n = north.Hash = Border_A.IndexOf(value: lastCh) != -1 ?
                                            GetNeighbour(geohash: parent, direction: N) + Base32[index: Neighbour_A.IndexOf(value: lastCh)] :
                                            parent                                      + Base32[index: Neighbour_A.IndexOf(value: lastCh)];
                
                    // West

                    west.Hash = Border_D.IndexOf(value: lastCh) != -1 ?
                                       GetNeighbour(geohash: parent, direction: W) + Base32[index: Neighbour_D.IndexOf(value: lastCh)] :
                                       parent                                      + Base32[index: Neighbour_D.IndexOf(value: lastCh)];
                
                    // East

                    east.Hash = Border_C.IndexOf(value: lastCh) != -1 ?
                                       GetNeighbour(geohash: parent, direction: E) + Base32[index: Neighbour_C.IndexOf(value: lastCh)] :
                                       parent                                      + Base32[index: Neighbour_C.IndexOf(value: lastCh)];
                
                    // South

                    s = south.Hash = Border_B.IndexOf(value: lastCh) != -1 ?
                                            GetNeighbour(geohash: parent, direction: S) + Base32[index: Neighbour_B.IndexOf(value: lastCh)] :
                                            parent                                      + Base32[index: Neighbour_B.IndexOf(value: lastCh)];
                }
                
            }
            else
            {
                // Odd
                
                if (parent.Length == 0)
                {
                    // North

                    n = north.Hash = Base32[index: Neighbour_C.IndexOf(value: lastCh)].ToString();

                    // West
                
                    west.Hash = Base32[index: Neighbour_B.IndexOf(value: lastCh)].ToString();
                
                    // East

                    east.Hash = Base32[index: Neighbour_A.IndexOf(value: lastCh)].ToString();
                
                    // South

                    s = south.Hash = Base32[index: Neighbour_D.IndexOf(value: lastCh)].ToString();
                }
                else
                {
                    // North

                    n = north.Hash = Border_C.IndexOf(value: lastCh) != -1 ?
                                      GetNeighbour(geohash: parent, direction: N) + Base32[index: Neighbour_C.IndexOf(value: lastCh)] :
                                      parent                                      + Base32[index: Neighbour_C.IndexOf(value: lastCh)];
                
                    // West

                    west.Hash = Border_B.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent, direction: W) + Base32[index: Neighbour_B.IndexOf(value: lastCh)] :
                                     parent                                      + Base32[index: Neighbour_B.IndexOf(value: lastCh)];
                
                    // East

                    east.Hash = Border_A.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent, direction: E) + Base32[index: Neighbour_A.IndexOf(value: lastCh)] :
                                     parent                                      + Base32[index: Neighbour_A.IndexOf(value: lastCh)];
                
                    // South

                    s = south.Hash = Border_D.IndexOf(value: lastCh) != -1 ?
                                            GetNeighbour(geohash: parent, direction: S) + Base32[index: Neighbour_D.IndexOf(value: lastCh)] :
                                            parent                                      + Base32[index: Neighbour_D.IndexOf(value: lastCh)];
                }

            }
            
            // Now you can do NW/NE/SW/SE by referring to the already existing entries...

            // North...

            var n_HashLength = n.Length;

            var n_LastCh = n[index: hashLength - 1];

            var n_Parent = n.Substring(startIndex: 0,
                                      length: n_HashLength - 1);
            
            var n_EvenHashLength = n_HashLength % 2 == 0;

            if (n_EvenHashLength)
            {
                if (n_Parent.Length == 0)
                {
                    // ...West
                
                    northWest.Hash = Base32[index: Neighbour_D.IndexOf(value: n_LastCh)].ToString();
                
                    // ...East
                
                    northEast.Hash = Base32[index: Neighbour_C.IndexOf(value: n_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    northWest.Hash = Border_D.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent, direction: W) + Base32[index: Neighbour_D.IndexOf(value: n_LastCh)] :
                                     n_Parent                                      + Base32[index: Neighbour_D.IndexOf(value: n_LastCh)];
                
                    // ...East
                
                    northEast.Hash = Border_C.IndexOf(value: n_LastCh) != -1 ?
                                            GetNeighbour(geohash: n_Parent, direction: E) + Base32[index: Neighbour_C.IndexOf(value: n_LastCh)] :
                                            n_Parent                                      + Base32[index: Neighbour_C.IndexOf(value: n_LastCh)];
                }
            }
            else
            {
                if (n_Parent.Length == 0)
                {
                    // ...West
                
                    northWest.Hash = Base32[index: Neighbour_B.IndexOf(value: n_LastCh)].ToString();
                
                    // ...East
                
                    northEast.Hash = Base32[index: Neighbour_A.IndexOf(value: n_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    northWest.Hash = Border_B.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent, direction: W) + Base32[index: Neighbour_B.IndexOf(value: n_LastCh)] :
                                     n_Parent                                      + Base32[index: Neighbour_B.IndexOf(value: n_LastCh)];
                
                    // ...East
                
                    northEast.Hash = Border_A.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent, direction: E) + Base32[index: Neighbour_A.IndexOf(value: n_LastCh)] :
                                     n_Parent                                      + Base32[index: Neighbour_A.IndexOf(value: n_LastCh)];
                }
            }


            // South...
            
            var s_HashLength = s.Length;

            var s_LastCh = s[index: hashLength - 1];

            var s_Parent = s.Substring(startIndex: 0,
                                       length: s_HashLength - 1);
            
            var s_EvenHashLength = s_HashLength % 2 == 0;

            if (s_EvenHashLength)
            {
                if (s_Parent.Length == 0)
                {
                    // ...West
                
                    southWest.Hash = Base32[index: Neighbour_D.IndexOf(value: s_LastCh)].ToString();
                
                    // ...East
                
                    southEast.Hash = Base32[index: Neighbour_C.IndexOf(value: s_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    southWest.Hash = Border_D.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent, direction: W) + Base32[index: Neighbour_D.IndexOf(value: s_LastCh)] :
                                     s_Parent                                      + Base32[index: Neighbour_D.IndexOf(value: s_LastCh)];
                
                    // ...East
                
                    southEast.Hash = Border_C.IndexOf(value: s_LastCh) != -1 ?
                                            GetNeighbour(geohash: s_Parent, direction: E) + Base32[index: Neighbour_C.IndexOf(value: s_LastCh)] :
                                            s_Parent                                      + Base32[index: Neighbour_C.IndexOf(value: s_LastCh)];
                }
            }
            else
            {
                if (s_Parent.Length == 0)
                {
                    // ...West
                
                    southWest.Hash = Base32[index: Neighbour_B.IndexOf(value: s_LastCh)].ToString();
                
                    // ...East
                
                    southEast.Hash = Base32[index: Neighbour_A.IndexOf(value: s_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    southWest.Hash = Border_B.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent, direction: W) + Base32[index: Neighbour_B.IndexOf(value: s_LastCh)] :
                                     s_Parent                                      + Base32[index: Neighbour_B.IndexOf(value: s_LastCh)];
                
                    // ...East
                
                    southEast.Hash = Border_A.IndexOf(value: s_LastCh) != -1 ?
                                            GetNeighbour(geohash: s_Parent, direction: E) + Base32[index: Neighbour_A.IndexOf(value: s_LastCh)] :
                                            s_Parent                                      + Base32[index: Neighbour_A.IndexOf(value: s_LastCh)];
                }
            }
        }


        //        public void UpdateNeighbours(GeoHash input,
//                                     ref GeoHash[] results)
//        {
//            GetNeighbours(geohash: Geocode,
//                          results: ref _neighbours);
//
//            for (var i = 0;
//                 i < results.Length;
//                 i++)
//            {
//                results[i]._geocode = _neighbours[i];
//            }
//        }


        //        [SerializeField]
//        public bool AutoUpdateNeighbours = false;

//        [SerializeField]
//        private string[] _neighbours = new string[9];
//        public string[] Neighbours => _neighbours;

        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            
            Precision = _precision;

            UpdateHash();
        }


        public void UpdateHash()
        {
            _hash = CoordinateToHash(latitude: _latitude,
                                  longitude: _longitude,
                                  precision: _precision);

//            if (AutoUpdateNeighbours) UpdateNeighbours();
        }

        

//        public void UpdateNeighbours() => GetNeighbours(geohash: _code,
//                                                        results: ref _neighbours);



        public GeoHash(double latitude,
                       double longitude) : base(latitude: latitude,
                                                longitude: longitude)
        {
            Latitude  = latitude;
            Longitude = longitude;

            UpdateHash();
        }


        public GeoHash(string geohash) => Hash = geohash;


        public GeoHash(Coordinate coordinate)
        {
            Latitude  = coordinate.Latitude;
            Longitude = coordinate.Longitude;

            UpdateHash();
        }
        
        public GeoHash() { }

        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- Static

        private const string Base32 = "0123456789bcdefghjkmnpqrstuvwxyz";

        private const int Base32_Char_BitSize = 5;

        public const int Default_Precision = 10;
        public const int Min_Precision     = 1;
        public const int Max_Precision     = 22;

        private const char N = 'n';
        private const char S = 's';
        private const char E = 'e';
        private const char W = 'w';
        
        private const string Neighbour_A = "p0r21436x8zb9dcf5h7kjnmqesgutwvy";
        private const string Neighbour_B = "14365h7k9dcfesgujnmqp0r2twvyx8zb";
        private const string Neighbour_C = "bc01fg45238967deuvhjyznpkmstqrwx";
        private const string Neighbour_D = "238967debc01fg45kmstqrwxuvhjyznp";

        private const string Border_A = "prxz";
        private const string Border_B = "028b";
        private const string Border_C = "bcfguvyz";
        private const string Border_D = "0145hjnp";

        private static readonly Dictionary<char,string[]> NeighbourLookup = new Dictionary<char, string[]>
                                                                            {
                                                                                { N, new[] { Neighbour_A, Neighbour_C } },
                                                                                { S, new[] { Neighbour_B, Neighbour_D } },
                                                                                { E, new[] { Neighbour_C, Neighbour_A } },
                                                                                { W, new[] { Neighbour_D, Neighbour_B } }
                                                                            };
        
        private static readonly Dictionary<char,string[]> BorderLookup = new Dictionary<char, string[]>
                                                                         {
                                                                             { N, new[] { Border_A, Border_C } },
                                                                             { S, new[] { Border_B, Border_D } },
                                                                             { E, new[] { Border_C, Border_A } },
                                                                             { W, new[] { Border_D, Border_B } }
                                                                         };

        private static bool InvalidLatLong(double latitude,
                                           double longitude) => latitude  < Min_Latitude  ||
                                                                latitude  > Max_Latitude  ||
                                                                longitude < Min_Longitude ||
                                                                longitude > Max_Longitude;

        

        public static string CoordinateToHash(double latitude,
                                           double longitude,
                                           int precision = Default_Precision)
        {
            var latMin = Min_Latitude;
            var latMax = Max_Latitude;

            var longMin = Min_Longitude;
            var longMax = Max_Longitude;

            precision = Mathf.Clamp(value: precision,
                                    min: Min_Precision,
                                    max: Max_Precision);

            var buffer = new char[precision];

            for (var i = 0;
                 i < precision;
                 i++)
            {
                var charIndex = 0;

                for (var j = 0;
                     j < Base32_Char_BitSize;
                     j++)
                {
                    if ((i * Base32_Char_BitSize + j) % 2 != 0)
                    {
                        // Latitude

                        var mid = (latMin + latMax) / 2;

                        if (latitude > mid)
                        {
                            charIndex = (charIndex << 1) + 1;

                            latMin = mid;
                        }
                        else
                        {
                            charIndex <<= 1;

                            latMax = mid;
                        }
                    }
                    else
                    {
                        // Longitude

                        var mid = (longMin + longMax) / 2;

                        if (longitude > mid)
                        {
                            charIndex = (charIndex << 1) + 1;

                            longMin = mid;
                        }
                        else
                        {
                            charIndex <<= 1;

                            longMax = mid;
                        }
                    }
                }

                buffer[i] = Base32[index: charIndex];
            }

            return new string(value: buffer);
        }

        public static (double, double) HashToCoordinate(string input)
        {
            // Get the boxing bounds of the hash
            
            var (southWestLat, southWestLong, northEastLat, northEastLong) = GetLatLongBounds(geohash: input);

            // And halve it to get the center lat/long point
            
            return ((southWestLat + northEastLat) * 0.5f, (southWestLong + northEastLong) * 0.5f);
        }

        public static void GetNeighbours(string geohash, ref string[] results)
        {
            var hashLength = geohash.Length;

            var lastCh = geohash[index: hashLength - 1];

            var parent = geohash.Substring(startIndex: 0,
                                           length: hashLength - 1);

            if (results.Length != 9) results = new string[9];

            results[4] = geohash; // Set the center entry since we already have it
            
            string n; // cache the north result for later use with north-west and north-east

            string s; // cache the south result for later use with south-west and south-east
            
            // Per-direction

            if (hashLength % 2 == 0)
            {
                // Even

                if (parent.Length == 0)
                {
                    // North

                    n = results[1] = Base32[index: Neighbour_A.IndexOf(value: lastCh)].ToString();
                
                    // West
                
                    results[3] = Base32[index: Neighbour_D.IndexOf(value: lastCh)].ToString();
                
                    // East

                    results[5] = Base32[index: Neighbour_C.IndexOf(value: lastCh)].ToString();
                
                    // South

                    s = results[7] = Base32[index: Neighbour_B.IndexOf(value: lastCh)].ToString();
                }
                else
                {
                    // North

                    n = results[1] = Border_A.IndexOf(value: lastCh) != -1 ?
                                      GetNeighbour(geohash: parent,
                                                   direction: N) + Base32[index: Neighbour_A.IndexOf(value: lastCh)] :
                                      parent + Base32[index: Neighbour_A.IndexOf(value: lastCh)];
                
                    // West

                    results[3] = Border_D.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent,
                                                  direction: W) + Base32[index: Neighbour_D.IndexOf(value: lastCh)] :
                                     parent + Base32[index: Neighbour_D.IndexOf(value: lastCh)];
                
                    // East

                    results[5] = Border_C.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent,
                                                  direction: E) + Base32[index: Neighbour_C.IndexOf(value: lastCh)] :
                                     parent + Base32[index: Neighbour_C.IndexOf(value: lastCh)];
                
                    // South

                    s = results[7] = Border_B.IndexOf(value: lastCh) != -1 ?
                                      GetNeighbour(geohash: parent,
                                                   direction: S) + Base32[index: Neighbour_B.IndexOf(value: lastCh)] :
                                      parent + Base32[index: Neighbour_B.IndexOf(value: lastCh)];
                }
                
            }
            else
            {
                // Odd
                
                if (parent.Length == 0)
                {
                    // North

                    n = results[1] = Base32[index: Neighbour_C.IndexOf(value: lastCh)].ToString();

                    // West
                
                    results[3] = Base32[index: Neighbour_B.IndexOf(value: lastCh)].ToString();
                
                    // East

                    results[5] = Base32[index: Neighbour_A.IndexOf(value: lastCh)].ToString();
                
                    // South

                    s = results[7] = Base32[index: Neighbour_D.IndexOf(value: lastCh)].ToString();
                }
                else
                {
                    // North

                    n = results[1] = Border_C.IndexOf(value: lastCh) != -1 ?
                                      GetNeighbour(geohash: parent,
                                                   direction: N) + Base32[index: Neighbour_C.IndexOf(value: lastCh)] :
                                      parent + Base32[index: Neighbour_C.IndexOf(value: lastCh)];
                
                    // West

                    results[3] = Border_B.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent,
                                                  direction: W) + Base32[index: Neighbour_B.IndexOf(value: lastCh)] :
                                     parent + Base32[index: Neighbour_B.IndexOf(value: lastCh)];
                
                    // East

                    results[5] = Border_A.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent,
                                                  direction: E) + Base32[index: Neighbour_A.IndexOf(value: lastCh)] :
                                     parent + Base32[index: Neighbour_A.IndexOf(value: lastCh)];
                
                    // South

                    s = results[7] = Border_D.IndexOf(value: lastCh) != -1 ?
                                     GetNeighbour(geohash: parent,
                                                  direction: S) + Base32[index: Neighbour_D.IndexOf(value: lastCh)] :
                                     parent + Base32[index: Neighbour_D.IndexOf(value: lastCh)];
                }

            }
            
            // Now you can do NW/NE/SW/SE by referring to the already existing entries...

            // North...

            var n_HashLength = n.Length;

            var n_LastCh = n[index: hashLength - 1];

            var n_Parent = n.Substring(startIndex: 0,
                                      length: n_HashLength - 1);
            
            var n_EvenHashLength = n_HashLength % 2 == 0;

            if (n_EvenHashLength)
            {
                if (n_Parent.Length == 0)
                {
                    // ...West
                
                    results[0] = Base32[index: Neighbour_D.IndexOf(value: n_LastCh)].ToString();
                
                    // ...East
                
                    results[2] = Base32[index: Neighbour_C.IndexOf(value: n_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    results[0] = Border_D.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent,
                                                  direction: W) + Base32[index: Neighbour_D.IndexOf(value: n_LastCh)] :
                                     n_Parent + Base32[index: Neighbour_D.IndexOf(value: n_LastCh)];
                
                    // ...East
                
                    results[2] = Border_C.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent,
                                                  direction: E) + Base32[index: Neighbour_C.IndexOf(value: n_LastCh)] :
                                     n_Parent + Base32[index: Neighbour_C.IndexOf(value: n_LastCh)];
                }
            }
            else
            {
                if (n_Parent.Length == 0)
                {
                    // ...West
                
                    results[0] = Base32[index: Neighbour_B.IndexOf(value: n_LastCh)].ToString();
                
                    // ...East
                
                    results[2] = Base32[index: Neighbour_A.IndexOf(value: n_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    results[0] = Border_B.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent,
                                                  direction: W) + Base32[index: Neighbour_B.IndexOf(value: n_LastCh)] :
                                     n_Parent + Base32[index: Neighbour_B.IndexOf(value: n_LastCh)];
                
                    // ...East
                
                    results[2] = Border_A.IndexOf(value: n_LastCh) != -1 ?
                                     GetNeighbour(geohash: n_Parent,
                                                  direction: E) + Base32[index: Neighbour_A.IndexOf(value: n_LastCh)] :
                                     n_Parent + Base32[index: Neighbour_A.IndexOf(value: n_LastCh)];
                }
            }


            // South...
            
            var s_HashLength = s.Length;

            var s_LastCh = s[index: hashLength - 1];

            var s_Parent = s.Substring(startIndex: 0,
                                       length: s_HashLength - 1);
            
            var s_EvenHashLength = s_HashLength % 2 == 0;

            if (s_EvenHashLength)
            {
                if (s_Parent.Length == 0)
                {
                    // ...West
                
                    results[6] = Base32[index: Neighbour_D.IndexOf(value: s_LastCh)].ToString();
                
                    // ...East
                
                    results[8] = Base32[index: Neighbour_C.IndexOf(value: s_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    results[6] = Border_D.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent,
                                                  direction: W) + Base32[index: Neighbour_D.IndexOf(value: s_LastCh)] :
                                     s_Parent + Base32[index: Neighbour_D.IndexOf(value: s_LastCh)];
                
                    // ...East
                
                    results[8] = Border_C.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent,
                                                  direction: E) + Base32[index: Neighbour_C.IndexOf(value: s_LastCh)] :
                                     s_Parent + Base32[index: Neighbour_C.IndexOf(value: s_LastCh)];
                }
            }
            else
            {
                if (s_Parent.Length == 0)
                {
                    // ...West
                
                    results[6] = Base32[index: Neighbour_B.IndexOf(value: s_LastCh)].ToString();
                
                    // ...East
                
                    results[8] = Base32[index: Neighbour_A.IndexOf(value: s_LastCh)].ToString();
                }
                else
                {
                    // ...West
                
                    results[6] = Border_B.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent,
                                                  direction: W) + Base32[index: Neighbour_B.IndexOf(value: s_LastCh)] :
                                     s_Parent + Base32[index: Neighbour_B.IndexOf(value: s_LastCh)];
                
                    // ...East
                
                    results[8] = Border_A.IndexOf(value: s_LastCh) != -1 ?
                                     GetNeighbour(geohash: s_Parent,
                                                  direction: E) + Base32[index: Neighbour_A.IndexOf(value: s_LastCh)] :
                                     s_Parent + Base32[index: Neighbour_A.IndexOf(value: s_LastCh)];
                }
            }
        }

        // This is the original method, converted from JS @ github.com/davetroy/geohash-js
        public static string GetNeighbour(string geohash,
                                          char direction)
        {

            var hashLength = geohash.Length;

            var lastCh = geohash[index: hashLength - 1];

            var parent = geohash.Substring(startIndex: 0,
                                           length: hashLength - 1);
            
            var evenHashLength = hashLength % 2;

            if (BorderLookup[key: direction][evenHashLength].IndexOf(value: lastCh) != -1 &&
                parent                                                  != string.Empty)
            {
                parent = GetNeighbour(geohash: parent,
                                      direction: direction);
            }

            return parent + Base32[index: NeighbourLookup[key: direction][evenHashLength].IndexOf(value: lastCh)];
        }

        /// <summary>
        ///     Returns SW/NE latitude/longitude bounds of specified geohash.
        /// </summary>
        public static(double,double,double,double) GetLatLongBounds(string geohash)
        {
            var evenBit = true;
            
            var latMin  = Min_Latitude;
            var latMax  = Max_Latitude;
            var lonMin  = Min_Longitude;
            var lonMax  = Max_Longitude;

            foreach (var idx in geohash.Select(selector: chr => Base32.IndexOf(value: chr)))
            {
                if (idx == -1)
                {
                    LogError(message: "Invalid geohash");

                    return default;
                }

                for (var n = 4;
                     n >= 0;
                     n--)
                {
                    var bitN = idx >> n & 1;

                    if (evenBit)
                    {
                        // Longitude
                        
                        var lonMid = (lonMin + lonMax) / 2;

                        if (bitN == 1)
                        {
                            lonMin = lonMid;
                        }
                        else
                        {
                            lonMax = lonMid;
                        }
                    }
                    else
                    {
                        // Latitude
                        
                        var latMid = (latMin + latMax) / 2;

                        if (bitN == 1)
                        {
                            latMin = latMid;
                        }
                        else
                        {
                            latMax = latMid;
                        }
                    }

                    evenBit = !evenBit;
                }
            }

            return (latMin, lonMin, latMax, lonMax);
        }

    }

}