using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.DotNetExtensions;

namespace Vanilla.UnityExtensions
{

	public static class Vector3Extensions
	{

		// -------------------------------------------------------------------------------------------------------------------------------- Clamp //


		public static Vector3 Clamp(this Vector3 input,
		                            float max) => new Vector3(x: Mathf.Clamp(value: input.x,
		                                                                     min: 0.0f,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: 0.0f,
		                                                                     max: max),
		                                                      z: Mathf.Clamp(value: input.z,
		                                                                     min: 0.0f,
		                                                                     max: max));


		public static Vector3 Clamp(this Vector3 input,
		                            float min,
		                            float max) => new Vector3(x: Mathf.Clamp(value: input.x,
		                                                                     min: min,
		                                                                     max: max),
		                                                      y: Mathf.Clamp(value: input.y,
		                                                                     min: min,
		                                                                     max: max),
		                                                      z: Mathf.Clamp(value: input.z,
		                                                                     min: min,
		                                                                     max: max));


		public static Vector3 Clamp(this Vector3 input,
		                            Vector3 max) => new Vector3(x: Mathf.Clamp(value: input.x,
		                                                                       min: 0.0f,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: 0.0f,
		                                                                       max: max.y),
		                                                        z: Mathf.Clamp(value: input.z,
		                                                                       min: 0.0f,
		                                                                       max: max.z));


		public static Vector3 Clamp(this Vector3 input,
		                            Vector3 min,
		                            Vector3 max) => new Vector3(x: Mathf.Clamp(value: input.x,
		                                                                       min: min.x,
		                                                                       max: max.x),
		                                                        y: Mathf.Clamp(value: input.y,
		                                                                       min: min.y,
		                                                                       max: max.y),
		                                                        z: Mathf.Clamp(value: input.z,
		                                                                       min: min.z,
		                                                                       max: max.z));


		public static Vector3 Clamp(this Vector3 input,
		                            float xMin,
		                            float xMax,
		                            float yMin,
		                            float yMax,
		                            float zMin,
		                            float zMax) => new Vector3(x: Mathf.Clamp(value: input.x,
		                                                                      min: xMin,
		                                                                      max: xMax),
		                                                       y: Mathf.Clamp(value: input.y,
		                                                                      min: yMin,
		                                                                      max: yMax),
		                                                       z: Mathf.Clamp(value: input.z,
		                                                                      min: zMin,
		                                                                      max: zMax));


		public static Vector3 Clamp01(this Vector3 input) => new Vector3(x: Mathf.Clamp01(value: input.x),
		                                                                 y: Mathf.Clamp01(value: input.y),
		                                                                 z: Mathf.Clamp01(value: input.z));


		// --------------------------------------------------------------------------------------------------------------------------------- Wrap //


