using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.DotNetExtensions;

namespace Vanilla.UnityExtensions
{

	public static class Vector4Extensions
	{

		// ----------------------------------------------------------------------------------------------------------------------------- Addition //
		
		public static void Add(ref this Vector4 input,
		                       Vector4 amount) => input += amount;
		
		public static Vector4 Add(Vector4 input,
		                          Vector4 amount) => input + amount;
		
		// -------------------------------------------------------------------------------------------------------------------------- Subtraction //
		
		public static void Sub(ref this Vector4 input,
		                       Vector4 amount) => input -= amount;
		
		public static Vector4 Sub(this Vector4 input,
		                          Vector4 amount) => input - amount;
		
		// ----------------------------------------------------------------------------------------------------------------------- Multiplication //

		public static void Mul(ref this Vector4 input,
		                       float amount) => input *= amount;
		
		public static void Mul(ref this Vector4 input,
		                       Vector4 amount) => input = new Vector4(x: input.x * amount.x,
		                                                              y: input.y * amount.y,
		                                                              z: input.z * amount.z);


		public static Vector4 GetMul(this Vector4 input,
		                             float amount) => input * amount;


		public static Vector4 GetMul(this Vector4 input,
		                             Vector4 amount) => new(x: input.x * amount.x,
		                                                    y: input.y * amount.y,
		                                                    z: input.z * amount.z);
		
		// ----------------------------------------------------------------------------------------------------------------------------- Division //
		
		public static void Div(ref this Vector4 input,
		                       float amount) => input /= amount;
		
		public static void Div(ref this Vector4 input,
		                       Vector4 amount) => input = new Vector4(x: input.x / amount.x,
		                                                              y: input.y / amount.y,
		                                                              z: input.z / amount.z);


		public static Vector4 GetDiv(this Vector4 input,
		                             float amount) => input / amount;


		public static Vector4 GetDiv(this Vector4 input,
		                             Vector4 amount) => new(x: input.x / amount.x,
		                                                    y: input.y / amount.y,
		                                                    z: input.z / amount.z);
		
		// -------------------------------------------------------------------------------------------------------------------------------- Clamp //


		public static Vector4 Clamp(this Vector4 input,
		                            float max) => new Vector4(x: Mathf.Clamp(value: input.x,
		                                                                     min: 0.0f,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: 0.0f,
		                                                                     max: max),
		                                                      z: Mathf.Clamp(value: input.z,
		                                                                     min: 0.0f,
		                                                                     max: max));


		public static Vector4 Clamp(this Vector4 input,
		                            float min,
		                            float max) => new Vector4(x: Mathf.Clamp(value: input.x,
		                                                                     min: min,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: min,
		                                                                     max: max),
		                                                      z: Mathf.Clamp(value: input.z,
		                                                                     min: min,
		                                                                     max: max));


		public static Vector4 Clamp(this Vector4 input,
		                            Vector4 max) => new Vector4(x: Mathf.Clamp(value: input.x,
		                                                                       min: 0.0f,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: 0.0f,
		                                                                       max: max.y),
		                                                        z: Mathf.Clamp(value: input.z,
		                                                                       min: 0.0f,
		                                                                       max: max.z));


		public static Vector4 Clamp(this Vector4 input,
		                            Vector4 min,
		                            Vector4 max) => new Vector4(x: Mathf.Clamp(value: input.x,
		                                                                       min: min.x,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: min.y,
		                                                                       max: max.y),
		                                                        z: Mathf.Clamp(value: input.z,
		                                                                       min: min.z,
		                                                                       max: max.z));


		public static Vector4 Clamp(this Vector4 input,
		                            float xMin,
		                            float xMax,
		                            float yMin,
		                            float yMax,
		                            float zMin,
		                            float zMax) => new Vector4(x: Mathf.Clamp(value: input.x,
		                                                                      min: xMin,
		                                                                      max: xMax),
		                                                       y: Mathf.Clamp(value: input.y,
		                                                                      min: yMin,
		                                                                      max: yMax),
		                                                       z: Mathf.Clamp(value: input.z,
		                                                                      min: zMin,
		                                                                      max: zMax));


		public static Vector4 Clamp01(this Vector4 input) => new Vector4(x: Mathf.Clamp01(value: input.x),
		                                                                 y: Mathf.Clamp01(value: input.y),
		                                                                 z: Mathf.Clamp01(value: input.z));


		// --------------------------------------------------------------------------------------------------------------------------------- Wrap //


