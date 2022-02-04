using UnityEngine;

namespace Vanilla.DotNetExtensions
{

	public static class FloatExtensions
	{

		// -------------------------------------------------------------------------------------------------------------------------------- Clamp //


		public static float Clamp(this float input,
		                          float      max) => Mathf.Clamp(value: input,
		                                                         min: 0.0f,
		                                                         max: max);

		public static float Clamp(this float input,
		                          float      min,
		                          float      max) => Mathf.Clamp(value: input,
		                                                         min: min,
		                                                         max: max);

		public static float Clamp01(this float input) => Mathf.Clamp01(value: input);


//		public static float Clamp(this float input,
//		                          FloatRange range) => Mathf.Clamp(value: input,
//		                                                           min: range.min,
//		                                                           max: range.max);

		// --------------------------------------------------------------------------------------------------------------------------------- Wrap //


		public static float Wrap(this float input,
		                         float      max) => Mathf.Clamp(value: input - Mathf.Floor(f: input / max) * max,
		                                                        min: 0.0f,
		                                                        max: max);


		public static float Wrap(this float input,
		                         float      min,
		                         float      max) => Mathf.Clamp(value: input - Mathf.Floor(f: input / max) * max,
		                                                        min: min,
		                                                        max: max);


//		public static float Wrap(this float input,
//		                         FloatRange range) => Mathf.Clamp(value: input - Mathf.Floor(f: input / range.max) * range.max,
//		                                                          min: range.min,
//		                                                          max: range.max);
		
		// ---------------------------------------------------------------------------------------------------------------------------- Normalize //

		/// <summary>
		/// 	Returns a float representing a normalized value (0-1) between the input and the given maximum.
		/// </summary>
		public static float Normalize(this float input,
		                              float      max) => Mathf.Clamp01(value: input / max);

		// --------------------------------------------------------------------------------------------------------------------------- Validation //
		
		// Sign

		public static bool IsLessThanEpsilon(this float input) => input < Mathf.Epsilon;
		
		public static bool IsPositive(this float input) => input > 0.0f;

		public static bool IsPositiveOrZero(this float input) => input >= 0.0f;
		
		public static bool IsNegative(this float input) => input < 0.0f;

		public static bool IsNegativeOrZero(this float input) => input <= 0.0f;

		public static bool IsOdd(this float input) => (Mathf.RoundToInt(f: input) & 1) == 1;

		public static bool IsEven(this float input) => (Mathf.RoundToInt(f: input) & 1) == 0;

		public static bool IsDivisibleBy(this float input,
		                                 float factor) => input % factor == 0;

		// Normalization

		public static bool IsNormalized(this float input) => input > 0.0f && input < 1.0f;

		// Inclusive

		public static bool IsWithinInclusiveRange(this float input,
		                                          float      max) => input >= 0.0f && input <= max;

		public static bool IsWithinInclusiveRange(this float input,
		                                          float      min,
		                                          float      max) => input >= min && input <= max;

//		public static bool IsWithinInclusiveRange(this float input,
//		                                          FloatRange range) => input >= range.min && input <= range.max;

		// Exclusive
		
		public static bool IsWithinExclusiveRange(this float input,
		                                          float      max) => input > 0.0f && input < max;


		public static bool IsWithinExclusiveRange(this float input,
		                                          float      min,
		                                          float      max) => input > min && input < max;

//		public static bool IsWithinExclusiveRange(this float input,
//		                                          FloatRange range) => input > range.min && input < range.max;
		
		// ----------------------------------------------------------------------------------------------------------------------------- Rounding //

		
		/// <summary>
		/// 	Returns a float of the nearest whole number.
		/// </summary>
		public static float Round(this float input) => Mathf.Round(f: input);


		/// <summary>
		/// 	Returns a float to the next highest whole number.
		/// </summary>
		public static float Ceiling(this float input) => Mathf.Ceil(f: input);


		/// <summary>
		/// 	Returns a float to the next lowest whole number.
		/// </summary>
		public static float Floor(this float input) => Mathf.Floor(f: input);

		/// <summary>
		/// 	Returns the nearest multiple of factor.
		///
		/// 	This function will be able to handle positive or negative values.
		/// </summary>
		public static float Nearest(this float input,
		                            float      factor) => Mathf.Round(f: Mathf.Abs(f: input) / factor) * (input.IsPositive() ? factor : -factor);

		/// <summary>
		/// 	Returns a float to the nearest value.
		///
		/// 	This function is only able to handle positive values.
		/// </summary>
		public static float NearestPositive(this float input,
		                                    float factor) => Mathf.Round(f: input / factor) * factor;
		

		/// <summary>
		/// 	Returns an integer of the nearest whole number.
		/// </summary>
		public static int RoundInt(this float input) => Mathf.RoundToInt(f: input);


		/// <summary>
		/// 	Returns an integer of the next highest whole number.
		/// </summary>
		public static int CeilingInt(ref this float input) => Mathf.CeilToInt(f: input);