		public static Vector3 Wrap(this Vector3 input,
		                           float max) => new Vector3(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max),
		                                                     z: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.z / max) * max,
		                                                                    min: 0.0f,
		                                                                    max: max));


		public static Vector3 Wrap(this Vector3 input,
		                           float min,
		                           float max) => new Vector3(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max) * max,
		                                                                    min: min,
		                                                                    max: max),
		                                                     y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max) * max,
		                                                                    min: min,
		                                                                    max: max),
		                                                     z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max) * max,
		                                                                    min: min,
		                                                                    max: max));


		public static Vector3 Wrap(this Vector3 input,
		                           Vector3 max) => new Vector3(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: 0.0f,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: 0.0f,
		                                                                      max: max.y),
		                                                       z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max.z) * max.z,
		                                                                      min: 0.0f,
		                                                                      max: max.z));


		public static Vector3 Wrap(this Vector3 input,
		                           Vector3 min,
		                           Vector3 max) => new Vector3(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / max.x) * max.x,
		                                                                      min: min.x,
		                                                                      max: max.x),
		                                                       y: Mathf.Clamp(value: input.y - Mathf.Floor(f: input.y / max.y) * max.y,
		                                                                      min: min.y,
		                                                                      max: max.y),
		                                                       z: Mathf.Clamp(value: input.z - Mathf.Floor(f: input.z / max.z) * max.z,
		                                                                      min: min.z,
		                                                                      max: max.z));


		public static Vector3 Wrap(this Vector3 input,
		                           float xMin,
		                           float xMax,
		                           float yMin,
		                           float yMax,
		                           float zMin,
		                           float zMax) => new Vector3(x: Mathf.Clamp(value: input.x - Mathf.Floor(f: input.x / xMax) * xMax,
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
		/// 	Returns a Vector3 with both components to the nearest whole number.
		/// </summary>
		public static Vector3 Round(this Vector3 input) => new Vector3(x: Mathf.Round(f: input.x),
		                                                               y: Mathf.Round(f: input.y),
		                                                               z: Mathf.Round(f: input.z));


		/// <summary>
		/// 	Returns a Vector3 with both components to the next highest whole number.
		/// </summary>
		public static Vector3 Ceiling(this Vector3 input) => new Vector3(x: Mathf.Ceil(f: input.x),
		                                                                 y: Mathf.Ceil(f: input.y),
		                                                                 z: Mathf.Ceil(f: input.z));


		/// <summary>
		/// 	Returns a Vector3 with both components to the next highest whole number.
		/// </summary>
		public static Vector3 Floor(this Vector3 input) => new Vector3(x: Mathf.Floor(f: input.x),
		                                                               y: Mathf.Floor(f: input.y),
		                                                               z: Mathf.Floor(f: input.z));


		/// <summary>
		/// 	Returns a Vector3 with both components to the nearest multiple of factor.
		///
		/// 	This function can handle positive or negative values.
		/// </summary>
		public static Vector3 Nearest(this Vector3 input,
		                              float factor) => new Vector3(x: input.x.Nearest(factor: factor),
		                                                           y: input.y.Nearest(factor: factor),
		                                                           z: input.z.Nearest(factor: factor));


		/// <summary>
		/// 	Returns a Vector3 with both components to the nearest value.
		///
		/// 	This function is only able to handle positive values.
		/// </summary>
		public static Vector3 NearestPositive(this Vector3 input,
		                                      float factor) => new Vector3(x: input.x.NearestPositive(factor: factor),
		                                                                   y: input.y.NearestPositive(factor: factor),
		                                                                   z: input.z.NearestPositive(factor: factor));


		// ---------------------------------------------------------------------------------------------------------------------------- Magnitude //


		/// <summary>
		///		Returns a new Vector3 with it's magnitude set to factor.
		/// </summary>
		public static Vector3 SetMagnitude(this Vector3 input,
		                                   float factor) => input.normalized * factor;


		/// <summary>
		///		Returns a new Vector3 with it's magnitude scaled by factor.
		/// </summary>
		public static Vector3 ScaleMagnitude(this Vector3 input,
		                                     float factor) => input.normalized * (input.magnitude * factor);


		/// <summary>
		///		Returns a new Vector3 with the new magnitude amount added. 
		/// </summary>
		public static Vector3 AddMagnitude(this Vector3 input,
		                                   float length)
		{
			var magnitude = input.magnitude;

			return input * (magnitude + length / magnitude);
		}


		public static Vector3 ClampMagnitude(this Vector3 input,
		                                     float maxLength) => Vector3.ClampMagnitude(vector: input,
		                                                                                maxLength: maxLength);


		// --------------------------------------------------------------------------------------------------------------------------------- Linq //


		/// <summary>
		///     Returns a Vector3 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector3 Sum(this List<Vector3> input) => new Vector3(x: input.Sum(i => i.x),
		                                                                   y: input.Sum(i => i.y),
		                                                                   z: input.Sum(i => i.z));


		/// <summary>
		///     Returns a Vector3 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector3 Sum(this Vector3[] input) => new Vector3(x: input.Sum(i => i.x),
		                                                               y: input.Sum(i => i.y),
		                                                               z: input.Sum(i => i.z));


		/// <summary>
		///     Returns a Vector3 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector3 Average(this List<Vector3> input) => new Vector3(x: input.Average(i => i.x),
		                                                                       y: input.Average(i => i.y),
		                                                                       z: input.Average(i => i.z));


		/// <summary>
		///     Returns a Vector3 representing the sum of all the vectors in the collection.
		/// </summary>
		public static Vector3 Average(this Vector3[] input) => new Vector3(x: input.Average(i => i.x),
		                                                                   y: input.Average(i => i.y),
		                                                                   z: input.Average(i => i.z));


		// --------------------------------------------------------------------------------------------------------------------------- Directions //


		/// <summary>
		///		Returns an un-normalized direction vector from input to target.
		/// </summary>
		public static Vector3 DirectionTo(this Vector3 input,
		                                  Vector3 target) => target - input;


		/// <summary>
		///		Returns an un-normalized direction vector from input to target.position.
		/// </summary>
		public static Vector3 DirectionTo(this Vector3 input,
		                                  Transform target) => target.position - input;


		/// <summary>
		///		Returns a normalized direction vector from input to target.
		/// </summary>
		public static Vector3 NormalDirectionTo(this Vector3 input,
		                                        Vector3 target) => (target - input).normalized;


		/// <summary>
		///		Returns a normalized direction vector from input to target.position.
		/// </summary>
		public static Vector3 NormalDirectionTo(this Vector3 input,
		                                        Transform target) => (target.position - input).normalized;

	}

}