		public static Vector4 Wrap(this Vector4 input,
		                           float max) => new Vector4(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max),
		                                                     z: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.z / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max));


		public static Vector4 Wrap(this Vector4 input,
		                           float min,
		                           float max) => new Vector4(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: min,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: min,
		                                                                    max: max),
		                                                     z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max) * max,
		                                                                    min: min,
		                                                                    max: max));


		public static Vector4 Wrap(this Vector4 input,
		                           Vector4 max) => new Vector4(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: 0.0f,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: 0.0f,
		                                                                      max: max.y),
		                                                       z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max.z) * max.z,
		                                                                      min: 0.0f,
		                                                                      max: max.z));


		public static Vector4 Wrap(this Vector4 input,
		                           Vector4 min,
		                           Vector4 max) => new Vector4(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: min.x,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: min.y,
		                                                                      max: max.y),
		                                                       z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max.z) * max.z,
		                                                                      min: min.z,
		                                                                      max: max.z));


		public static Vector4 Wrap(this Vector4 input,
		                           float xMin,
		                           float xMax,
		                           float yMin,
		                           float yMax,
		                           float zMin,
		                           float zMax) => new Vector4(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / xMax) * xMax,
		                                                                     min: xMin,
		                                                                     max: xMax),
		                                                      y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / yMax) * yMax,
		                                                                     min: yMin,
		                                                                     max: yMax),
		                                                      z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / zMax) * zMax,
		                                                                     min: zMin,
		                                                                     max: zMax));


		// ------------------------------------------------------------------------------------------------------------------------------ Nearest //


		/// <summary>
		/// 	Returns a Vector4 with both components to the nearest whole number.
		/// </summary>
		public static Vector4 Round(this Vector4 input) => new Vector4(x: Mathf.Round(f: input.x),
		                                                               y: Mathf.Round(f: input.y),
		                                                               z: Mathf.Round(f: input.z));


		/// <summary>
		/// 	Returns a Vector4 with both components to the next highest whole number.
		/// </summary>
		public static Vector4 Ceiling(this Vector4 input) => new Vector4(x: Mathf.Ceil(f: input.x),
		                                                                 y: Mathf.Ceil(f: input.y),
		                                                                 z: Mathf.Ceil(f: input.z));


		/// <summary>
		/// 	Returns a Vector4 with both components to the next highest whole number.
		/// </summary>
		public static Vector4 Floor(this Vector4 input) => new Vector4(x: Mathf.Floor(f: input.x),
		                                                               y: Mathf.Floor(f: input.y),
		                                                               z: Mathf.Floor(f: input.z));


		/// <summary>
		/// 	Returns a Vector4 with both components to the nearest multiple of factor.
		///
		/// 	This function can handle positive or negative values.
		/// </summary>
		public static Vector4 Nearest(this Vector4 input,
		                              float factor) => new Vector4(x: input.x.Nearest(factor: factor),
		                                                           y: input.y.Nearest(factor: factor),
		                                                           z: input.z.Nearest(factor: factor));


		/// <summary>
		/// 	Returns a Vector4 with both components to the nearest value.
		///
		/// 	This function is only able to handle positive values.
		/// </summary>
		public static Vector4 NearestPositive(this Vector4 input,
		                                      float factor) => new Vector4(x: input.x.NearestPositive(factor: factor),
		                                                                   y: input.y.NearestPositive(factor: factor),
		                                                                   z: input.z.NearestPositive(factor: factor));


		// ---------------------------------------------------------------------------------------------------------------------------- Magnitude //


		/// <summary>
		///		Returns a new Vector4 with it's magnitude set to factor.
		/// </summary>
		public static Vector4 SetMagnitude(this Vector4 input,
		                                   float factor) => input.normalized * factor;


		/// <summary>
		///		Returns a new Vector4 with it's magnitude scaled by factor.
		/// </summary>
		public static Vector4 ScaleMagnitude(this Vector4 input,
		                                     float factor) => input.normalized * (input.magnitude * factor);


		/// <summary>
		///		Returns a new Vector4 with the new magnitude amount added. 
		/// </summary>
		public static Vector4 AddMagnitude(this Vector4 input,
		                                   float length)
		{
			var magnitude = input.magnitude;

			return input * (magnitude + length / magnitude);
		}


//		public static Vector4 ClampMagnitude(this Vector4 input,
//		                                     float maxLength) => Vector4.ClampMagnitude(vector: input,
//		                                                                                maxLength: maxLength);


		// --------------------------------------------------------------------------------------------------------------------------------- Linq //


		/// <summary>
		///     Returns a Vector4 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector4 Sum(this List<Vector4> input) => new Vector4(x: input.Sum(i => i.x),
		                                                                   y: input.Sum(i => i.y),
		                                                                   z: input.Sum(i => i.z),
		                                                                   w: input.Sum(i => i.w));


		/// <summary>
		///     Returns a Vector4 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector4 Sum(this Vector4[] input) => new Vector4(x: input.Sum(i => i.x),
		                                                               y: input.Sum(i => i.y),
		                                                               z: input.Sum(i => i.z),
		                                                               w: input.Sum(i => i.w));


		/// <summary>
		///     Returns a Vector4 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector4 Average(this List<Vector4> input) => new Vector4(x: input.Average(i => i.x),
		                                                                       y: input.Average(i => i.y),
		                                                                       z: input.Average(i => i.z),
		                                                                       w: input.Average(i => i.w));


		/// <summary>
		///     Returns a Vector4 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector4 Average(this Vector4[] input) => new Vector4(x: input.Average(i => i.x),
		                                                                   y: input.Average(i => i.y),
		                                                                   z: input.Average(i => i.z),
		                                                                   w: input.Average(i => i.w));

	}

}