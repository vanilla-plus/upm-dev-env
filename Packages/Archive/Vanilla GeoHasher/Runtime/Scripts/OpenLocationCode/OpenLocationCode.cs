using System;
using System.Text;

using Google.OpenLocationCode;

using UnityEngine;

namespace Vanilla.Geocodes
{

    [Serializable]
    public class OpenLocationCode : GeoCode,
                                    ISerializationCallbackReceiver
    {

        public OpenLocationCode(double latitude,
                                double longitude) : base(latitude,
                                                         longitude) { }


        public OpenLocationCode(string plusCode) : base(plusCode) { }


        protected override string Encode(double latitude,
                                         double longitude) => OpenLocationCodeUtility.Encode(latitude,
                                                                                             longitude,
                                                                                             Default_Precision);


        protected override string Encode(double latitude,
                                         double longitude,
                                         int precision) => OpenLocationCodeUtility.Encode(latitude,
                                                                                          longitude,
                                                                                          Precision);


        protected override(double latitude, double longitude) Decode(string hash)
        {
            var code = OpenLocationCodeUtility.Decode(hash);

            _precision = code.CodeLength;

            return (code.Center.Latitude, code.Center.Longitude);
        }


        // OpenLocationCode precision must be a multiple of 2 between the range of 2 <-> 20!

        public override int Default_Precision => 4;

        public override int Min_Precision => 2;

        public override int Max_Precision => 16;

        public override int Precision_Factor => 2;

        public void OnBeforeSerialize() { }

        public virtual void OnAfterDeserialize() => Validate();


        // Flex time
//        
//        public static string Encode2(double latitude,
//                                    double longitude,
//                                    int precision)
//        {
//            // Limit the maximum number of digits in the code.
////            precision = Math.Min(precision,
////                                  MaxDigitCount);
//
////            Check that the code length requested is valid.
////            if (precision < 2 ||
////                (precision < PairCodeLength && precision % 2 == 1))
////            {
////                throw new ArgumentException($"Illegal code length {precision}.");
////            }
//
//            // Ensure that latitude and longitude are valid.
//            latitude = Math.Min(Math.Max(latitude, -Max_Latitude), Max_Latitude);
//            
//            while (longitude < -Max_Longitude) {
//                longitude += Max_Longitude * 2;
//            }
//            while (longitude >= Max_Longitude) {
//                longitude -= Max_Longitude * 2;
//            }
//
//            // Latitude 90 needs to be adjusted to be just less, so the returned code can also be decoded.
//            if (Math.Abs((int) latitude - Max_Latitude) < Mathf.Epsilon)
//            {
//                latitude -= 0.9 * ComputeLatitudePrecision(precision);
//            }
//
//            // Store the code - we build it in reverse and reorder it afterwards.
//            StringBuilder reverseCodeBuilder = new StringBuilder();
//
//            // Compute the code.
//            // This approach converts each value to an integer after multiplying it by
//            // the final precision. This allows us to use only integer operations, so
//            // avoiding any accumulation of floating point representation errors.
//
//            // Multiply values by their precision and convert to positive. Rounding
//            // avoids/minimises errors due to floating point precision.
//            long latVal = (long) (Math.Round((latitude  + Max_Latitude) * LatIntegerMultiplier * 1e6) / 1e6);
//            long lngVal = (long) (Math.Round((longitude + Max_Longitude) * LngIntegerMultiplier * 1e6) / 1e6);
//
//            if (precision > PairCodeLength)
//            {
//                for (int i = 0;
//                     i < GridCodeLength;
//                     i++)
//                {
//                    long latDigit = latVal % GridRows;
//                    long lngDigit = lngVal % GridColumns;
//                    int  ndx      = (int) (latDigit * GridColumns + lngDigit);
//                    reverseCodeBuilder.Append(CodeAlphabet[ndx]);
//                    latVal /= GridRows;
//                    lngVal /= GridColumns;
//                }
//            }
//            else
//            {
//                latVal /= GridRowsMultiplier;
//                lngVal /= GridColumnsMultiplier;
//            }
//
//            // Compute the pair section of the code.
//            for (int i = 0;
//                 i < PairCodeLength / 2;
//                 i++)
//            {
//                reverseCodeBuilder.Append(CodeAlphabet[(int) (lngVal % EncodingBase)]);
//                reverseCodeBuilder.Append(CodeAlphabet[(int) (latVal % EncodingBase)]);
//                latVal /= EncodingBase;
//                lngVal /= EncodingBase;
//
//                // If we are at the separator position, add the separator.
//                if (i == 0)
//                {
//                    reverseCodeBuilder.Append(SeparatorCharacter);
//                }
//            }
//
//            // Reverse the code.
//            char[] reversedCode = reverseCodeBuilder.ToString().ToCharArray();
//            Array.Reverse(reversedCode);
//            StringBuilder codeBuilder = new StringBuilder(new string(reversedCode));
//
//            // If we need to pad the code, replace some of the digits.
//            if (precision < SeparatorPosition)
//            {
//                codeBuilder.Remove(precision,
//                                   SeparatorPosition - precision);
//
//                for (int i = precision;
//                     i < SeparatorPosition;
//                     i++)
//                {
//                    codeBuilder.Insert(i,
//                                       PaddingCharacter);
//                }
//            }
//
//            return codeBuilder.ToString(0,
//                                        Math.Max(SeparatorPosition + 1,
//                                                 precision        + 1));
//        }
//        
//        /// <summary>
//        /// Provides a normal precision code, approximately 14x14 meters.<br/>
//        /// Used to specify encoded code length (<see cref="Encode(double,double,int)"/>)
//        /// </summary>
//        public const int CodePrecisionNormal = 10;
//
//        /// <summary>
//        /// Provides an extra precision code length, approximately 2x3 meters.<br/>
//        /// Used to specify encoded code length (<see cref="Encode(double,double,int)"/>)
//        /// </summary>
////        public const int CodePrecisionExtra = 11;
//
//
//        // A separator used to break the code into two parts to aid memorability.
//        private const char SeparatorCharacter = '+';
//
//        // The number of characters to place before the separator.
//        private const int SeparatorPosition = 8;
//
//        // The character used to pad codes.
//        private const char PaddingCharacter = '0';
//
//        // The character set used to encode the digit values.
//        internal const string CodeAlphabet = "23456789CFGHJMPQRVWX";
//
//        // The base to use to convert numbers to/from.
//        private const int EncodingBase = 20; // CodeAlphabet.Length;
//
//        // The encoding base squared also rep
//        private const int EncodingBaseSquared = EncodingBase * EncodingBase;
//
//        // The maximum value for latitude in degrees.
////        private const int LatitudeMax = 90;
//
//        // The maximum value for longitude in degrees.
////        private const int LongitudeMax = 180;
//
//        // Maximum code length using just lat/lng pair encoding.
//        private const int PairCodeLength = 10;
//
//        // Number of digits in the grid coding section.
//        private const int GridCodeLength = MaxDigitCount - PairCodeLength;
//
//        // Maximum code length for any plus code
//        private const int MaxDigitCount = 15;
//
//        // Number of columns in the grid refinement method.
//        private const int GridColumns = 4;
//
//        // Number of rows in the grid refinement method.
//        private const int GridRows = 5;
//
//        // The maximum latitude digit value for the first grid layer
//        private const int FirstLatitudeDigitValueMax = 8; // lat -> 90
//
//        // The maximum longitude digit value for the first grid layer
//        private const int FirstLongitudeDigitValueMax = 17; // lon -> 180
//
//
//        private const long GridRowsMultiplier = 3125; // Pow(GridRows, GridCodeLength)
//
//        private const long GridColumnsMultiplier = 1024; // Pow(GridColumns, GridCodeLength)
//
//        // Value to multiple latitude degrees to convert it to an integer with the maximum encoding
//        // precision. I.e. ENCODING_BASE**3 * GRID_ROWS**GRID_CODE_LENGTH
//        private const long LatIntegerMultiplier = 8000 * GridRowsMultiplier;
//
//        // Value to multiple longitude degrees to convert it to an integer with the maximum encoding
//        // precision. I.e. ENCODING_BASE**3 * GRID_COLUMNS**GRID_CODE_LENGTH
//        private const long LngIntegerMultiplier = 8000 * GridColumnsMultiplier;
//
//        // Value of the most significant latitude digit after it has been converted to an integer.
//        private const long LatMspValue = LatIntegerMultiplier * EncodingBaseSquared;
//
//        // Value of the most significant longitude digit after it has been converted to an integer.
//        private const long LngMspValue = LngIntegerMultiplier * EncodingBaseSquared;
//
//
//
//        // The ASCII integer of the minimum digit character used as the offset for indexed code digits
//        private static readonly int IndexedDigitValueOffset = CodeAlphabet[0]; // 50
//
//        // The digit values indexed by the character ASCII integer for efficient lookup of a digit value by its character
//        private static readonly int[] IndexedDigitValues = new int[CodeAlphabet[CodeAlphabet.Length - 1] - IndexedDigitValueOffset + 1]; // int[38]
//
//        
//        private static double ComputeLatitudePrecision(int codeLength) => codeLength <= CodePrecisionNormal ?
//                                                                              Math.Pow(EncodingBase,
//                                                                                       codeLength / -2 + 2) :
//                                                                              Math.Pow(EncodingBase,
//                                                                                       -3) /
//                                                                              Math.Pow(GridRows,
//                                                                                       codeLength - PairCodeLength);
//        
//        internal static string TrimCode(string code) {
//            StringBuilder codeBuilder = new StringBuilder();
//            foreach (char c in code) {
//                if (c != PaddingCharacter && c != SeparatorCharacter) {
//                    codeBuilder.Append(c);
//                }
//            }
//            return codeBuilder.Length != code.Length ? codeBuilder.ToString() : code;
//        }
//        
//        private CodeArea DecodeValid(string codeDigits) {
//            // Initialise the values. We work them out as integers and convert them to doubles at the end.
//            long latVal = (long) (-Max_Latitude  * LatIntegerMultiplier);
//            long lngVal = (long) (-Max_Longitude * LngIntegerMultiplier);
//            // Define the place value for the digits. We'll divide this down as we work through the code.
//            long latPlaceVal = LatMspValue;
//            long lngPlaceVal = LngMspValue;
//
//            int pairPartLength = Math.Min(codeDigits.Length, PairCodeLength);
//            int codeLength     = Math.Min(codeDigits.Length, MaxDigitCount);
//            for (int i = 0; i < pairPartLength; i += 2) {
//                latPlaceVal /= EncodingBase;
//                lngPlaceVal /= EncodingBase;
//                latVal      += DigitValueOf(codeDigits[i])     * latPlaceVal;
//                lngVal      += DigitValueOf(codeDigits[i + 1]) * lngPlaceVal;
//            }
//            for (int i = PairCodeLength; i < codeLength; i++) {
//                latPlaceVal /= GridRows;
//                lngPlaceVal /= GridColumns;
//                int digit = DigitValueOf(codeDigits[i]);
//                int row   = digit / GridColumns;
//                int col   = digit % GridColumns;
//                latVal += row * latPlaceVal;
//                lngVal += col * lngPlaceVal;
//            }
//            return new CodeArea(
//                                (double)latVal                 / LatIntegerMultiplier,
//                                (double)lngVal                 / LngIntegerMultiplier,
//                                (double)(latVal + latPlaceVal) / LatIntegerMultiplier,
//                                (double)(lngVal + lngPlaceVal) / LngIntegerMultiplier,
//                                codeLength
//                               );
//        }
//        
//        private int DigitValueOf(char digitChar) => IndexedDigitValues[digitChar - IndexedDigitValueOffset];

    }

}