		/// <summary>
		/// 	Returns an integer of the next lowest whole number.
		/// </summary>
		public static int FloorInt(ref this float input) => Mathf.FloorToInt(f: input);


		/// <summary>
		/// 	Returns a float to the nearest value.
		///
		/// 	This function will be able to handle positive or negative values.
		/// </summary>
		public static int NearestInt(this float input,
		                             float factor) => Mathf.RoundToInt(f: Mathf.Round(f: Mathf.Abs(f: input) / factor) * ( input.IsPositive() ? factor : -factor ));

		/// <summary>
		/// 	Returns an integer the nearest multiple of factor.
		///
		/// 	This function is only be able to handle positive values.
		/// </summary>
		public static int NearestPositiveInt(ref this float input,
		                                     float          factor) => Mathf.RoundToInt(f: Mathf.Round(f: input / factor) * factor);


		// --------------------------------------------------------------------------------------------------------------------------------- Sign //

		/// <summary>
		/// 	Returns a positive version of the input float.
		/// </summary>
		public static float Positive(this float input) => Mathf.Abs(f: input);


		/// <summary>
		/// 	Returns a negative version of the input float.
		/// </summary>
		public static float Negative(this float input) => -Mathf.Abs(f: input);

		// ----------------------------------------------------------------------------------------------------------------------------- Decimals //

		/// <summary>
		///     Returns the input with the given number of digits
		/// </summary>
		public static float DecimalPlaces(this float input,
		                                  int        digits) => (float) System.Math.Round(value: input, 
		                                                                                  digits: digits);

		// ----------------------------------------------------------------------------------------------------------------------------- Rotation //

		public static float ToDegrees(this float radians) => radians * Mathf.Rad2Deg;

		public static float ToRadians(this float degrees) => degrees * Mathf.Deg2Rad;

		/// <summary>
		///		Returns a Vector2 direction representing the input radians.
		///
		///		Please note that this function completely ignores the mathematics convention of 0 radians = 1,0 because welcome to Unity baby
		/// </summary>
		public static Vector2 RadiansToDirection(this float radians) => new Vector2(x: Mathf.Sin(f: radians),
		                                                                              y: Mathf.Cos(f: radians));

		/// <summary>
		///		Returns a Vector2 direction representing the input degrees.
		///
		///		Please note that this function completely ignores the mathematics convention of 0 degrees = 1,0 because welcome to Unity baby
		/// </summary>
		public static Vector2 DegreesToDirection(this float degrees) => RadiansToDirection(radians: degrees * Mathf.Deg2Rad);

		// --------------------------------------------------------------------------------------------------------------------------- Difference //
		
		public static float Difference(this float input,
		                               float      target) => input - target;
		
		public static float AbsDifference(this float input,
		                                  float      target) => Mathf.Abs(f: input - target);

		// -------------------------------------------------------------------------------------------------------------------------------- Noise //

		/// <summary>
		/// 	Returns a noise value given a 3D position
		/// </summary>
		public static float ToPerlin3D(float x,
		                               float y,
		                               float z) => (Mathf.PerlinNoise(x: x,
		                                                              y: y) +
		                                            Mathf.PerlinNoise(x: y,
		                                                              y: z) +
		                                            Mathf.PerlinNoise(x: x,
		                                                              y: z) +
		                                            Mathf.PerlinNoise(x: y,
		                                                              y: x) +
		                                            Mathf.PerlinNoise(x: z,
		                                                              y: y) +
		                                            Mathf.PerlinNoise(x: z,
		                                                              y: x)) /
		                                           6.0f;
		
		// -------------------------------------------------------------------------------------------------------------------------- Dot Product //


		/// <summary>
		///		Converts between degrees and dot-product result, i.e. between -1 and 1 in a non-linear fashion.
		///
		///		This method allows us to easily convert degrees with dot product without worrying about the non-linearity of their results.
		///
		///		For example, a perfect dot product result is 1, the opposite is -1 and halfway is 0.
		/// 
		///		However, 0.5 is not the same as 45˚. 45˚ is 0.7071068! At least without this method...
		/// </summary>
		public static float DegreesToDotProduct(this float degrees) => Mathf.Cos(f: degrees.Clamp(min: 0.0f,
		                                                                                          max: 180.0f) * Mathf.Deg2Rad);


		/// <summary>
		///		Converts between degrees and dot-product result, i.e. between -1 and 1 in a non-linear fashion.
		///
		///		This method allows us to easily convert degrees with dot product without worrying about the non-linearity of their results.
		///
		///		For example, a perfect dot product result is 1, the opposite is -1 and halfway is 0.
		/// 
		///		However, 0.5 is not the same as 45˚. 45˚ is 0.7071068! At least without this method...
		/// </summary>
		public static float DotProductToDegrees(this float product) => Mathf.Acos(f: product.Clamp(min: -1,
		                                                                                           max: 1)) * Mathf.Rad2Deg;

	}

}