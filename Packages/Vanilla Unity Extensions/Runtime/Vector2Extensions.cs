using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.DotNetExtensions;

namespace Vanilla.UnityExtensions
{

	public static class Vector2Extensions
	{

		// -------------------------------------------------------------------------------------------------------------------------------- Clamp //


		public static Vector2 Clamp(this Vector2 input,
		                            float max) => new Vector2(x: Mathf.Clamp(value: input.x,
		                                                                     min: 0.0f,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: 0.0f,
		                                                                     max: max));


		public static Vector2 Clamp(this Vector2 input,
		                            float min,
		                            float max) => new Vector2(x: Mathf.Clamp(value: input.x,
		                                                                     min: min,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: min,
		                                                                     max: max));


		public static Vector2 Clamp(this Vector2 input,
		                            Vector2 max) => new Vector2(x: Mathf.Clamp(value: input.x,
		                                                                       min: 0.0f,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: 0.0f,
		                                                                       max: max.y));


		public static Vector2 Clamp(this Vector2 input,
		                            Vector2 min,
		                            Vector2 max) => new Vector2(x: Mathf.Clamp(value: input.x,
		                                                                       min: min.x,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: min.y,
		                                                                       max: max.y));


		public static Vector2 Clamp(this Vector2 input,
		                            float xMin,
		                            float xMax,
		                            float yMin,
		                            float yMax) => new Vector2(x: Mathf.Clamp(value: input.x,
		                                                                      min: xMin,
		                                                                      max: xMax),
		                                                       y: Mathf.Clamp(value: input.y,
		                                                                      min: yMin,
		                                                                      max: yMax));


		public static Vector2 Clamp01(this Vector2 input) => new Vector2(x: Mathf.Clamp01(value: input.x),
		                                                                 y: Mathf.Clamp01(value: input.y));


		// --------------------------------------------------------------------------------------------------------------------------------- Wrap //


		public static Vector2 Wrap(this Vector2 input,
		                           float max) => new Vector2(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max));


