using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace Vanilla.Geocodes
{

    public class WorldGridTest : MonoBehaviour
    {

        [Header("Settings")]
        public bool useV1 = true;
        public int  precision;

        [Header("Hash To LatLong")]
        public string inputHash;
        public double outputLatitude;
        public double outputLongitude;
        
        [Header("LatLong To Hash")]
        public double inputLatitude;
        public double inputLongitude;
        public int    outputRow;
        public int    outputColumn;
        public string outputHash;

        [Header("Debug")]
        public double latitudeRange;
        public double longitudeRange;
        
        public double latitudeCellSize;
        public double longitudeCellSize;

        void OnValidate()
        {
            #if UNITY_EDITOR
            latitudeRange = useV1 ?
                                Mojiten1.LATITUDE_MAX - Mojiten1.LATITUDE_MIN :
                                Mojiten2.LATITUDE_MAX - Mojiten2.LATITUDE_MIN;
            
            longitudeRange = useV1 ?
                                Mojiten1.LONGITUDE_MAX - Mojiten1.LONGITUDE_MIN :
                                Mojiten2.LONGITUDE_MAX - Mojiten2.LONGITUDE_MIN;

            latitudeCellSize = latitudeRange /
                               (useV1 ?
                                    Mojiten1.GridSize :
                                    Mojiten2.Latitude_Rows);

            longitudeCellSize = longitudeRange /
                                (useV1 ?
                                     Mojiten1.GridSize :
                                     Mojiten2.Longitude_Columns);
            
            ToHash();
            ToLatLong();
            #endif
        }

//        public List<string> neighbours;


        [ContextMenu("To Hash")]
        public void ToHash()
        {
            var (row, column, geocode) = useV1 ?
                                             Mojiten1.Encode(inputLatitude,
                                                             inputLongitude,
                                                             precision) :
                                             Mojiten2.Encode(inputLatitude,
                                                             inputLongitude,
                                                             precision);

            outputRow    = row;
            outputColumn = column;
            outputHash   = geocode;
        }


        [ContextMenu("To LatLong")]
        public void ToLatLong()
        {
            var (lat, lon) = useV1 ?
                                 Mojiten1.Decode(inputHash) :
                                 Mojiten2.Decode(inputHash);

            outputLatitude  = lat;
            outputLongitude = lon;
        }


//        [ContextMenu("Get Neighbours")]
//        public void GetNeighbours()
//        {
////            neighbours = WorldGrid.GetNeighboringCells(hash);
//        }

    }
//
//    public class WorldGrid
//    {
//
//        //        private static readonly double EarthRadius  = 6371.0; // Radius of the earth in KM
//
////        private static readonly string GeoCodeChars = "abcdefghijklmnop";
////        private static readonly int    GridSize     = 4;      // Since you've specified 16 cells, we use a 4x4 grid.
//
//        //        private static readonly string GeoCodeChars = "0123456789abcdefghijklmnopqrstuvwxyz";
////        private static readonly int    GridSize     = 6;
//
////    Assuming a GridSize of 8...
////
////    Longitude x Latitude
////
////    1 character: (20,037,500m     / 8) = 5,009,375m x   2,504,687.5m
////    2 characters: (2,504,687.5m   / 8) = 626,172m   x   313,086m
////    3 characters: (313,086m       / 8) = 78,272m    x   39,136m
////    4 characters: (39,136m        / 8) = 9,784m     x   4,892m
////    5 characters: (4,892m         / 8) = 1,223m     x   611.5m
////    6 characters: (611.5m         / 8) = 153m       x   76.4m
////    7 characters: (76.4m          / 8) = 19m        x   9.5m
////    8 characters: (9.5m           / 8) = 2.4m       x   1.2m
////    9 characters: (1.2m           / 8) = 0.3m       x   0.15m
////
////      You could get these uniform if you can double longitudes grid size (but not latitude)
////      If you can get to 128 characters in the set, that would make the cell sizes uniform again.
//
////        private static readonly string GeoCodeChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_+";
////        private static readonly int GridSize = 8;
//        
//        //•
//        // ぁあ
//
////        private static readonly string GeoCodeChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_+-!$&'()*,;=:@[]^`{|}~";
//
////        private static readonly string GeoCodeChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_+-!$&'()*,;=:@[]^`{|}~•◦●○®©℗™℠ºª‣▴☑︎";
////        private static readonly int GridSize = 10; // 10x10 grid
//
//        private const string CHARS_Numbers   = "0123456789";
//        
//        private const string CHARS_LowerCase = "abcdefghijklmnopqrstuvwxyz";
//        private const string CHARS_UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//        
////        private const string CHARS_Symbols = "_+-^$";
////        private const string CHARS_Unicode = "■●❤◆▶◀▲▼★";
//
//        #region Emojis (Doesn't work :()
////        private const string CHARS_EMOJI_VEGE      = "🥕🍄🌽🍆🍅🥦🌶";
//
////        private const string CHARS_EMOJI_FACES     = "🙂😂😍😭🤔😱😡😑😵😴😳😎";
////        private const string CHARS_EMOJI_FRUIT     = "🍌🍎🍊🍋🍑🍒🍉🍍🍓🍐🍇🥝";
////        private const string CHARS_EMOJI_FOOD      = "🌮🍰🍟🌭🧀🍗🍫🍦🍔🍖🍿🍝";
////        private const string CHARS_EMOJI_ANIMALS   = "🐱🐶🙉🐍🦅🐳🦀🐄🐥🐓🐸🐁";
////        private const string CHARS_EMOJI_WEATHER   = "🔆🌙🌈🌪❄⛄🌨⛱⚡💧🌡🌀";
////        private const string CHARS_EMOJI_SPORTS    = "⚽🏀🏈🎾⚾🎳🎱🏓🏹⛳🛹🥊";
////        private const string CHARS_EMOJI_TRANSPORT = "🚁✈️🚂🚲🏍️🚀⛵🏎️🚒🚑🚓🚗";
////        private const string CHARS_EMOJI_MUSIC     = "🎺🎵🎤🎷🎸🪕🎹🥁🎻💿🔈🎧";
//        
////        private const string CHARS_EMOJI_FACES     = "🙂😂😍😭🤔😱😡😵😴😎";
////        private const string CHARS_EMOJI_FRUIT     = "🍌🍎🍊🍋🍒🍉🍍🍓🍐🍇";
////        private const string CHARS_EMOJI_FOOD      = "🌮🍰🍟🌭🧀🍫🍦🍔🍿🍝";
////        private const string CHARS_EMOJI_ANIMALS   = "🐈🐕🐒🐍🦅🐳🦀🐄🐁🐸";
////        private const string CHARS_EMOJI_WEATHER   = "🔆🌙🌈🌪⛄🌨⛱💧🌡🌀";
////        private const string CHARS_EMOJI_SPORTS    = "⚽🏀🏈🎾⚾🎳🎱🏓🏹🛹";
////        private const string CHARS_EMOJI_TRANSPORT = "🚁🛩🚂🚲🚀⛵🏎🚒🚑🚓";
////        private const string CHARS_EMOJI_MUSIC     = "🎺🎵🎤🎷🎸🎹🥁💿🔈🎧";
////        private const string CHARS_EMOJI_MISC_1    = "🧲💣🪓🔫⏰🔨💎💊🔑🔍";
////        private const string CHARS_EMOJI_MISC_2    = "🧨💡⏳🧸🪩📬💰📷📞🗿";
//#endregion
//        
////        private const string CHARS_Hiragana  = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん";
////        private const string CHARS_Katakana  = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン";
//        
//        private const string HIRAGANA = "あいうえおかがきぎくぐけげこごさざしじすずせぜそぞただちぢつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもやゆよらりるれろわをん";
//        private const string KATAKANA = "アイウエオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモヤユヨラリルレロワヲン";
//
//        private const string KANA_MISC = "ー。";
//        
//
////        private const string CHARS_Greek_Upper = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"; // I'd remove A + B since they're not visually distinct from English uppercase A + B
//        
//
//        private const string GeoCodeChars = HIRAGANA + KATAKANA + KANA_MISC; // 144! (12x12)
//        private static readonly int GridSize = 12; // 12x12 grid
//
////        private const           string GeoCodeChars = CHARS_EMOJI_FACES + CHARS_EMOJI_FRUIT + CHARS_EMOJI_FOOD + CHARS_EMOJI_ANIMALS + CHARS_EMOJI_WEATHER + CHARS_EMOJI_SPORTS +  CHARS_EMOJI_TRANSPORT + CHARS_EMOJI_MUSIC + CHARS_EMOJI_MISC_1 + CHARS_EMOJI_MISC_2;
////        private static readonly int    GridSize     = 10; // 12x12 grid
////
//        public static string Encode(double latitude, double longitude, int precision)
//        {
//            double minLat  = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
//
//            latitude = Math.Clamp(latitude,
//                                  minLat,
//                                  maxLat);
//            
//            var geocode = "";
//
//            for (var i = 0; i < precision; i++)
//            {
////                var latStep = (maxLat - minLat) / GridSizeLat;
////                var lonStep = (maxLon - minLon) / GridSizeLon;
//                var latStep = (maxLat - minLat) / GridSize;
//                var lonStep = (maxLon - minLon) / GridSize;
//
//                var row    = (int)((maxLat    - latitude) / latStep);
//                var column = (int)((longitude - minLon)   / lonStep);
//
////                var index = row * GridSizeLon + column;
//                var index = row * GridSize + column;
//                geocode += GeoCodeChars[index];
//
//                maxLat -= row * latStep;
//                minLat =  maxLat - latStep;
//                minLon += column * lonStep;
//                maxLon =  minLon + lonStep;
//            }
//            
//            return geocode;
////
////            // Adjust the latitude range from -70 to 70 instead of -90 to 90
//////            latitude = Math.Max(-70.0, Math.Min(70.0, latitude));
//////    
//////            // Setup initial conditions
//////            double minLat  = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
//////            var    geoCode = new StringBuilder();
//////
//////            for (int i = 0; i < precision; i++)
//////            {
//////                int latIndex = 0, lonIndex = 0;
//////
//////                for (int bit = 0; bit < GridSize; bit++)
//////                {
//////                    double midLat = (minLat + maxLat) / 2.0;
//////                    double midLon = (minLon + maxLon) / 2.0;
//////
//////                    if (latitude > midLat) // upper half of grid
//////                    {
//////                        latIndex |= (1 << (GridSize - bit - 1));
//////                        minLat   =  midLat;
//////                    }
//////                    else // lower half of grid
//////                    {
//////                        maxLat = midLat;
//////                    }
//////
//////                    if (longitude > midLon) // right half of grid
//////                    {
//////                        lonIndex |= (1 << (GridSize - bit - 1));
//////                        minLon   =  midLon;
//////                    }
//////                    else // left half of grid
//////                    {
//////                        maxLon = midLon;
//////                    }
//////                }
//////
//////                // combine the latitude and longitude bits and convert to a character
//////                geoCode.Append(GeoCodeChars[latIndex * GridSize + lonIndex]);
//////            }
//////
//////            return geoCode.ToString();
//        }
//
////        private const string GeoCodeChars = CHARS_Numbers + CHARS_Hiragana + CHARS_Katakana; // 102
//
////        private static readonly int GridSizeLat = 8;    // 140.0 total latitude     / 8     = 17.5
////        private static readonly int GridSizeLon = 12;   // 360.0 total longitude    / 12    = 30
//
////        private static readonly int GridSizeLat = 6;  // 140.0 total latitude     / 6     = 23.333
////        private static readonly int GridSizeLon = 16; // 360.0 total longitude    / 16    = 22.5
////        
////        public static string Encode(double lat, double lon, int precision = 5)
////        {
////            double minLat  = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
////            var    geoCode = new StringBuilder();
////
////            for (int i = 0; i < precision; i++)
////            {
////                int latIndex = 0, lonIndex = 0;
////
////                for (int bit = 0; bit < GridSizeLat; bit++)
////                {
////                    double midLat = (minLat + maxLat) / 2.0;
////
////                    if (lat > midLat)
////                    {
////                        latIndex |= (1 << (GridSizeLat - bit - 1));
////                        minLat   =  midLat;
////                    }
////                    else
////                    {
////                        maxLat = midLat;
////                    }
////                }
////
////                for (int bit = 0; bit < GridSizeLon; bit++)
////                {
////                    double midLon = (minLon + maxLon) / 2.0;
////
////                    if (lon > midLon)
////                    {
////                        lonIndex |= (1 << (GridSizeLon - bit - 1));
////                        minLon   =  midLon;
////                    }
////                    else
////                    {
////                        maxLon = midLon;
////                    }
////                }
////
////                geoCode.Append(GeoCodeChars[latIndex * GridSizeLon + lonIndex]);
////            }
////
////            return geoCode.ToString();
////        }
//
//        public static(double, double) Decode(string geocode)
//        {
//            double minLat = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
//
//            foreach (var character in geocode)
//            {
////                var latStep = (maxLat - minLat) / GridSizeLat;
////                var lonStep = (maxLon - minLon) / GridSizeLon;
//                var latStep = (maxLat - minLat) / GridSize;
//                var lonStep = (maxLon - minLon) / GridSize;
//
//                var index = GeoCodeChars.IndexOf(character);
//                
////                var row    = index / GridSizeLon;
////                var column = index % GridSizeLon;
//                var row    = index / GridSize;
//                var column = index % GridSize;
//
//                maxLat -= row * latStep;
//                minLat =  maxLat - latStep;
//                minLon += column * lonStep;
//                maxLon =  minLon + lonStep;
//            }
//
//            double latitude  = (minLat + maxLat) / 2.0;
//            double longitude = (minLon + maxLon) / 2.0;
//
//            return (latitude, longitude);
//        }
//
//
////
////        public static(double, double) Decode(string geocode)
////        {
////            double minLat = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
////
////            foreach (var character in geocode)
////            {
////                var latStep = (maxLat - minLat) / GridSizeLat;
////                var lonStep = (maxLon - minLon) / GridSizeLon;
////
////                var index = GeoCodeChars.IndexOf(character);
////
////                var row    = index / GridSizeLon;
////                var column = index % GridSizeLon;
////
////                maxLat -= row * latStep;
////                minLat =  maxLat - latStep;
////                minLon += column * lonStep;
////                maxLon =  minLon + lonStep;
////            }
////
////            double latitude  = (minLat + maxLat) / 2.0;
////            double longitude = (minLon + maxLon) / 2.0;
////
////            return (latitude, longitude);
////        }
////        
////        public static (double, double) Decode(string geoCode)
////        {
////            double minLat = -70.0, maxLat = 70.0, minLon = -180.0, maxLon = 180.0;
////
////            foreach (char c in geoCode)
////            {
////                int index    = GeoCodeChars.IndexOf(c);
////                int latIndex = index / GridSizeLon;
////                int lonIndex = index % GridSizeLon;
////
////                for (int bit = 0; bit < GridSizeLat; bit++)
////                {
////                    double midLat = (minLat + maxLat) / 2.0;
////
////                    if ((latIndex & (1 << (GridSizeLat - bit - 1))) != 0)
////                    {
////                        minLat = midLat;
////                    }
////                    else
////                    {
////                        maxLat = midLat;
////                    }
////                }
////
////                for (int bit = 0; bit < GridSizeLon; bit++)
////                {
////                    double midLon = (minLon + maxLon) / 2.0;
////
////                    if ((lonIndex & (1 << (GridSizeLon - bit - 1))) != 0)
////                    {
////                        minLon = midLon;
////                    }
////                    else
////                    {
////                        maxLon = midLon;
////                    }
////                }
////            }
////
////            double lat = (minLat + maxLat) / 2.0;
////            double lon = (minLon + maxLon) / 2.0;
////
////            return (lat, lon);
////        }
//
//        // So close but this ones broken :(
////        public static List<string> GetNeighboringCells(string geoCode)
////        {
////            List<string> neighbors = new List<string>();
////
////            var original = Decode(geoCode);
////
////            // Approximate distance between cells at this level of precision
////            double deltaLong = (40_075_000 / Math.Pow(GridSize, geoCode.Length)) * (180.0 / 360.0);
////            double deltaLat  = (20_037_500 / Math.Pow(GridSize, geoCode.Length)) * (180.0 / 360.0);
////
////            // Add small offset in each cardinal direction
////            List<(double, double)> offsets = new List<(double, double)>
////                                             {
////                                                 (deltaLat, 0),           // N
////                                                 (deltaLat, deltaLong),   // NE
////                                                 (0, deltaLong),          // E
////                                                 (-deltaLat, deltaLong),  // SE
////                                                 (-deltaLat, 0),          // S
////                                                 (-deltaLat, -deltaLong), // SW
////                                                 (0, -deltaLong),         // W
////                                                 (deltaLat, -deltaLong),  // NW
////                                             };
////
////            foreach(var offset in offsets)
////            {
////                double newLat  = original.Item1 + offset.Item1;
////                double newLong = original.Item2 + offset.Item2;
////
////                // Correct for longitude overflow
////                if (newLong > 180)
////                    newLong = newLong - 360;
////                else if (newLong < -180)
////                    newLong = newLong + 360;
////
////                // Correct for latitude overflow
////                if (newLat > 90)
////                    newLat = 90;
////                else if (newLat < -90)
////                    newLat = -90;
////
////                string neighborGeoCode = Encode(newLat, newLong, geoCode.Length);
////                neighbors.Add(neighborGeoCode);
////            }
////
////            return neighbors;
////        }
//
//    }

}