		public static Vector2 Wrap(this Vector2 input,
		                           float min,
		                           float max) => new Vector2(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: min,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: min,
		                                                                    max: max));


		public static Vector2 Wrap(this Vector2 input,
		                           Vector2 max) => new Vector2(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: 0.0f,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: 0.0f,
		                                                                      max: max.y));


		public static Vector2 Wrap(this Vector2 input,
		                           Vector2 min,
		                           Vector2 max) => new Vector2(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: min.x,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: min.y,
		                                                                      max: max.y));


		public static Vector2 Wrap(this Vector2 input,
		                           float xMin,
		                           float xMax,
		                           float yMin,
		                           float yMax) => new Vector2(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / xMax) * xMax,
		                                                                     min: xMin,
		                                                                     max: xMax),
		                                                      y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / yMax) * yMax,
		                                                                     min: yMin,
		                                                                     max: yMax));


		// ------------------------------------------------------------------------------------------------------------------------------ Nearest //


		/// <summary>
		/// 	Returns a Vector2 with both components to the nearest whole number.
		/// </summary>
		public static Vector2 Round(this Vector2 input) => new Vector2(x: Mathf.Round(f: input.x),
		                                                               y: Mathf.Round(f: input.y));


		/// <summary>
		/// 	Returns a Vector2 with both components to the next highest whole number.
		/// </summary>
		public static Vector2 Ceiling(this Vector2 input) => new Vector2(x: Mathf.Ceil(f: input.x),
		                                                                 y: Mathf.Ceil(f: input.y));


		/// <summary>
		/// 	Returns a Vector2 with both components to the next highest whole number.
		/// </summary>
		public static Vector2 Floor(this Vector2 input) => new Vector2(x: Mathf.Floor(f: input.x),
		                                                               y: Mathf.Floor(f: input.y));


		/// <summary>
		/// 	Returns a Vector2 with both components to the nearest multiple of factor.
		///
		/// 	This function can handle positive or negative values.
		/// </summary>
		public static Vector2 Nearest(this Vector2 input,
		                              float factor) => new Vector2(x: input.x.Nearest(factor: factor),
		                                                           y: input.y.Nearest(factor: factor));


		/// <summary>
		/// 	Returns a Vector2 with both components to the nearest value.
		///
		/// 	This function is only able to handle positive values.
		/// </summary>
		public static Vector2 NearestPositive(this Vector2 input,
		                                      float factor) => new Vector2(x: input.x.NearestPositive(factor: factor),
		                                                                   y: input.y.NearestPositive(factor: factor));


		// ---------------------------------------------------------------------------------------------------------------------------- Magnitude //


		/// <summary>
		///		Returns a new Vector2 with it's magnitude set to factor.
		/// </summary>
		public static Vector2 SetMagnitude(this Vector2 input,
		                                   float factor) => input.normalized * factor;


		/// <summary>
		///		Returns a new Vector2 with it's magnitude scaled by factor.
		/// </summary>
		public static Vector2 ScaleMagnitude(this Vector2 input,
		                                     float factor) => input.normalized * (input.magnitude * factor);


		/// <summary>
		///		Returns a new Vector2 with the new magnitude amount added. 
		/// </summary>
		public static Vector2 AddMagnitude(this Vector2 input,
		                                   float length)
		{
			var magnitude = input.magnitude;

			return input * (magnitude + length / magnitude);
		}


		public static Vector2 ClampMagnitude(this Vector2 input,
		                                     float maxLength) => Vector2.ClampMagnitude(vector: input,
		                                                                                maxLength: maxLength);


		// --------------------------------------------------------------------------------------------------------------------------------- Linq //


		/// <summary>
		///     Returns a Vector2 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector2 Sum(this List<Vector2> input) => new Vector2(x: input.Sum(i => i.x),
		                                                                   y: input.Sum(i => i.y));


		/// <summary>
		///     Returns a Vector2 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector2 Sum(this Vector2[] input) => new Vector2(x: input.Sum(i => i.x),
		                                                               y: input.Sum(i => i.y));


		/// <summary>
		///     Returns a Vector2 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector2 Average(this List<Vector2> input) => new Vector2(x: input.Average(i => i.x),
		                                                                       y: input.Average(i => i.y));


		/// <summary>
		///     Returns a Vector2 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector2 Average(this Vector2[] input) => new Vector2(x: input.Average(i => i.x),
		                                                                   y: input.Average(i => i.y));


		// ---------------------------------------------------------------------------------------------------------------------------- Rotations //


		/// <summary>
		///		Returns a Vector2 representing the direction from input to target.
		/// </summary>
		public static Vector2 DirectionTo(this Vector2 input,
		                                  Vector2 target) => target - input;


		/// <summary>
		///     This will convert a direction vector2 into radians. Make sure the input direction is normalized!
		///
		///		Y up = 0 radians
		/// </summary>
		public static float DirectionToRadians(this Vector2 input)
		{
			var o = Mathf.Atan2(y: input.x,
			                    x: input.y);

			if (o < 0.0f) o = Mathf.PI + (Mathf.PI + o);

			return o;
		}


		/// <summary>
		///     This will convert a direction Vector2 into an angle in degrees (0 - 360). Make sure the input direction is normalized!
		///
		///		Y up = 0 degrees
		/// </summary>
		public static float DirectionToDegrees(this Vector2 input) => -Mathf.Atan2(y: input.x,
		                                                                           x: input.y) * Mathf.Rad2Deg;


		// ------------------------------------------------------------------------------------------------------------------------------- Rotate //


		/// <summary>
		/// 	Rotate a Vector2
		/// </summary>
		///
		/// <param name="degrees">
		/// 	The amount of degrees to rotate the vector by
		/// </param>
		/// 
		/// <returns>
		/// 	A Vector2 representing the input vector rotated by degrees
		/// </returns>
		public static Vector2 Rotate(this Vector2 input,
		                             float degrees)
		{
			var sin = Mathf.Sin(f: degrees * Mathf.Deg2Rad);
			var cos = Mathf.Cos(f: degrees * Mathf.Deg2Rad);

			var x = input.x;
			var y = input.y;

			return new Vector2(x: cos * x - sin * y,
			                   y: sin * x + cos * y);
		}


		// ------------------------------------------------------------------------------------------------------------------------------ Swizzle //


		/// <summary>
		///     Returns a new Vector3 with the following format - [ x, input.x, input.y ]
		/// </summary>
		public static Vector2 YX(this Vector2 input,
		                         float x) => new Vector2(x: input.y,
		                                                 y: input.x);

		/// <summary>
		///     Returns a new Vector3 with the following format - [ x, input.x, input.y ]
		/// </summary>
		public static Vector3 _XY(this Vector2 input,
		                          float x) => new Vector3(x: x,
		                                                  y: input.x,
		                                                  z: input.y);


		/// <summary>
		///     Returns a new Vector3 with the following format - [ x, input.y, input.x ]
		/// </summary>
		public static Vector3 _YX(this Vector2 input,
		                          float x) => new Vector3(x: x,
		                                                  y: input.y,
		                                                  z: input.x);


		/// <summary>
		///     Returns a new Vector3 with the following format - [ input.x, y, input.z ]
		/// </summary>
		public static Vector3 X_Y(this Vector2 input,
		                          float y) => new Vector3(x: input.x,
		                                                  y: y,
		                                                  z: input.y);


		/// <summary>
		///     Returns a new Vector3 with the following format - [ input.y, y, input.x ]
		/// </summary>
		public static Vector3 Y_X(this Vector2 input,
		                          float y) => new Vector3(x: input.y,
		                                                  y: y,
		                                                  z: input.x);


		/// <summary>
		///     Returns a new Vector3 with the following format - [ input.x, input.y, z ]
		/// </summary>
		public static Vector3 XY_(this Vector2 input,
		                          float z) => new Vector3(x: input.x,
		                                                  y: input.y,
		                                                  z: z);


		/// <summary>
		///     Returns a new Vector3 with the following format - [ input.x, input.y, z ]
		/// </summary>
		public static Vector3 YX_(this Vector2 input,
		                          float z) => new Vector3(x: input.y,
		                                                  y: input.x,
		                                                  z: z);

	}

}