//using System;
//using System.Collections.Generic;
//using System.Linq;
//
//using Unity.Collections;
//
//using UnityEngine;
//
//using Vanilla.UnityExtensions;
//
//using Random = UnityEngine.Random;
//
//namespace Vanilla.Core.Math
//{
//
//	/// ---------------------------------------------------------------------------------------------------- Structs //
//	
//	[Serializable]
//	public struct Line2
//	{
//
//		[SerializeField]
//		public Vector2 position;
//
//		[SerializeField]
//		public Vector2 direction;
//
//	}
//
//	[Serializable]
//	public struct Line3
//	{
//
//		[SerializeField]
//		public Vector3 position;
//
//		[SerializeField]
//		public Vector3 direction;
//
//
//		public bool ClosestPointsOnTwoLines
//		(
//			out Vector3 aClosest,
//			out Vector3 bClosest,
//			Line3       lineA,
//			Line3       lineB)
//		{
//			aClosest = Vector3.zero;
//			bClosest = Vector3.zero;
//
//			var a = Vector3.Dot(lhs: lineA.direction,
//			                    rhs: lineA.direction);
//
//			var b = Vector3.Dot(lhs: lineA.direction,
//			                    rhs: lineB.direction);
//
//			var e = Vector3.Dot(lhs: lineB.direction,
//			                    rhs: lineB.direction);
//
//			var d = a * e - b * b;
//
//			// Lines are not parallel
//			if (d.IsZero()) return false;
//
//			var r = lineA.position - lineB.position;
//
//			var c = Vector3.Dot(lhs: lineA.direction,
//			                    rhs: r);
//
//			var f = Vector3.Dot(lhs: lineB.direction,
//			                    rhs: r);
//
//			var s = ( b * f - c * e ) / d;
//			var t = ( a * f - c * b ) / d;
//
//			aClosest = lineA.position + lineA.direction * s;
//
//			bClosest = lineB.position + lineB.direction * t;
//
//			return true;
//		}
//
//	}
//
//	public enum InterpolationType
//	{
//
//		Linear,
//		EaseIn,
//		EaseOut,
//		EaseInAndOut,
//		Bounce
//
//	}
//
//	public static class VanillaMath
//	{
//
//		#region Float
//
//
//
//		/// -------------------------------------------------------------------------------------------------- Float //
//
//
//
////		public static int GetCeilingInt
////		(
////			this float input)
////		{
////			return Mathf.CeilToInt(input);
////		}
////
////
////		public static void ToFloor
////		(
////			ref this float input)
////		{
////			Mathf.Floor(input);
////		}
////
////
////		public static void ToFloorInt
////		(
////			ref this float input)
////		{
////			Mathf.FloorToInt(input);
////		}
////
////
////		public static float GetFloor
////		(
////			this float input)
////		{
////			return Mathf.Floor(input);
////		}
////
////
////		public static int GetFloorInt
////		(
////			this float input)
////		{
////			return Mathf.FloorToInt(input);
////		}
////
////
////		public static float GetNormal
////		(
////			this float input,
////			float      max)
////		{
////			return Mathf.Clamp01(input / max);
////		}
//
//
//		// --- Ranges, Wraps, Clamps --- //
//
////
////		public static void Clamp
////		(
////			ref this float input,
////			float          min,
////			float          max)
////		{
////			input = Mathf.Clamp(value: input,
////			                    min: min,
////			                    max: max);
////		}
////		
////		public static float GetClamp
////		(
////			this float input,
////			float      min,
////			float      max)
////		{
////			return Mathf.Clamp(value: input,
////			                   min: min,
////			                   max: max);
////		}
////		
////		public static void Clamp01
////		(
////			ref this float input)
////		{
////			
////			input = Mathf.Clamp01(input);
////		}
////		
////		public static float GetClamp01
////		(
////			this float input)
////		{
////			return Mathf.Clamp01(input);
////		}
////
////		public static void Clamp
////		(
////			ref this float input,
////			FloatRange      range)
////		{
////			input = Mathf.Clamp(value: input,
////			                    min: range.min,
////			                    max: range.max);
////		}
////
////		public static float GetClamp
////		(
////			this float input,
////			FloatRange      range)
////		{
////			return Mathf.Clamp(value: input,
////			                   min: range.min,
////			                   max: range.max);
////		}
////
////
////		public static void Wrap
////		(
////			ref this float input,
////			float          min,
////			float          max)
////		{
////			input = Mathf.Clamp(value: input - Mathf.Floor(f: input / max) * max,
////			                    min: min,
////			                    max: max);
////		}
////
////
////		public static float GetWrap
////		(
////			this float input,
////			float      max)
////		{
////			return Mathf.Clamp(value: input - Mathf.Floor(f: input / max) * max,
////			                   min: 0.0f,
////			                   max: max);
////		}
//
//
//		// --- Rotations --- //
//
//
////		public static void RadiansToDegrees
////		(
////			ref this float input)
////		{
////			input *= Mathf.Rad2Deg;
////		}
////
////
////		public static float GetRadiansToDegrees
////		(
////			this float input)
////		{
////			return input * Mathf.Rad2Deg;
////		}
////
////
////		public static void DegreesToRadians
////		(
////			ref this float input)
////		{
////			input *= Mathf.Deg2Rad;
////		}
////
////
////		public static float GetDegreesToRadians
////		(
////			this float input)
////		{
////			return input * Mathf.Deg2Rad;
////		}
////
////
////		public static float GetAbsoluteDifference
////		(
////			this float input,
////			float      target)
////		{
////			return Mathf.Abs(input - target);
////		}
//
//
//		// --- Interpolation --- //
//
//
//		/// <summary>
//		///     Pass in a normalized float and an interpolation type and you'll receive the corresponding
//		///     modification.
//		/// </summary>
//		/// 
//		/// <param name="input">
//		///     A normalized float (between 0-1). This would ideally be iterated over time in a coroutine.
//		/// </param>
//		/// 
//		/// <param name="interpolationType">
//		///     What kind of interpolation do we want to apply to i?
//		/// </param>
//		public static void Interpolate
//		(
//			ref this float    input,
//			InterpolationType interpolationType)
//		{
//			switch (interpolationType)
//			{
//				case InterpolationType.EaseIn:
//
//					input.ToCoserp();
//
//					break;
//
//				case InterpolationType.EaseOut:
//
//					input.ToSinerp();
//
//					break;
//
//				case InterpolationType.EaseInAndOut:
//
//					input.ToHermite();
//
//					break;
//
//				case InterpolationType.Bounce:
//
//					input.ToBounce();
//
//					break;
//			}
//		}
//
//
//		/// <summary>
//		///     An ease-in-and-out formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-in-and-out effect.
//		/// </summary>
//		public static void ToHermite
//		(
//			ref this float input)
//		{
//			input = input * input * input * ( input * ( 6f * input - 15f ) + 10f );
//		}
//
//
//		/// <summary>
//		///     An ease-out formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-out effect.
//		/// </summary>
//		public static void ToSinerp
//		(
//			ref this float input)
//		{
//			input = Mathf.Sin(input * Mathf.PI * 0.5f);
//		}
//
//
//		/// <summary>
//		///     An ease-in formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-in effect.
//		/// </summary>
//		public static void ToCoserp
//		(
//			ref this float input)
//		{
//			input = 1.0f - Mathf.Cos(input * Mathf.PI * 0.5f);
//		}
//
//
//		/// <summary>
//		///     An ease-in formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth bounce effect.
//		/// </summary>
//		public static void ToBounce
//		(
//			ref this float input)
//		{
//			input = Mathf.Abs(Mathf.Sin(6.28f * ( input + 1f ) * ( input + 1f )) * ( 1f - input ));
//		}
//
//
//		/* Checks
//		 
//		Only ever true or false.
//		*/
//
//
//		public static bool IsNormalized
//		(
//			this float input)
//		{
//			return !( input < 0 || input > 1.0f );
//		}
//
//
//		public static bool IsWithinInclusiveRange
//		(
//			this float input,
//			float      max)
//		{
//			return input >= 0.0f && input <= max;
//		}
//
//
//		public static bool IsWithinInclusiveRange
//		(
//			this float input,
//			float      min,
//			float      max)
//		{
//			return input >= min && input <= max;
//		}
//
//
//		public static bool IsWithinExclusiveRange(this float input,
//		                                          float      max) => input > 0.0f && input < max;
//
//
//		public static bool IsWithinExclusiveRange(this float input,
//		                                          float      min,
//		                                          float      max) => input > min && input < max;
//
//		/// <summary>
//		///     Returns true if the value is above 0.
//		///     Use IsZeroOrHigher if 0 is to be considered positive.
//		/// </summary>
//		public static bool IsPositive(this float input) => input > 0.0f;
//
//		public static bool IsZero(this float input) => Mathf.Abs(input) < Mathf.Epsilon;
//
//		/// <summary>
//		///     Returns true if the value is lower than 0.
//		/// </summary>
//		public static bool IsNegative(this float input) => input < 0.0f;
//
//		public static bool IsZeroOrLower(this float input) => input <= 0.0f;
//		
//		public static bool IsZeroOrHigher(this float input) => input >= 0.0f;
//
//
//		/// <summary>
//		///     Returns true if input, divided by division, leaves no remainder. For example, input of 10 and
//		///     divider	of 3 would have a remainder of 1 and return false. An input of 10 and divider of 5
//		///     would return true.
//		/// 
//		///     We check if the difference is lower than Mathf.Epsilon to catch floating point errors.
//		/// </summary>
//		public static bool IsDivisableBy(this float input,
//		                                 float      divider) => 
//			( input % divider ).GetAbsoluteDifference(target: 0).IsZero();
//
//
//		public static bool IsTheSameAs(this float input,
//		                               float      comparable) => 
//			Mathf.Approximately(a: input,
//			                    b: comparable);
//
//
//		public static bool IsRoughly
//		(
//			this float input,
//			float      target)
//		{
//			return Mathf.Abs(input - target) < Mathf.Epsilon;
//		}
//
//		public static bool HasElapsed
//		(
//			ref this float input,
//			float          timerLength)
//		{
//			if (( input -= Time.deltaTime ).IsPositive()) return false;
//
//			input.Wrap(min: 0.0f,
//			           max: timerLength);
//
//			return true;
//		}
//
//
//		public static bool HasElapsedRandomizedLength
//		(
//			ref this float input,
//			ref      float timerLength,
//			ref      FloatRange timerLengthRange)
//		{
//			if (( input -= Time.deltaTime ).IsPositive()) return false;
//
//			timerLength = timerLengthRange.GetRandomValue();
//
//			input.Wrap(min: 0.0f,
//			           max: timerLength);
//
//			return true;
//		}
//
//
//		public static void Between01
//		(
//			ref this float input)
//		{
//			input = Mathf.Clamp01(input);
//		}
//
//
//		public static void Normalize
//		(
//			ref this float input,
//			float          max)
//		{
//			input /= max;
//		}
//
//
//		public static void Normalize
//		(
//			ref this float input,
//			float          min,
//			float          max)
//		{
//			input = ( input - min ) / ( max - min );
//		}
//
//
//		// --- Gets
//
//
//		public static float SinWave()
//		{
//			return Mathf.Sin(Time.time);
//		}
//
//
//		public static float WrapInt
//		(
//			float input,
//			float max)
//		{
//			return Mathf.Clamp(value: input - Mathf.Floor(input / max) * max,
//			                   min: 0.0f,
//			                   max: max);
//		}
//
//
//		public static float WrapInt
//		(
//			float input,
//			float min,
//			float max)
//		{
//			return Mathf.Clamp(value: input - Mathf.Floor(input / max) * max,
//			                   min: min,
//			                   max: max);
//		}
//
//
//		/// <summary>
//		///     Pass in a normalized float and an interpolation type and you'll receive the corresponding
//		///     modification.
//		/// </summary>
//		/// 
//		/// <param name="input">
//		///     A normalized float (between 0-1). This would ideally be iterated over time in a coroutine.
//		/// </param>
//		/// 
//		/// <param name="interpolationType">
//		///     What kind of interpolation do we want to apply to i?
//		/// </param>
//		public static float GetInterpolate
//		(
//			this float        input,
//			InterpolationType interpolationType)
//		{
//			switch (interpolationType)
//			{
//				case InterpolationType.EaseIn:
//
//					return GetCoserp(input);
//
//				case InterpolationType.EaseOut:
//
//					return GetSinerp(input);
//
//				case InterpolationType.EaseInAndOut:
//
//					return GetHermite(input);
//
//				case InterpolationType.Bounce:
//
//					return GetBounce(input);
//
//				default:
//
//					return input;
//			}
//		}
//
//
//		/// <summary>
//		///     An ease-in-and-out formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-in-and-out effect.
//		/// </summary>
//		public static float GetHermite
//		(
//			this float input)
//		{
//			return input * input * input * ( input * ( 6f * input - 15f ) + 10f );
//		}
//
//
//		/// <summary>
//		///     An ease-out formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-out effect.
//		/// </summary>
//		public static float GetSinerp
//		(
//			this float input)
//		{
//			return Mathf.Sin(input * Mathf.PI * 0.5f);
//		}
//
//
//		/// <summary>
//		///     An ease-in formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth ease-in effect.
//		/// </summary>
//		public static float GetCoserp
//		(
//			this float input)
//		{
//			return 1.0f - Mathf.Cos(input * Mathf.PI * 0.5f);
//		}
//
//
//		/// <summary>
//		///     An ease-in formula for a normalized float.
//		///     Modulate your standard 'i' time normal with this to get a smooth bounce effect.
//		/// </summary>
//		public static float GetBounce
//		(
//			this float x)
//		{
//			return Mathf.Abs(Mathf.Sin(6.28f * ( x + 1f ) * ( x + 1f )) * ( 1f - x ));
//		}
//
//
//		public static float GetBetween01
//		(
//			this float input)
//		{
//			return Mathf.Clamp01(input);
//		}
//
//
//		public static float GetNormalized
//		(
//			this float input,
//			float      max)
//		{
//			return input / max;
//		}
//
//
//		public static float GetNormalized
//		(
//			this float input,
//			float      min,
//			float      max)
//		{
//			return ( input - min ) / ( max - min );
//		}
//
//
//		public static int GetFromIntRangeFloor
//		(
//			int   min,
//			int   max,
//			float normal)
//		{
//			return Mathf.FloorToInt(Mathf.Lerp(a: min,
//			                                   b: max,
//			                                   t: normal));
//		}
//
//
//		public static int GetFromIntRangeRound
//		(
//			int   min,
//			int   max,
//			float normal)
//		{
//			return Mathf.RoundToInt(Mathf.Lerp(a: min,
//			                                   b: max,
//			                                   t: normal));
//		}
//
//
//		public static int GetFromIntRangeCeil
//		(
//			int   min,
//			int   max,
//			float normal)
//		{
//			return Mathf.CeilToInt(Mathf.Lerp(a: min,
//			                                  b: max,
//			                                  t: normal));
//		}
//
//
//		/* Iterations
//		
//		Do something to or return a value based on a collection of a type.
//		
//		*/
//
//
//		public static void AddToAll
//		(
//			this float[] input,
//			float        value)
//		{
//			for (var i = 0; i < input.Length; i++) input[i] += value;
//		}
//
//
//		public static void SetAll
//		(
//			this float[] input,
//			float        value)
//		{
//			for (var i = 0; i < input.Length; i++) input[i] = value;
//		}
//
//
////		public static float Tally
////		(
////			this IEnumerable<float> input)
////		{
////			return input?.Sum() ?? 0.0f;
////		}
//
//
////		public static float Average(this IEnumerable<float> input)
////		{
////			
////			
////			if (input == null) return 0.0f;
////
////			float output = 0.0f;
////
////			foreach (float f in input)
////			{
////				output += f;
////			}
////
////			return output / input.Count;
////		}
//
////		public static float Average(this float[] input)
////		{
////			if (input.Length == 0) return 0;
////			
////			float output = 0;
////
////			foreach (var v in input)
////			{
////				output += v;
////			}
////
////			return output / input.Length;
////		}
////		
////		public static float Average(this List<float> input)
////		{
////			if (input.Count == 0) return 0;
////
////			float output = 0;
////
////			foreach (var v in input)
////			{
////				output += v;
////			}
////
////			return output / input.Count;
////		}
//
//		// --- Signing --- //
//
//
//		public static void Unsigned
//		(
//			ref this float input)
//		{
//			input = input < 0.0f ?
//				        -input :
//				        input;
//		}
//
//
//		public static float GetUnsigned
//		(
//			this float input)
//		{
//			return input < 0.0f ?
//				       -input :
//				       input;
//		}
//
//
//
//		#endregion
//
//
//		/// ------------------------------------------------------------------------------------------------ Vector2 //
//
//		#region Vector2
//
//
//
//
//		// --- Swizzles --- //
//
//

//
//
//		// --- Signed --- //
//
//
//		public static void Unsigned
//		(
//			ref this Vector2 input)
//		{
//			input.x.Unsigned();
//			input.y.Unsigned();
//		}
//
//
//		public static Vector2 GetUnsigned
//		(
//			this Vector2 input)
//		{
//			return new Vector2(x: input.x.GetUnsigned(),
//			                   y: input.y.GetUnsigned());
//		}
//
//		// Comparisons
//		
//		/// <summary>
//		/// 	Treating this vector as a direction, is the dot product of these two vectors
//		/// 	within degrees of each other?
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		///
//		/// <param name="direction">
//		///		Another vector treated as a direction. i.e. targetTransform.forward
//		/// </param>
//		/// 
//		/// <param name="degrees">
//		///		How many degrees should we use to compare these directions?
//		/// </param>
//		///
//		/// <returns>
//		///		True if the degrees between the two directional vectors are less than 'degrees'.
//		/// </returns>
//		public static bool IsAlignedWith
//		(
//			this Vector2 input,
//			Vector2      direction,
//			float        degrees)
//		{
//			input.Normalize();
//
//			direction.Normalize();
//
//			return Normalized2DDotProductCheck(a: input,
//			                                   b: direction,
//			                                   degrees: degrees);
//		}
//		
//		/// <summary>
//		///     This Vector3.Dot check assumes that the values passed into it are normalized, allowing other methods
//		///     to not double up on normalization or its checks for efficiency reasons. For this reason, it has
//		///     also been made private to prevent accidental usage outside this class.
//		/// </summary>
//		private static bool Normalized2DDotProductCheck
//		(
//			Vector2 a,
//			Vector2 b,
//			float   degrees)
//		{
//			return Vector2.Dot(lhs: a,
//			                   rhs: b) >= degrees.GetDegreesToDotProduct();
//		}
//		
//		// --- Operator Overloads --- //
//
//		// Can't do this from outside the struct :(
////		public static Vector3 operator +(Vector3 a, Vector2 b)
////			=> new Vector3(a.x + b.x, a.y + b.y, a.z);
//
//		
//
//		#endregion
//
//		/// ------------------------------------------------------------------------------------------------ Vector3 //
//
//		#region Vector3
//
//
//		/// --- Constructors --- //
//		public static Vector3 GetRandomBoxPosition(float range) => new Vector3(x: Random.Range(minInclusive: -range,
//		                                                                                       maxInclusive: range),
//		                                                                       y: Random.Range(minInclusive: -range,
//		                                                                                       maxInclusive: range),
//		                                                                       z: Random.Range(minInclusive: -range,
//		                                                                                       maxInclusive: range));
//
//		/// --- Extensions --- ///
//		public static bool IsWithinBounds(this Vector3 input,
//		                                  Vector3 bounds) => input.x > -bounds.x && input.x < bounds.x &&
//		                                                     input.y > -bounds.y && input.y < bounds.y &&
//		                                                     input.z > -bounds.z && input.z < bounds.z;
//
//		public static bool IsWithinBounds(this Vector3 input,
//		                                  float        x,
//		                                  float        y,
//		                                  float        z) => input.x > -x && input.x < x &&
//		                                                     input.y > -y && input.y < y &&
//		                                                     input.z > -z && input.z < z;
//
//
//		public static Vector3Int ToInt3(this Vector3 input) => new Vector3Int(x: input.x.GetRoundInt(),
//		                                                                      y: input.y.GetRoundInt(),
//		                                                                      z: input.z.GetRoundInt());
//
//
//		public static Vector3 GetRandomCube(float size) => new Vector3(x: Random.Range(minInclusive: -size,
//		                                                                               maxInclusive: size),
//		                                                               y: Random.Range(minInclusive: -size,
//		                                                                               maxInclusive: size),
//		                                                               z: Random.Range(minInclusive: -size,
//		                                                                               maxInclusive: size));
//
//
//		public static Vector3 GetRandomCube(float min,
//		                                    float max) => new Vector3(x: Random.Range(minInclusive: min,
//		                                                                              maxInclusive: max),
//		                                                              y: Random.Range(minInclusive: min,
//		                                                                              maxInclusive: max),
//		                                                              z: Random.Range(minInclusive: min,
//		                                                                              maxInclusive: max));
//
//
//		public static Vector3 GetRandomCube(float xMin,
//		                                    float xMax,
//		                                    float yMin,
//		                                    float yMax,
//		                                    float zMin,
//		                                    float zMax) => new Vector3(x: Random.Range(minInclusive: xMin,
//		                                                                               maxInclusive: xMax),
//		                                                               y: Random.Range(minInclusive: yMin,
//		                                                                               maxInclusive: yMax),
//		                                                               z: Random.Range(minInclusive: zMin,
//		                                                                               maxInclusive: zMax));
//
//		public static void RoundToFloor(ref this Vector3 input) => input = new Vector3(x: Mathf.Floor(f: input.x),
//		                                                                               y: Mathf.Floor(f: input.y),
//		                                                                               z: Mathf.Floor(f: input.z));
//
//
//		public static Vector3 GetRoundToFloor(this Vector3 input) => new Vector3(x: Mathf.Floor(f: input.x),
//		                                                                         y: Mathf.Floor(f: input.y),
//		                                                                         z: Mathf.Floor(f: input.z));
//
//
//		public static void Round(ref this Vector3 input) => input = new Vector3(x: Mathf.Round(f: input.x),
//		                                                                        y: Mathf.Round(f: input.y),
//		                                                                        z: Mathf.Round(f: input.z));
//
//
//		public static Vector3 GetRound(this Vector3 input) => new Vector3(x: Mathf.Round(f: input.x),
//		                                                                  y: Mathf.Round(f: input.y),
//		                                                                  z: Mathf.Round(f: input.z));
//
//
//		public static Vector3Int GetRoundToInt(this Vector3 input) => new Vector3Int(x: input.x.GetRoundInt(),
//		                                                                             y: input.y.GetRoundInt(),
//		                                                                             z: input.z.GetRoundInt());
//
//
//		public static void RoundToCeil(ref this Vector3 input) => input = new Vector3(x: Mathf.Ceil(f: input.x),
//		                                                                              y: Mathf.Ceil(f: input.y),
//		                                                                              z: Mathf.Ceil(f: input.z));
//
//
//		public static Vector3 GetRoundToCeil(this Vector3 input) => new Vector3(x: Mathf.Ceil(f: input.x),
//		                                                                        y: Mathf.Ceil(f: input.y),
//		                                                                        z: Mathf.Ceil(f: input.z));
//
//
//		public static void BoxClamp(ref this Vector3 input,
//		                            float            size)
//		{
//			input.x.Clamp(min: -size,
//			              max: size);
//
//			input.y.Clamp(min: -size,
//			              max: size);
//
//			input.z.Clamp(min: -size,
//			              max: size);
//		}
//
//
//		public static void BoxClamp
//		(
//			ref this Vector3 input,
//			float            min,
//			float            max)
//		{
//			input.x.Clamp(min: min,
//			              max: max);
//
//			input.y.Clamp(min: min,
//			              max: max);
//
//			input.z.Clamp(min: min,
//			              max: max);
//		}
//
//
//		public static void RectangleClamp
//		(
//			ref this Vector3 input,
//			Vector3          bounds)
//		{
//			input.x.Clamp(min: -bounds.x,
//			              max: bounds.x);
//
//			input.y.Clamp(min: -bounds.y,
//			              max: bounds.y);
//
//			input.z.Clamp(min: -bounds.z,
//			              max: bounds.z);
//		}
//
//
//		public static void RectangleClamp
//		(
//			ref this Vector3 input,
//			Vector3          minBounds,
//			Vector3          maxBounds)
//		{
//			input.x.Clamp(min: minBounds.x,
//			              max: maxBounds.x);
//
//			input.y.Clamp(min: minBounds.y,
//			              max: maxBounds.y);
//
//			input.z.Clamp(min: minBounds.z,
//			              max: maxBounds.z);
//		}
//
//
//		public static void RectangleClamp
//		(
//			ref this Vector3 input,
//			float            xMin,
//			float            xMax,
//			float            yMin,
//			float            yMax,
//			float            zMin,
//			float            zMax)
//		{
//			input.x.Clamp(min: xMin,
//			              max: xMax);
//
//			input.y.Clamp(min: yMin,
//			              max: yMax);
//
//			input.z.Clamp(min: zMin,
//			              max: zMax);
//		}
//
//
//		public static void RadialClamp
//		(
//			ref this Vector3 input,
//			float            radius)
//		{
//			input = Vector3.ClampMagnitude(vector: input,
//			                               maxLength: radius);
//		}
//
//
//		public static Vector3 GetBoxClamp
//		(
//			this Vector3 input,
//			float        size)
//		{
//			return new Vector3(x: input.x.GetClamp(min: -size,
//			                                       max: size),
//			                   y: input.y.GetClamp(min: -size,
//			                                       max: size),
//			                   z: input.z.GetClamp(min: -size,
//			                                       max: size));
//		}
//
//
//		public static Vector3 GetBoxClamp
//		(
//			this Vector3 input,
//			float        min,
//			float        max)
//		{
//			return new Vector3(x: input.x.GetClamp(min: min,
//			                                       max: max),
//			                   y: input.y.GetClamp(min: min,
//			                                       max: max),
//			                   z: input.z.GetClamp(min: min,
//			                                       max: max));
//		}
//
//
//		public static Vector3 GetBoxClamp
//		(
//			this Vector3 input,
//			FloatRange        range)
//		{
//			return new Vector3(x: input.x.GetClamp(range),
//			                   y: input.y.GetClamp(range),
//			                   z: input.z.GetClamp(range));
//		}
//
//
//		public static Vector3 GetRectangleClamp
//		(
//			this Vector3 input,
//			Vector3      bounds)
//		{
//			return new Vector3(x: input.x.GetClamp(min: -bounds.x,
//			                                       max: bounds.x),
//			                   y: input.y.GetClamp(min: -bounds.y,
//			                                       max: bounds.y),
//			                   z: input.z.GetClamp(min: -bounds.z,
//			                                       max: bounds.z));
//		}
//
//
//		public static Vector3 GetRectangleClamp
//		(
//			this Vector3 input,
//			Vector3      minBounds,
//			Vector3      maxBounds)
//		{
//			return new Vector3(x: input.x.GetClamp(min: minBounds.x,
//			                                       max: maxBounds.x),
//			                   y: input.y.GetClamp(min: minBounds.y,
//			                                       max: maxBounds.y),
//			                   z: input.z.GetClamp(min: minBounds.z,
//			                                       max: maxBounds.z));
//		}
//
//
//		public static Vector3 GetRectangleClamp
//		(
//			this Vector3 input,
//			float        xMin,
//			float        yMin,
//			float        zMin,
//			float        xMax,
//			float        yMax,
//			float        zMax)
//		{
//			return new Vector3(x: input.x.GetClamp(min: xMin,
//			                                       max: xMax),
//			                   y: input.y.GetClamp(min: yMin,
//			                                       max: yMax),
//			                   z: input.z.GetClamp(min: zMin,
//			                                       max: zMax));
//		}
//
//
//		public static Vector3 GetRadialClamp
//		(
//			this Vector3 input,
//			float        radius)
//		{
////			return Vector3.ClampMagnitude(input, radius); // faster?
//
//			return input.normalized * radius; // faster?
//		}
//
//
//		public static void ToNearestPositiveOnly
//		(
//			ref this Vector3 input,
//			float            factor)
//		{
//			input.x.ToNearestPositive(factor);
//			input.y.ToNearestPositive(factor);
//			input.z.ToNearestPositive(factor);
//		}
//
//
//		public static void ToNearest
//		(
//			ref this Vector3 input,
//			float            factor)
//		{
//			input.x.ToNearest(factor);
//			input.y.ToNearest(factor);
//			input.z.ToNearest(factor);
//		}
//
//
//		public static Vector3 GetToNearestPositiveOnly
//		(
//			this Vector3 input,
//			float        factor)
//		{
//			return new Vector3(x: input.x.GetNearestPositive(factor),
//			                   y: input.y.GetNearestPositive(factor),
//			                   z: input.z.GetNearestPositive(factor));
//		}
//
//
//		public static Vector3 GetToNearest
//		(
//			this Vector3 input,
//			float        factor)
//		{
//			return new Vector3(x: input.x.GetToNearest(factor),
//			                   y: input.y.GetToNearest(factor),
//			                   z: input.z.GetToNearest(factor));
//		}
//
//
//		//increase or decrease the length of vector by size
//		public static Vector3 GetModifiedMagnitude
//		(
//			this Vector3 input,
//			float        size)
//		{
//			//get the vector length
//			var magnitude = Vector3.Magnitude(input);
//
//			//calculate new vector length
//			var newMagnitude = magnitude + size;
//
//			//calculate the ratio of the new length to the old length
//			var scale = newMagnitude / magnitude;
//
//			//scale the vector
//			return input * scale;
//		}
//
//
//		public static Vector3 GetChangedMagnitude
//		(
//			this Vector3 input,
//			float        newMagnitude)
//		{
//			return input.normalized * newMagnitude;
//		}
//
//
//		public static Vector3 GetSum
//		(
//			this IEnumerable<Vector3> input)
//		{
//			return input.Aggregate(seed: Vector3.zero,
//			                       func:
//			                       (
//				                       current,
//				                       v) => current + v);
//		}
//
//
//		/// <summary>
//		///     Returns a Vector2 representing the sum of all the vectors in the collection.
//		/// </summary>
//		public static Vector3 Sum(this IEnumerable<Vector3> input) => input.Aggregate(seed: Vector3.zero,
//		                                                                              func: (total, next) => total + next);
//		
//		/// <summary>
//		///     Returns a Vector2 representing the average of all the vectors in the collection.
//		/// </summary>
//		public static Vector3 Average(this IEnumerable<Vector3> input) => input.Sum() / input.Count();
//
//
//		/// <summary>
//		///     This function rotates the given vector3 (treated as a position) around the given axis.
//		/// </summary>
//		public static void RotateAround(ref this Vector3 input,
//		                                Axis3D             axis,
//		                                float            degrees)
//		{
//			var a = Vector3.zero;
//			var d = Vector3.zero;
//
//			switch (axis)
//			{
//				case Axis3D.X:
//
//					a = Vector3.right;
//
//					d = new Vector3(x: degrees,
//					                y: 0,
//					                z: 0);
//
//					break;
//
//				case Axis3D.Y:
//
//					a = Vector3.up;
//
//					d = new Vector3(x: 0,
//					                y: degrees,
//					                z: 0);
//
//					break;
//
//				case Axis3D.Z:
//
//					a = Vector3.forward;
//
//					d = new Vector3(x: 0,
//					                y: 0,
//					                z: degrees);
//
//					break;
//			}
//
//			input = Quaternion.Euler(d) * ( input - a ) + a;
//		}
//
//
//		/// <summary>
//		///     This function rotates the given Vector3 (treated as a position) around the given axis
//		///     by degrees.
//		/// </summary>
//		public static void RotateAround
//		(
//			ref this Vector3 input,
//			Vector3          axis,
//			Vector3          degrees)
//		{
//			input = Quaternion.Euler(degrees) * ( input - axis ) + axis;
//		}
//
//
//		/// <summary>
//		///     This function rotates the given vector3 (treated as a position) around axis of the amount
//		///     found in angles (treated as degrees).
//		/// </summary>
//		public static Vector3 GetRotateAround
//		(
//			this Vector3 input,
//			Axis3D         axis,
//			float        degrees)
//		{
//			var a = Vector3.zero;
//			var d = Vector3.zero;
//
//			switch (axis)
//			{
//				case Axis3D.X:
//
//					a = Vector3.right;
//
//					d = new Vector3(x: degrees,
//					                y: 0,
//					                z: 0);
//
//					break;
//
//				case Axis3D.Y:
//
//					a = Vector3.up;
//
//					d = new Vector3(x: 0,
//					                y: degrees,
//					                z: 0);
//
//					break;
//
//				case Axis3D.Z:
//
//					a = Vector3.forward;
//
//					d = new Vector3(x: 0,
//					                y: 0,
//					                z: degrees);
//
//					break;
//			}
//
//			return Quaternion.Euler(d) * ( input - a ) + a;
//		}
//
//
//		/// <summary>
//		///     This function rotates the given Vector3 (treated as a position) around the given axis
//		///     by degrees.
//		/// </summary>
//		public static Vector3 GetRotateAround
//		(
//			this Vector3 input,
//			Vector3          axis,
//			Vector3          degrees)
//		{
//			return Quaternion.Euler(degrees) * ( input - axis ) + axis;
//		}
//
//
//		// --- Swizzles --- //
//
//
//		public static Vector2 XYToXY
//		(
//			this Vector3 input)
//		{
//			return new Vector2(x: input.x,
//			                   y: input.y);
//		}
//
//
//		/// <summary>
//		///     Returns a Vector2 that uses the input Vector3 X component for X and input Z component for Y.
//		/// </summary>
//		/// <param name="input"></param>
//		/// <returns></returns>
//		public static Vector2 XZToXY
//		(
//			this Vector3 input)
//		{
//			return new Vector2(x: input.x,
//			                   y: input.z);
//		}
//
//
//		public static Vector2 YZToXY
//		(
//			this Vector3 input)
//		{
//			return new Vector2(x: input.y,
//			                   y: input.z);
//		}
//
//
//		// --- Flattened --- //
//
//
//		/// <summary>
//		///     Returns the same Vector3 with x as 0.
//		/// </summary>
//		public static Vector3 SquashX
//		(
//			this Vector3 input)
//		{
//			return new Vector3(x: 0,
//			                   y: input.y,
//			                   z: input.z);
//		}
//
//
//		/// <summary>
//		///     Returns the same Vector3 with y as 0.
//		/// </summary>
//		public static Vector3 SquashY
//		(
//			this Vector3 input)
//		{
//			return new Vector3(x: input.x,
//			                   y: 0,
//			                   z: input.z);
//		}
//
//
//		/// <summary>
//		///     Returns the same Vector3 with z as 0.
//		/// </summary>
//		public static Vector3 SquashZ
//		(
//			this Vector3 input)
//		{
//			return new Vector3(x: input.x,
//			                   y: input.y,
//			                   z: 0);
//		}
//
//
//		// --- Signed --- //
//
//
//		public static void Unsigned
//		(
//			ref this Vector3 input)
//		{
//			input.x.Unsigned();
//			input.y.Unsigned();
//			input.z.Unsigned();
//		}
//
//
//		public static Vector3 GetUnsigned
//		(
//			this Vector3 input)
//		{
//			return new Vector3(x: input.x.GetUnsigned(),
//			                   y: input.y.GetUnsigned(),
//			                   z: input.z.GetUnsigned());
//		}
//
//		// --- Swizzles --- //
//
//
//		public static void Swizzle
//		(
//			ref this Vector3                   input,
//			VanillaMath3D.V3SwizzleFormat swizzleFormat)
//		{
//			var cache = input;
//			
//			switch (swizzleFormat)
//			{
//
//				case VanillaMath3D.V3SwizzleFormat.XZY:
//
////					input.x = cache.x;
//					input.y = cache.z;
//					input.z = cache.y;
//
//					break;
//				
//				case VanillaMath3D.V3SwizzleFormat.YXZ:
//				
//					input.x = cache.y;
//					input.y = cache.x;
////					input.z = cache.z;
//
//					break;
//				
//				case VanillaMath3D.V3SwizzleFormat.YZX:
//					
//					input.x = cache.y;
//					input.y = cache.z;
//					input.z = cache.x;
//					
//					break;
//				
//				case VanillaMath3D.V3SwizzleFormat.ZXY:
//					
//					input.x = cache.z;
//					input.y = cache.x;
//					input.z = cache.y;
//					
//					break;
//				
//				case VanillaMath3D.V3SwizzleFormat.ZYX:
//					
//					input.x = cache.z;
////					input.y = cache.y;
//					input.z = cache.x;
//					
//					break;
//				
//			}
//		}
//
//		public static Vector3 GetSwizzle
//		(
//			this Vector3                   input,
//			VanillaMath3D.V3SwizzleFormat swizzleFormat)
//		{
//			switch (swizzleFormat)
//			{
//
//				case VanillaMath3D.V3SwizzleFormat.XZY:
//
//					return new Vector3(x: input.x,
//					                   y: input.z,
//					                   z: input.y);
//				
//				case VanillaMath3D.V3SwizzleFormat.YXZ:
//				
//					return new Vector3(x: input.y,
//					                   y: input.x,
//					                   z: input.z);
//					
//				case VanillaMath3D.V3SwizzleFormat.YZX:
//					
//					return new Vector3(x: input.y,
//					                   y: input.z,
//					                   z: input.x);
//				
//				case VanillaMath3D.V3SwizzleFormat.ZXY:
//					
//					return new Vector3(x: input.z,
//					                   y: input.x,
//					                   z: input.y);
//				
//				case VanillaMath3D.V3SwizzleFormat.ZYX:
//					
//					return new Vector3(x: input.z,
//					                   y: input.y,
//					                   z: input.x);
//				
//			}
//
//			return input;
//		}
//
//		#endregion
//
//		#region Quaternion
//
//
//
//		/// --------------------------------------------------------------------------------------------- Quaternion //
//		public static void Normalize
//		(
//			ref this Quaternion input)
//		{
//			var n = Mathf.Sqrt(input.x * input.x + input.y * input.y + input.z * input.z + input.w * input.w);
//
//			input = new Quaternion(x: input.x / n,
//			                       y: input.y / n,
//			                       z: input.z / n,
//			                       w: input.w / n);
//		}
//
//
//		public static Quaternion GetNormalized
//		(
//			this Quaternion input)
//		{
//			var n = Mathf.Sqrt(input.x * input.x + input.y * input.y + input.z * input.z + input.w * input.w);
//
//			return new Quaternion(x: input.x / n,
//			                      y: input.y / n,
//			                      z: input.z / n,
//			                      w: input.w / n);
//		}
//
//
//		// Another way? Is it faster? Stopwatch time!
////		public static Quaternion GetNormalized(this Quaternion input)
////		{
////			var lengthD = 1.0f / ( input.w * input.w + input.x * input.x + input.y * input.y + input.z * input.z );
////
////			input.w *= lengthD;
////			input.x *= lengthD;
////			input.y *= lengthD;
////			input.z *= lengthD;
////
////			return input;
////		}
//
//
//		public static void MultiplyBy
//		(
//			ref this Quaternion a,
//			Quaternion          b)
//		{
//			var x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
//			var y = a.w * b.y + a.y * b.w - a.x * b.z + a.z * b.x;
//			var z = a.w * b.z + a.z * b.w + a.x * b.y - a.y * b.x;
//			var w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
//
//			a = new Quaternion(x: x,
//			                   y: y,
//			                   z: z,
//			                   w: w).GetNormalized();
//		}
//
//
//		/// <summary>
//		///     Use this to check if two transforms are rotationally similar.
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		/// 
//		/// <param name="target">
//		///     The transform that we should compare rotations with.
//		/// </param>
//		/// 
//		/// <param name="degrees">
//		///     How many degrees do we allow as a margin of error?
//		/// </param>
//		/// 
//		/// <returns>
//		///     Returns true if the given transforms are rotationally within the given degrees of each other.
//		/// </returns>
//		public static bool IsAlignedWith
//		(
//			this Transform input,
//			Transform      target,
//			float          degrees)
//		{
//			return input.IsAlignedWith(target: target.rotation,
//			                           degrees: degrees);
//		}
//
//
//		/// <summary>
//		///     Use this to check if a transform is rotationally similar to a target rotation.
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		/// 
//		/// <param name="target">
//		///     The rotation that we should compare against.
//		/// </param>
//		/// 
//		/// <param name="degrees">
//		///     How many degrees do we allow as a margin of error?
//		/// </param>
//		/// 
//		/// <returns>
//		///     Returns true if the given transform is rotationally within the given degrees of the target.
//		/// </returns>
//		public static bool IsAlignedWith
//		(
//			this Transform input,
//			Quaternion     target,
//			float          degrees)
//		{
//			return Quaternion.Dot(a: input.rotation,
//			                      b: target) >= degrees.GetDegreesToDotProduct();
//		}
//
//		/// <summary>
//		///     Use this to check if a rotation is similar to another.
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		/// 
//		/// <param name="target">
//		///     The rotation that we should compare against.
//		/// </param>
//		/// 
//		/// <param name="degrees">
//		///     How many degrees do we allow as a margin of error?
//		/// </param>
//		/// 
//		/// <returns>
//		///     Returns true if the given rotation is within the given degrees of the target.
//		/// </returns>
//		public static bool IsAlignedWith
//		(
//			this Quaternion input,
//			Quaternion      target,
//			float           degrees)
//		{
//			return Quaternion.Dot(a: input,
//			                      b: target) >= degrees.GetDegreesToDotProduct();
//		}
//
//
//		/// <summary>
//		///     Returns true if this quaternion is sufficiently close enough to the target quaternion.
//		///     This can be used to check whether or not one of two quaternions which are supposed to be
//		///     very similar but has its component signs reversed (q has the same rotation as -q).
//		/// </summary>
//		public static bool IsCloseTo
//		(
//			this Quaternion input,
//			Quaternion      target)
//		{
//			return Quaternion.Dot(a: input,
//			                      b: target).IsZeroOrHigher();
//		}
//
//
//		/// <summary>
//		///     This inverts the sign of the individual components of a quaternion.
//		///     This is not the same as Quaternion.Inverse().
//		/// </summary>
//		public static void InvertComponentSigns
//		(
//			ref this Quaternion input)
//		{
//			input = new Quaternion(x: -input.x,
//			                       y: -input.y,
//			                       z: -input.z,
//			                       w: -input.w);
//		}
//
//
//		/// <summary>
//		///     This inverts the sign of the individual components of a quaternion.
//		///     This is not the same as Quaternion.Inverse().
//		/// </summary>
//		public static Quaternion GetInvertComponentSigns
//		(
//			this Quaternion input)
//		{
//			return new Quaternion(x: -input.x,
//			                      y: -input.y,
//			                      z: -input.z,
//			                      w: -input.w);
//		}
//
//
//		/// <summary>
//		///     This function returns a new quaternion representing the difference between one quaternion and another.
//		/// </summary>
//		public static Quaternion Delta
//		(
//			this Quaternion input,
//			Quaternion      target)
//		{
//			return Quaternion.Inverse(rotation: input) * target;
//		}
//
//
//		/// <summary>
//		///     This function will restrict a quaternion from overstepping limitInDegrees whenever its value changes.
//		/// 
//		///     A deltaFrame is required in order to achieve this.
//		/// 
//		///     For example, if the difference between this quaternion and its deltaFrame is 35 degrees but the
//		///     limitation is set to 20 degrees, it will only rotate in that direction by 20 degrees instead.
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		/// 
//		/// <param name="deltaFrame">
//		///     A copy of this quaternion from the last frame.
//		/// </param>
//		/// 
//		/// <param name="limitInDegrees">
//		///     Whats the maximum number of degrees this quaternion is allowed to move in one frame?
//		/// </param>
//		public static void Throttle
//		(
//			ref this Quaternion input,
//			Quaternion          deltaFrame,
//			float               limitInDegrees)
//		{
//			if (Quaternion.Angle(a: input,
//			                     b: deltaFrame) < limitInDegrees) return;
//
//			deltaFrame.Delta(target: input).ToAngleAxis(angle: out var angle,
//			                                            axis: out var axis);
//
//			input = deltaFrame * Quaternion.AngleAxis(angle: angle.GetClamp(min: 0.0f,
//			                                                                max: limitInDegrees),
//			                                          axis: axis);
//		}
//
//
//		/// <summary>
//		///     This function will restrict a quaternion from overstepping limitInDegrees whenever its value changes.
//		/// 
//		///     A deltaFrame is required in order to achieve this.
//		/// 
//		///     For example, if the difference between this quaternion and its deltaFrame is 35 degrees but the
//		///     limitation is set to 20 degrees, it will only rotate in that direction by 20 degrees instead.
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		/// 
//		/// <param name="deltaFrame">
//		///     A copy of this quaternion from the last frame.
//		/// </param>
//		/// 
//		/// <param name="limitInDegrees">
//		///     Whats the maximum number of degrees this quaternion is allowed to move in one frame?
//		/// </param>
//		public static Quaternion GetThrottle
//		(
//			this Quaternion input,
//			Quaternion      deltaFrame,
//			float           limitInDegrees)
//		{
//			if (Quaternion.Angle(a: input,
//			                     b: deltaFrame) < limitInDegrees) return input;
//
//			deltaFrame.Delta(target: input).ToAngleAxis(angle: out var angle,
//			                                            axis: out var axis);
//
//			return deltaFrame * Quaternion.AngleAxis(angle: angle.GetClamp(min: 0.0f,
//			                                                               max: limitInDegrees),
//			                                         axis: axis);
//		}
//
//
//		/// <summary>
//		///     This function prevents the quaternion from being any further than x degrees away from a base quaternion.
//		/// 
//		///     For example, if this quaternion is 60 degrees away from the base quaternion and limitInDegrees is 45,
//		///     the quaternion will be set to the same direction except at 45 degrees.
//		/// </summary>
//		public static void Clamp
//		(
//			ref this Quaternion input,
//			Quaternion          baseQuaternion,
//			float               limitInDegrees)
//		{
//			if (Quaternion.Angle(a: input,
//			                     b: baseQuaternion) < limitInDegrees) return;
//
//			baseQuaternion.Delta(target: input).ToAngleAxis(angle: out var angle,
//			                                                axis: out var axis);
//
//			input = baseQuaternion * Quaternion.AngleAxis(angle: angle.GetClamp(min: 0.0f,
//			                                                                    max: limitInDegrees),
//			                                              axis: axis);
//		}
//
//
//		/// <summary>
//		///     This function prevents the quaternion from being any further than x degrees away from a base quaternion.
//		/// 
//		///     For example, if this quaternion is 60 degrees away from the base quaternion and limitInDegrees is 45,
//		///     the quaternion will be set to the same direction except at 45 degrees.
//		/// </summary>
//		public static Quaternion GetClamp
//		(
//			this Quaternion input,
//			Quaternion      baseQuaternion,
//			float           limitInDegrees)
//		{
//			if (Quaternion.Angle(a: input,
//			                     b: baseQuaternion) < limitInDegrees) return input;
//
//			baseQuaternion.Delta(target: input).ToAngleAxis(angle: out var angle,
//			                                                axis: out var axis);
//
//			return baseQuaternion * Quaternion.AngleAxis(angle: angle.GetClamp(min: 0.0f,
//			                                                                   max: limitInDegrees),
//			                                             axis: axis);
//		}
//
//
//		/// <summary>
//		///     Returns a quaternion representing the average of all the quaternions in the array.
//		/// </summary>
//		public static Quaternion GetAverage1
//		(
//			this Quaternion[] input)
//		{
//			// If this doesn't work as expected, replace firstRotation with lastRotation (input[input.Length-1])
//			// The example it came from was using a Stack<T>, which uses the push/pop pattern.
//			// It shouldn't make a difference..?
//
//			if (input.Length == 0) return Quaternion.identity;
//
//			var firstRotation = input[0];
//
//			// This is not the same as having a 'new' quaternion like below!
//			// var output = Quaternion.identity;
//
//			var output = new Quaternion(x: 0.0f,
//			                            y: 0.0f,
//			                            z: 0.0f,
//			                            w: 0.0f);
//
//			for (var i = 0; i < input.Length; i++)
//			{
//				var q = input[i];
//
//				// Before we add the new rotation to the average (mean), we have to check whether the quaternion has
//				// to be inverted. Because q and -q are the same rotation, but cannot be averaged, we have to make sure
//				// they are all the same.
//
//				if (!q.IsCloseTo(firstRotation)) q.InvertComponentSigns();
//
//				//Average the values
//				output.w += q.w;
//
//				output.x += q.x;
//
//				output.y += q.y;
//
//				output.z += q.z;
//
//				input[i] = q;
//			}
//
//			return output.GetNormalized();
//		}
//
//
//		/// <summary>
//		///     Returns a quaternion representing the average of all the quaternions in the list.
//		/// </summary>
//		public static Quaternion GetAverage1
//		(
//			this List<Quaternion> input)
//		{
//			// If this doesn't work as expected, replace firstRotation with lastRotation (input[input.Length-1])
//			// The example it came from was using a Stack<T>, which uses the push/pop pattern.
//			// It shouldn't make a difference..?
//
//			if (input.Count == 0) return Quaternion.identity;
//
//			var firstRotation = input[0];
//
//			// This is not the same as having a 'new' quaternion like below!
//			// var output = Quaternion.identity;
//
//			var output = new Quaternion(x: 0.0f,
//			                            y: 0.0f,
//			                            z: 0.0f,
//			                            w: 0.0f);
//
//
//			for (var i = 0; i < input.Count; i++)
//			{
//				var q = input[i];
//
//				// Before we add the new rotation to the average (mean), we have to check whether the quaternion has
//				// to be inverted. Because q and -q are the same rotation, but cannot be averaged, we have to make sure
//				// they are all the same.
//
//				if (!q.IsCloseTo(firstRotation)) q.InvertComponentSigns();
//
//				//Average the values
//
//				output.w += q.w;
//
//				output.x += q.x;
//
//				output.y += q.y;
//
//				output.z += q.z;
//
//				input[i] = q;
//			}
//
//			return output.GetNormalized();
//		}
//
//
//		public static Quaternion GetAverage2
//		(
//			this List<Quaternion> input)
//		{
//			var c = Quaternion.identity;
//
//			for (var i = 0; i < input.Count; i++)
//			{
//				Average2(cumulative: ref c,
//				         firstRotation: input[0],
//				         newRotation: input[i],
//				         addAmount: i);
//			}
//
//			return c;
//		}
//
//
//		//Get an average (mean) from more then two quaternions (with two, slerp would be used).
//		//Note: this only works if all the quaternions are relatively close together.
//		//Usage: 
//		//-Cumulative is an external Vector4 which holds all the added x y z and w components.
//		//-newRotation is the next rotation to be added to the average pool
//		//-firstRotation is the first quaternion of the array to be averaged
//		//-addAmount holds the total amount of quaternions which are currently added
//		//This function returns the current average quaternion
//		private static Quaternion Average2
//		(
//			ref Quaternion cumulative,
//			Quaternion     firstRotation,
//			Quaternion     newRotation,
//			int            addAmount)
//		{
//			//Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
//			//q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
//			if (!newRotation.IsCloseTo(firstRotation)) newRotation.InvertComponentSigns();
//
//			var addDet = 1f / addAmount;
//
//			cumulative.w += newRotation.w;
//
//			var w = cumulative.w * addDet;
//
//			cumulative.x += newRotation.x;
//
//			var x = cumulative.x * addDet;
//
//			cumulative.y += newRotation.y;
//
//			var y = cumulative.y * addDet;
//
//			cumulative.z += newRotation.z;
//
//			var z = cumulative.z * addDet;
//
//			var lengthD = 1.0f / ( w * w + x * x + y * y + z * z );
//
//			w *= lengthD;
//			x *= lengthD;
//			y *= lengthD;
//			z *= lengthD;
//
//			return new Quaternion(x: x,
//			                      y: y,
//			                      z: z,
//			                      w: w);
//		}
//
//
//		public static Quaternion GetAverage3
//		(
//			this List<Quaternion> input)
//		{
//			var output = new Quaternion(x: 0,
//			                            y: 0,
//			                            z: 0,
//			                            w: 0);
//
//			foreach (var q in input)
//			{
//				if (Quaternion.Dot(a: q,
//				                   b: output) > 0)
//				{
//					output.x += q.x;
//					output.y += q.y;
//					output.z += q.z;
//					output.w += q.w;
//				}
//				else
//				{
//					output.x += -q.x;
//					output.y += -q.y;
//					output.z += -q.z;
//					output.w += -q.w;
//				}
//			}
//
//			var mag = Mathf.Sqrt(output.x * output.x + output.y * output.y + output.z * output.z + output.w * output.w);
//
//			if (!( mag > 0.0001 )) return output;
//
//			output.x /= mag;
//			output.y /= mag;
//			output.z /= mag;
//			output.w /= mag;
//
//			return output;
//		}
//
//
//		/// <summary>
//		///     Returns the quaternion as a Vector3 representing its direction.
//		/// </summary>
//		public static Vector3 ToDirection
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.forward;
//		}
//
//
//		/// <summary>
//		///     Returns the forward vector of a quaternion
//		/// </summary>
//		public static Vector3 Forward
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.forward;
//		}
//
//
//		/// <summary>
//		///     Returns the back vector of a quaternion
//		/// </summary>
//		public static Vector3 Back
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.back;
//		}
//
//
//		/// <summary>
//		///     Returns the up vector of a quaternion
//		/// </summary>
//		public static Vector3 Up
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.up;
//		}
//
//
//		/// <summary>
//		///     Returns the down vector of a quaternion
//		/// </summary>
//		public static Vector3 Down
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.down;
//		}
//
//
//		/// <summary>
//		///     Returns the right vector of a quaternion
//		/// </summary>
//		public static Vector3 Right
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.right;
//		}
//
//
//		/// <summary>
//		///     Returns the left vector of a quaternion
//		/// </summary>
//		public static Vector3 Left
//		(
//			this Quaternion input)
//		{
//			return input * Vector3.left;
//		}
//
//
//
//		#endregion
//
//		#region Int
//
//
//
//		/// ---------------------------------------------------------------------------------------------------- Int //
//		
//		/// Extensions
//		 
//		public static void ToNearest
//		(
//			ref this int input,
//			int          factor)
//		{
//			input = Mathf.RoundToInt((float) input / factor) * factor;
//		}
//
//
//		public static void ToNearestPowerOfTwo
//		(
//			ref this int input)
//		{
//			input = Mathf.ClosestPowerOfTwo(input.GetClamp(min: 2,
//			                                               max: int.MaxValue));
//		}
//
//
//		public static int GetToNearest
//		(
//			this int input,
//			int      factor)
//		{
//			return Mathf.RoundToInt((float) input / factor) * factor;
//		}
//		
//		public static int GetNearestPowerOfTwo
//		(
//			this int input)
//		{
//			return Mathf.ClosestPowerOfTwo(input.GetClamp(min: 2,
//			                                              max: int.MaxValue));
//		}
//
//
//		public static void Clamp
//		(
//			ref this int input,
//			int          min,
//			int          max)
//		{
//			input = Mathf.Clamp(value: input,
//			                    min: min,
//			                    max: max);
//		}
//
//
//		public static int GetClamp
//		(
//			this int input,
//			int      min,
//			int      max)
//		{
////			var output = Mathf.Clamp(input, min, max);
////
////			Vanilla.Core.Log($"Input [{input}] Output [{output}]");
////			
////			return output;
//
//			return Mathf.Clamp(value: input,
//			                   min: min,
//			                   max: max);
//		}
//
//
//		public static float GetNormal
//		(
//			this int input,
//			int      max)
//		{
//			return Mathf.Clamp01((float) input / max);
//		}
//
//
//		public static void Wrap
//		(
//			ref this int input,
//			int          max)
//		{
//			input = Mathf.Clamp(value: input - input / max * max,
//			                    min: 0,
//			                    max: max);
//		}
//
//
//		public static void Wrap
//		(
//			ref this int input,
//			int          min,
//			int          max)
//		{
//			input = Mathf.Clamp(value: input - input / max * max,
//			                    min: min,
//			                    max: max);
//		}
//
//
//		public static int GetWrap
//		(
//			this int input,
//			int      max)
//		{
//			return Mathf.Clamp(value: input - input / max * max,
//			                   min: 0,
//			                   max: max);
//		}
//
//
//		public static int GetWrap
//		(
//			this int input,
//			int      min,
//			int      max)
//		{
//			return Mathf.Clamp(value: input - input / max * max,
//			                   min: min,
//			                   max: max);
//		}
//
//
//		/// Returns
//		public static int GetNearest
//		(
//			int input,
//			int factor)
//		{
//			return Mathf.RoundToInt((float) input / factor) * factor;
//		}
//
//
//		public static void WrapInt
//		(
//			ref int input,
//			int     max)
//		{
//			input = Mathf.Clamp(value: input - input / max * max,
//			                    min: 0,
//			                    max: max);
//		}
//
//
//		public static void WrapInt
//		(
//			ref int input,
//			int     min,
//			int     max)
//		{
//			input = Mathf.Clamp(value: input - input / max * max,
//			                    min: min,
//			                    max: max);
//		}
//
//
//		/// Checks
//
//		public static bool IsValidIndexOf<T>(this int input,
//		                                     T[]      target) => target != null && target.Length != 0 && input >= 0 && input <= target.Length - 1;
//
//		public static bool IsValidIndexOf<T>(this int input,
//		                                     List<T>      target) => target != null && target.Count != 0 && input >= 0 && input <= target.Count - 1;
//
//		public static bool IsWithinInclusiveRange
//		(
//			this int input,
//			int      max)
//		{
//			return input >= 0.0f && input <= max;
//		}
//
//
//		public static bool IsWithinInclusiveRange
//		(
//			this int input,
//			int      min,
//			int      max) => input >= min && input <= max;
//
//
//		public static bool IsWithinExclusiveRange
//		(
//			this int input,
//			int      max)
//		{
//			return input > 0.0f && input < max;
//		}
//
//
//		public static bool IsWithinExclusiveRange
//		(
//			this int input,
//			int      min,
//			int      max)
//		{
//			return input > min && input < max;
//		}
//
//
//		public static bool IsPositive
//		(
//			this int input)
//		{
//			return input > 0;
//		}
//
//
//		public static int GetPositive
//		(
//			this int input)
//		{
//			return Mathf.Abs(input);
//		}
//
//
//		public static int GetNegative
//		(
//			this int input)
//		{
//			return -Mathf.Abs(input);
//		}
//
//
//		/// <summary>
//		///     Returns true if the input value is odd. However, this function is currently untested. It also only works
//		///     for an int, so consider rounding off if you need to check a float.
//		/// </summary>
//		public static bool IsOdd
//		(
//			this int input)
//		{
//			return ( input & 1 ) == 1;
//		}
//
//
//		/// <summary>
//		///     Returns true if the input value is even. However, this function is currently untested. It also only
//		///     works for an int, so consider rounding off if you need to check a float.
//		/// </summary>
//		public static bool IsEven
//		(
//			this int input)
//		{
//			return ( input & 1 ) == 0;
//		}
//
//
//		/// Iteratives
//		public static int GetAverage
//		(
//			this int[] input)
//		{
//			return input == null ?
//				       0 :
//				       input.Sum() / input.Length;
//		}
//
//
//		public static int GetAverage
//		(
//			this List<int> input)
//		{
//			return Vanilla.SoftNullCheck(input) ?
//				       0 :
//				       input.Sum() / input.Count;
//		}
//
//
//
//		#endregion
//
//		#region Int2
//
//
//
//		// ---------------------------------------------------------------------------------------------------- Int2 //
//
//
//		/// <summary>
//		///     Clamps both x and y between 0 and a given maximum.
//		/// </summary>
//		public static void BoxClamp
//		(
//			ref this Vector2Int input,
//			int           max)
//		{
//			input.x = input.x.GetClamp(min: 0,
//			                           max: max);
//
//			input.y = input.y.GetClamp(min: 0,
//			                           max: max);
//		}
//
//
//		/// <summary>
//		///     Clamps both x and y between a given minimum and maximum.
//		/// </summary>
//		public static void BoxClamp
//		(
//			ref this Vector2Int input,
//			int                 min,
//			int                 max)
//		{
//			input.x = input.x.GetClamp(min: min,
//			                           max: max);
//
//			input.y = input.y.GetClamp(min: min,
//			                           max: max);
//		}
//
//
//		/// <summary>
//		///     Clamps x and y between 0 and seperate maximums.
//		/// </summary>
//		public static void RectangleClamp
//		(
//			ref this Vector2Int input,
//			int           xMax,
//			int           yMax)
//		{
//			input.x = input.x.GetClamp(min: 0,
//			                           max: xMax);
//
//			input.y = input.y.GetClamp(min: 0,
//			                           max: yMax);
//		}
//
//
//		/// <summary>
//		///     Clamps x and y between seperate minimums and maximums.
//		/// </summary>
//		public static void RectangleClamp
//		(
//			ref this Vector2Int input,
//			int           xMin,
//			int           xMax,
//			int           yMin,
//			int           yMax)
//		{
//			input.x = input.x.GetClamp(min: xMin,
//			                           max: xMax);
//
//			input.y = input.y.GetClamp(min: yMin,
//			                           max: yMax);
//		}
//
//
//		public static Vector2Int GetClamp
//		(
//			this Vector2Int input,
//			int             max)
//			=> new Vector2Int(x: input.x.GetClamp(min: 0,
//			                                      max: max),
//			                  y: input.y.GetClamp(min: 0,
//			                                      max: max));
//
//
//		public static Vector2Int GetClamp
//		(
//			this Vector2Int input,
//			int             min,
//			int             max)
//			=> new Vector2Int(x: input.x.GetClamp(min: min,
//			                                      max: max),
//			                  y: input.y.GetClamp(min: min,
//			                                      max: max));
//
//
//
//		#endregion
//
//		#region Int3
//
//
//
//		// ---------------------------------------------------------------------------------------------------- Int3 //
//
//
//		public static Vector3Int GetRectangleClampInt
//		(
//			this Vector3 input,
//			float        xMin,
//			float        yMin,
//			float        zMin,
//			float        xMax,
//			float        yMax,
//			float        zMax)
//		{
//			return new Vector3Int(x: (int) input.x.GetClamp(min: xMin,
//			                                                max: xMax),
//			                      y: (int) input.y.GetClamp(min: yMin,
//			                                                max: yMax),
//			                      z: (int) input.z.GetClamp(min: zMin,
//			                                                max: zMax));
//		}
//
//
//		public static void RectangleClampInt
//		(
//			ref this Vector3Int input,
//			int                 xMax,
//			int                 yMax,
//			int                 zMax)
//		{
//			input.x = input.x.GetClamp(min: 0,
//			                           max: xMax);
//
//			input.y = input.y.GetClamp(min: 0,
//			                           max: yMax);
//
//			input.z = input.z.GetClamp(min: 0,
//			                           max: zMax);
//		}
//
//
//		public static Vector3Int GetRectangleClampInt
//		(
//			this Vector3Int input,
//			int             xMax,
//			int             yMax,
//			int             zMax) => new Vector3Int(x: input.x.GetClamp(min: 0,
//			                                                            max: xMax),
//			                                        y: input.y.GetClamp(min: 0,
//			                                                            max: yMax),
//			                                        z: input.z.GetClamp(min: 0,
//			                                                            max: zMax));
//
//
//		public static void RectangleClampInt
//		(
//			ref this Vector3Int input,
//			int                 xMin,
//			int                 yMin,
//			int                 zMin,
//			int                 xMax,
//			int                 yMax,
//			int                 zMax)
//		{
//			input.x = input.x.GetClamp(min: xMin,
//			                           max: xMax);
//
//			input.y = input.y.GetClamp(min: yMin,
//			                           max: yMax);
//
//			input.z = input.z.GetClamp(min: zMin,
//			                           max: zMax);
//		}
//
//
//		public static Vector3Int GetRectangleClampInt
//		(
//			this Vector3Int input,
//			int             xMin,
//			int             yMin,
//			int             zMin,
//			int             xMax,
//			int             yMax,
//			int             zMax)
//		{
//			return new Vector3Int(x: input.x.GetClamp(min: xMin,
//			                                          max: xMax),
//			                      y: input.y.GetClamp(min: yMin,
//			                                          max: yMax),
//			                      z: input.z.GetClamp(min: zMin,
//			                                          max: zMax));
//		}
//
//
//
//		#endregion
//
//		#region Line2
//
//
//
//		/// -------------------------------------------------------------------------------------------------- Line2 //
//
//
//
//		#endregion
//
//		#region Line3
//
//
//
//		/// -------------------------------------------------------------------------------------------------- Line3 //
//		public static Vector3 NearestPointApprox
//		(
//			this Line3 input,
//			Vector3    point)
//		{
//			var closestPoint = Vector3.Dot(lhs: point - input.position,
//			                               rhs: input.direction);
//
//			return input.position + closestPoint * input.direction;
//		}
//
//
//		public static Vector3 NearestPointStrict
//		(
//			this Line3 input,
//			Vector3    point)
//		{
//			var normalizedDirection = input.direction.normalized;
//
//			var closestPoint = Vector3.Dot(lhs: point - input.position,
//			                               rhs: normalizedDirection);
//
//			return input.position + Mathf.Clamp(value: closestPoint,
//			                                    min: 0.0f,
//			                                    max: Vector3.Magnitude(input.direction)) * normalizedDirection;
//		}
//
//
//		public static bool IsParallelTo
//		(
//			this Line3 input,
//			Line3      target)
//		{
//			var a = Vector3.Dot(lhs: input.direction,
//			                    rhs: input.direction);
//
//			var b = Vector3.Dot(lhs: input.direction,
//			                    rhs: target.direction);
//
//			var e = Vector3.Dot(lhs: target.direction,
//			                    rhs: target.direction);
//
//			var d = a * e - b * b;
//
//			return d.IsZero();
//		}
//
//
//
//		#endregion
//
//	}
//
//}
//
//// ---------------------------------------------------------------------------------------------------------- Math2D //
//
//namespace Vanilla.Core.Math
//{
//
//	public static class VanillaMath2D
//	{
//
//		//------------------------------------------------------------------------------------------------------------------
//		// Radians
//		//------------------------------------------------------------------------------------------------------------------
//		// Radians are essentially the normalized length of the circumference of a circle [e.g. the circle line itself].
//		// 1 radian is also the length of a circles radius.
//		// Pi is 3.1415 radians and is the length of half of a circles circumference. 2 of pi would be the whole circle!
//		// Lots of angle-related math use radians.
//		//------------------------------------------------------------------------------------------------------------------
//
//
//
//


//

//
//	}
//
//}
//
/////--------------------------------------------------------------------------------------------------------------------------------------------------
///// Math3D
/////--------------------------------------------------------------------------------------------------------------------------------------------------
//
//namespace Vanilla.Core.Math
//{
//
//	public static class VanillaMath3D
//	{
//
//		public enum Vec2SwizzleFormat
//		{
//
//			XY,
//			YZ
//
//		}
//
////		public enum Vec3ProjectionFormat
////		{
////
////			XYZ,
////			XZY,
////			YXZ,
////			YZX,
////			ZXY,
////			ZYX
////
////		}
//
//		public enum V3SwizzleFormat
//		{
//
//			XYZ,
//			XZY,
//			YXZ,
//			YZX,
//			ZXY,
//			ZYX
//
//		}
//
//		public enum V3ToV2SwizzleFormat
//		{
//
//			XY,
//			XZ,
//			YX,
//			YZ,
//			ZX,
//			ZY
//
//		}
//
//		// -------------------------------------------------------------------------------- Inexpensive Distance Checks
//
//
//		public static float GetSqrMag
//		(
//			this Vector3 input,
//			Vector3      target)
//		{
//			return ( input - target ).sqrMagnitude;
//		}
//
//
//		public static bool IsWithinDistance
//		(
//			this Vector3 input,
//			Vector3      target,
//			float        distance)
//		{
//			return ( input - target ).sqrMagnitude < distance * distance;
//		}
//
//
//		public static T GetNearestFromArray<T>
//		(
//			T[]     array,
//			Vector3 targetPosition)
//			where T : MonoBehaviour
//		{
//			var shortestSqrLength = float.MaxValue;
//			var nearestIndex      = int.MaxValue;
//
//			for (var i = 0; i < array.Length; i++)
//			{
//				var sqrLength = ( array[i].transform.position - targetPosition ).sqrMagnitude;
//
//				if (sqrLength > shortestSqrLength) continue;
//
//				shortestSqrLength = sqrLength;
//				nearestIndex      = i;
//			}
//
//			return array[nearestIndex];
//		}
//
//
//		public static T GetNearestFromList<T>
//		(
//			List<T> list,
//			Vector3 targetPosition)
//			where T : MonoBehaviour
//		{
//			var shortestSqrLength = float.MaxValue;
//			var nearestIndex      = int.MaxValue;
//
//			for (var i = 0; i < list.Count; i++)
//			{
//				var sqrLength = ( list[i].transform.position - targetPosition ).sqrMagnitude;
//
//				if (sqrLength > shortestSqrLength) continue;
//
//				shortestSqrLength = sqrLength;
//				nearestIndex      = i;
//			}
//
//			return list[nearestIndex];
//		}
//
//
//		public static int GetNearestFromArray
//		(
//			Vector3[] array,
//			Vector3   targetPosition)
//		{
//			var shortestSqrLength = float.MaxValue;
//			var nearestIndex      = int.MaxValue;
//
//			for (var i = 0; i < array.Length; i++)
//			{
//				var sqrLength = ( array[i] - targetPosition ).sqrMagnitude;
//
//				if (sqrLength > shortestSqrLength) continue;
//
//				shortestSqrLength = sqrLength;
//				nearestIndex      = i;
//			}
//
//			return nearestIndex;
//		}
//
//
//		public static Transform GetNearestOfTwo
//		(
//			Transform a,
//			Transform b,
//			Vector3   targetPosition)
//		{
//			return
//				( a.position - targetPosition ).sqrMagnitude
//			   <
//				( b.position - targetPosition ).sqrMagnitude ?
//					a :
//					b;
//		}
//
//
//		public static Transform GetNearestOfTwo<T>
//		(
//			T       a,
//			T       b,
//			Vector3 targetPosition)
//			where T : MonoBehaviour
//		{
//			return
//				( a.transform.position - targetPosition ).sqrMagnitude
//			   <
//				( b.transform.position - targetPosition ).sqrMagnitude ?
//					a.transform :
//					b.transform;
//		}
//
//
//		public static int GetNearestFromList
//		(
//			List<Vector3> list,
//			Vector3       targetPosition)
//		{
//			var shortestSqrLength = float.MaxValue;
//			var nearestIndex      = int.MaxValue;
//
//			for (var i = 0; i < list.Count; i++)
//			{
//				var sqrLength = ( list[i] - targetPosition ).sqrMagnitude;
//
//				if (sqrLength > shortestSqrLength) continue;
//
//				shortestSqrLength = sqrLength;
//				nearestIndex      = i;
//			}
//
//			return nearestIndex;
//		}
//
//
//		public static bool AIsCloserThanB
//		(
//			Vector3 a,
//			Vector3 b,
//			Vector3 targetPosition)
//		{
//			return ( a - targetPosition ).sqrMagnitude < ( b - targetPosition ).sqrMagnitude;
//		}
//
//
//		public static Vector3 GetNearest
//		(
//			Vector3 a,
//			Vector3 b,
//			Vector3 targetPosition)
//		{
//			return ( a - targetPosition ).sqrMagnitude < ( b - targetPosition ).sqrMagnitude ?
//				       a :
//				       b;
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Vector2
//		/// <
//		/// >
//		/// Vector3 Remapping
//		/// --------------------------------------------------------------------------------------
//
////		public static Vector3 Swizzle(this Vector3 input,  Vec3ProjectionFormat projectionFormat)
////		{
////			
////		}
//		public static Vector3 MapVector2ToVector3
//		(
//			Vector2              input,
//			V3SwizzleFormat projectionFormat,
//			float                z = 1.0f)
//		{
//			switch (projectionFormat)
//			{
//				case V3SwizzleFormat.XYZ:
//
//					return input;
//
//				case V3SwizzleFormat.XZY:
//
//					return new Vector3(x: input.x,
//					                   y: z,
//					                   z: input.y);
//
//				case V3SwizzleFormat.YXZ:
//
//					return new Vector3(x: input.y,
//					                   y: input.x,
//					                   z: z);
//
//				case V3SwizzleFormat.YZX:
//
//					return new Vector3(x: input.y,
//					                   y: z,
//					                   z: input.x);
//
//				case V3SwizzleFormat.ZXY:
//
//					return new Vector3(x: z,
//					                   y: input.x,
//					                   z: input.y);
//
//				case V3SwizzleFormat.ZYX:
//
//					return new Vector3(x: z,
//					                   y: input.y,
//					                   z: input.x);
//
//				default:
//
//					return input;
//			}
//		}
//
//
//		public static Vector3 MapVector2ToVector3
//		(
//			float                x,
//			float                y,
//			V3SwizzleFormat projectionFormat,
//			float                z = 1.0f)
//		{
//			switch (projectionFormat)
//			{
//				case V3SwizzleFormat.XYZ:
//
//					return new Vector3(x: x,
//					                   y: y,
//					                   z: z);
//
//				case V3SwizzleFormat.XZY:
//
//					return new Vector3(x: x,
//					                   y: z,
//					                   z: y);
//
//				case V3SwizzleFormat.YXZ:
//
//					return new Vector3(x: y,
//					                   y: x,
//					                   z: z);
//
//				case V3SwizzleFormat.YZX:
//
//					return new Vector3(x: y,
//					                   y: z,
//					                   z: x);
//
//				case V3SwizzleFormat.ZXY:
//
//					return new Vector3(x: z,
//					                   y: x,
//					                   z: y);
//
//				case V3SwizzleFormat.ZYX:
//
//					return new Vector3(x: z,
//					                   y: y,
//					                   z: x);
//
//				default:
//
//					return new Vector3(x: x,
//					                   y: y,
//					                   z: z);
//			}
//		}
//
//
//		public static Vector2 MapVector3ToVector2
//		(
//			Vector3                          input,
//			V3ToV2SwizzleFormat projectionFormat)
//		{
//			switch (projectionFormat)
//			{
//				case V3ToV2SwizzleFormat.XY:
//
//					return input;
//
//				case V3ToV2SwizzleFormat.XZ:
//
//					return new Vector2(x: input.x,
//					                   y: input.z);
//
//				case V3ToV2SwizzleFormat.YX:
//
//					return new Vector2(x: input.y,
//					                   y: input.x);
//
//				case V3ToV2SwizzleFormat.YZ:
//
//					return new Vector2(x: input.y,
//					                   y: input.z);
//
//				case V3ToV2SwizzleFormat.ZX:
//
//					return new Vector2(x: input.z,
//					                   y: input.x);
//
//				case V3ToV2SwizzleFormat.ZY:
//
//					return new Vector2(x: input.z,
//					                   y: input.y);
//
//				default:
//
//					return input;
//			}
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Drawing 3D representations of 2D shapes
//		/// --------------------------------------------------------------------------------------
//		/// <summary>
//		///     This can be used within a 'for' loop to get Vector3s that progressively make up a circle.
//		/// </summary>
//		/// 
//		/// <param name="positionId">
//		///     The position in a for loop. It doesn't need to be from a for loop, but this is the intended use-case.
//		/// </param>
//		/// 
//		/// <param name="angleSize">
//		///     The angle to iterate by. It is best to pre-calculate this by dividing 360 by the for loop maximum.
//		/// </param>
//		/// 
//		/// <param name="radius">The radius of the circular output positions.</param>
//		/// 
//		/// <example>
//		///     // Draws a circle with a diameter of 3 world-units, facing upwards.
//		/// 
//		///     int circleResolution = 64;
//		/// 
//		///     float angleSize = 360.0f / circleResolution;
//		/// 
//		///     for (int i = 0; i lessthan circleResolution; i++)
//		///     {
//		///     lineRenderer.SetPosition(i, DrawIterativeCircle(i, angleSize, 1.5f);
//		///     }
//		/// </example>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawIterativeCircle
//		(
//			int   positionId,
//			float angleSize,
//			float radius)
//		{
//			var angle = positionId * angleSize * Mathf.Deg2Rad;
//
//			return new Vector3(x: Mathf.Cos(f: angle) * radius,
//			                   y: 0,
//			                   z: Mathf.Sin(f: angle) * radius);
//		}
//
//
//		/// <summary>
//		///     This can be used within a 'for' loop to get Vector3s that progressively make up a circle.
//		/// </summary>
//		/// 
//		/// <param name="positionId">
//		///     The position in a for loop. It doesn't need to be from a for loop, but this is the intended use-case.
//		/// </param>
//		/// 
//		/// <param name="angleSize">
//		///     The angle to iterate by. It is best to pre-calculate this by dividing 360 by the for loop maximum.
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     The radius of the circular output positions.
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <example>
//		///     // Draws a circle with a diameter of 1 world-unit facing global forwards.
//		/// 
//		///     int circleResolution = 64;
//		/// 
//		///     float angleSize = 360.0f / circleResolution;
//		/// 
//		///     for (int i = 0; i lessthan circleResolution; i++)
//		///     {
//		///     lineRenderer.SetPosition(i, DrawIterativeCircle(i, angleSize, 0.5f, V3SwizzleFormat.XYo);
//		///     }
//		/// </example>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawIterativeCircle
//		(
//			int                  positionId,
//			float                angleSize,
//			float                radius,
//			V3SwizzleFormat projectionFormat)
//		{
//			var angle = positionId * angleSize * Mathf.Deg2Rad;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		/// <summary>
//		///     This can be used within a 'for' loop to get Vector3s that progressively make up a circle.
//		/// </summary>
//		/// 
//		/// <param name="positionId">
//		///     The position in a for loop. It doesn't need to be from a for loop, but this is the intended use-case.
//		/// </param>
//		/// 
//		/// <param name="angleSize">
//		///     The angle to iterate by. It is best to pre-calculate this by dividing 360 by the for loop maximum.
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     The radius of the circular output positions.
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <param name="clockwise">
//		///     In which direction is the circle being drawn?
//		/// </param>
//		/// 
//		/// <example>
//		///     // Draws a circle with a diameter of 4 world-units, counter-clockwise and facing upwards.
//		/// 
//		///     int circleResolution = 64;
//		/// 
//		///     float angleSize = 360.0f / circleResolution;
//		/// 
//		///     for (int i = 0; i less than circleResolution; i++)
//		///     {
//		///     lineRenderer.SetPosition(i, DrawIterativeCircle(i, angleSize, 2.0f, V3SwizzleFormat.XoY, false);
//		///     }
//		/// </example>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawIterativeCircle
//		(
//			int                  positionId,
//			float                angleSize,
//			float                radius,
//			V3SwizzleFormat projectionFormat,
//			bool                 clockwise)
//		{
//			var angle = positionId * angleSize * Mathf.Deg2Rad;
//
//			if (clockwise) angle = -angle;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		/// <summary>
//		///     This can be used within a 'for' loop to get Vector3s that progressively make up a circle.
//		/// </summary>
//		/// 
//		/// <param name="positionId">
//		///     The position in a for loop. It doesn't need to be from a for loop, but this is the intended use-case.
//		/// </param>
//		/// 
//		/// <param name="angleSize">
//		///     The angle to iterate by. It is best to pre-calculate this by dividing 360 by the for loop maximum.
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     The radius of the circular output positions.
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <param name="clockwise">
//		///     In which direction is the circle being drawn?
//		/// </param>
//		/// 
//		/// <param name="offset">
//		///     How offset should the points be as a starting point?
//		/// </param>
//		/// 
//		/// <example>
//		///     // Draws a circle with a diameter of 4 world-units, counter-clockwise and facing upwards.
//		/// 
//		///     int circleResolution = 64;
//		/// 
//		///     float angleSize = 360.0f / circleResolution;
//		/// 
//		///     for (int i = 0; i lessthan circleResolution; i++)
//		///     {
//		///     lineRenderer.SetPosition(i, DrawIterativeCircle(i, angleSize, 2.0f, V3SwizzleFormat.XoY, false);
//		///     }
//		/// </example>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawIterativeCircle
//		(
//			int                  positionId,
//			float                angleSize,
//			float                radius,
//			V3SwizzleFormat projectionFormat,
//			bool                 clockwise,
//			float                offset)
//		{
//			var angle = positionId * angleSize + offset * Mathf.Deg2Rad;
//
//			if (clockwise) angle = -angle;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		/// <summary>
//		///     This can be used with a normal float value (0.0f - 1.0f) in order to successively return the points of a circle as
//		///     a Vector3.
//		/// </summary>
//		/// 
//		/// <param name="normal">
//		///     Between 0 and 1, which point on the circle do we want?
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     How big should the circle be?
//		/// </param>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawPerfectCircle
//		(
//			float normal,
//			float radius)
//		{
//			var angle = Mathf.Lerp(a: 0.0f,
//			                       b: 360.0f,
//			                       t: normal) * Mathf.Deg2Rad;
//
//			return new Vector3(x: Mathf.Cos(f: angle) * radius,
//			                   y: 0.0f,
//			                   z: Mathf.Sin(f: angle) * radius);
//		}
//
//
//		/// <summary>
//		///     This can be used with a normal float value (0.0f - 1.0f) in order to successively return the points of a circle as
//		///     a Vector3.
//		/// </summary>
//		/// 
//		/// <param name="normal">
//		///     Between 0 and 1, which point on the circle do we want?
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     How big should the circle be?
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawPerfectCircle
//		(
//			float                normal,
//			float                radius,
//			V3SwizzleFormat projectionFormat)
//		{
//			var angle = Mathf.Lerp(a: 0.0f,
//			                       b: 360.0f,
//			                       t: normal) * Mathf.Deg2Rad;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		/// <summary>
//		///     This can be used with a normal float value (0.0f - 1.0f) in order to successively return the points of a circle as
//		///     a Vector3.
//		/// </summary>
//		/// 
//		/// <param name="normal">
//		///     Between 0 and 1, which point on the circle do we want?
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     How big should the circle be?
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <param name="clockwise">
//		///     In which direction is the circle being drawn?
//		/// </param>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawPerfectCircle
//		(
//			float                normal,
//			float                radius,
//			V3SwizzleFormat projectionFormat,
//			bool                 clockwise)
//		{
//			var angle = clockwise ?
//				            -Mathf.Lerp(a: 0.0f,
//				                        b: 360.0f,
//				                        t: normal) * Mathf.Deg2Rad :
//				            Mathf.Lerp(a: 0.0f,
//				                       b: 360.0f,
//				                       t: normal) * Mathf.Deg2Rad;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		/// <summary>
//		///     This can be used with a normal float value (0.0f - 1.0f) in order to successively return the points of a circle as
//		///     a Vector3.
//		/// </summary>
//		/// 
//		/// <param name="normal">
//		///     Between 0 and 1, which point on the circle do we want?
//		/// </param>
//		/// 
//		/// <param name="radius">
//		///     How big should the circle be?
//		/// </param>
//		/// 
//		/// <param name="projectionFormat">
//		///     Which Vector3 co-ordinate format should the returned points come in?
//		/// </param>
//		/// 
//		/// <param name="clockwise">
//		///     In which direction is the circle being drawn?
//		/// </param>
//		/// 
//		/// <param name="offset">
//		///     How offset should the points be as a starting point?
//		/// </param>
//		/// 
//		/// <returns>
//		///     A Vector3 representation of a point on the circumference of a circle with the given radius.
//		/// </returns>
//		public static Vector3 DrawPerfectCircle
//		(
//			float                normal,
//			float                radius,
//			V3SwizzleFormat projectionFormat,
//			bool                 clockwise,
//			float                offset)
//		{
//			var angle = clockwise ?
//				            -( Mathf.Lerp(a: 0.0f,
//				                          b: 360.0f,
//				                          t: normal) + offset ) * Mathf.Deg2Rad :
//				            ( Mathf.Lerp(a: 0.0f,
//				                         b: 360.0f,
//				                         t: normal) + offset ) * Mathf.Deg2Rad;
//
//			return MapVector2ToVector3(x: Mathf.Cos(f: angle) * radius,
//			                           y: Mathf.Sin(f: angle) * radius,
//			                           projectionFormat: projectionFormat);
//		}
//
//
//		public static int GetCirclePointIDFromNormal
//		(
//			float normal,
//			int   pointCount)
//		{
//			return Mathf.CeilToInt(f: Mathf.Lerp(a: 0,
//			                                     b: pointCount,
//			                                     t: normal));
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Vector Comparisons
//		/// --------------------------------------------------------------------------------------
//		public static Vector3 GetDirectionTo
//		(
//			this Vector3 input,
//			Vector3      target)
//		{
//			return target - input;
//		}
//
//
//		public static Vector3 GetDirectionTo
//		(
//			this Vector3 input,
//			Transform    target)
//		{
//			return target.position - input;
//		}
//
//
//		public static Vector3 GetDirectionTo
//		(
//			this Transform input,
//			Vector3        target)
//		{
//			return target - input.position;
//		}
//
//
//		public static Vector3 GetDirectionTo
//		(
//			this Transform input,
//			Transform      target)
//		{
//			return target.position - input.position;
//		}
//
//
//		public static Vector3 GetNormalizedDirectionTo
//		(
//			this Vector3 input,
//			Vector3      target)
//		{
//			return ( target - input ).normalized;
//		}
//
//
//		public static Vector3 GetNormalizedDirectionTo
//		(
//			this Vector3 input,
//			Transform    target)
//		{
//			return ( target.position - input ).normalized;
//		}
//
//
//		public static Vector3 GetNormalizedDirectionTo
//		(
//			this Transform input,
//			Vector3        target)
//		{
//			return ( target - input.position ).normalized;
//		}
//
//
//		public static Vector3 GetNormalizedDirectionTo
//		(
//			this Transform input,
//			Transform      target)
//		{
//			return ( target.position - input.position ).normalized;
//		}
//
//
//		// ------------------------------------------------------------------- Directional / Dot Product Comparisons //
//
//
//		public static void DegreesToDotProduct
//		(
//			ref this float input)
//		{
//			input = GetDegreesToDotProduct(input);
//		}
//
//
//		public static float GetDegreesToDotProduct
//		(
//			this float degrees)
//		{
//			return Mathf.Cos(f: degrees.GetClamp(min: 0.0f,
//			                                     max: 180.0f) * Mathf.Deg2Rad);
//		}
//
//		/// <summary>
//		/// 	Treating this vector as a direction, is the dot product of these two vectors
//		/// 	within degrees of each other?
//		/// </summary>
//		/// 
//		/// <param name="input"></param>
//		///
//		/// <param name="direction">
//		///		Another vector treated as a direction. i.e. targetTransform.forward
//		/// </param>
//		/// 
//		/// <param name="degrees">
//		///		How many degrees should we use to compare these directions?
//		/// </param>
//		///
//		/// <returns>
//		///		True if the degrees between the two directional vectors are less than 'degrees'.
//		/// </returns>
//		public static bool IsAlignedWith
//		(
//			this Vector3 input,
//			Vector3      direction,
//			float        degrees)
//		{
//			input.Normalize();
//
//			direction.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: direction,
//			                                 degrees: degrees);
//		}
//
//
//		// Transform ------------------------------------------------------------------------------------- Is Facing //
//
//
//		public static bool IsLookingAt
//		(
//			this Transform input,
//			Transform      target,
//			float          marginOfErrorInDegrees)
//		{
//			return input.IsLookingAt(target: target.position,
//			                      marginOfErrorInDegrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsLookingAt
//		(
//			this Transform input,
//			Vector3        target,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: input.GetNormalizedDirectionTo(target: target),
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingUp
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.up,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingDown
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.down,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingNorth
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.forward,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingSouth
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.back,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingEast
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.right,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingWest
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			return Normalized3DDotProductCheck(a: input.forward,
//			                                 b: Vector3.left,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingHorizon
//		(
//			this Transform input,
//			float          marginOfErrorInDegrees)
//		{
//			var dot = ( 90 - marginOfErrorInDegrees ).GetDegreesToDotProduct();
//
//			return Vector3.Dot(lhs: input.forward,
//			                   rhs: Vector3.up).IsWithinInclusiveRange(min: -dot,
//			                                                           max: dot);
//		}
//
//
//		// Vector3 --------------------------------------------------------------------------------------- Is Facing //
//
//
//		public static bool IsFacingUp
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.up,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingDown
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.down,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingNorth
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.forward,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingSouth
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.back,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingEast
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.right,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		public static bool IsFacingWest
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
//			input.Normalize();
//
//			return Normalized3DDotProductCheck(a: input,
//			                                 b: Vector3.left,
//			                                 degrees: marginOfErrorInDegrees);
//		}
//
//
//		/// <summary>
//		///     This Vector3.Dot check assumes that the values passed into it are normalized, allowing other methods
//		///     to not double up on normalization or its checks for efficiency reasons. For this reason, it has
//		///     also been made private to prevent accidental usage outside this class.
//		/// </summary>
//		private static bool Normalized3DDotProductCheck
//		(
//			Vector3 a,
//			Vector3 b,
//			float   degrees)
//		{
//			return Vector3.Dot(lhs: a,
//			                   rhs: b) >= degrees.GetDegreesToDotProduct();
//		}
//
//
//		public static bool IsFacingHorizon
//		(
//			this Vector3 input,
//			float        marginOfErrorInDegrees)
//		{
////			marginOfErrorNormal = Mathf.Clamp01(marginOfErrorNormal);
////
////			float dot = Vector3.Dot(input, Vector3.up);
////			return dot > -marginOfErrorNormal && dot < marginOfErrorNormal;
//
//			input.Normalize();
//
//			var dot = marginOfErrorInDegrees.GetDegreesToDotProduct();
//
//			return Vector3.Dot(lhs: input,
//			                   rhs: Vector3.up).IsWithinInclusiveRange(min: -dot,
//			                                                           max: dot);
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Grids and Snapping
//		/// --------------------------------------------------------------------------------------
//		/// <summary>
//		///     This function will return a vector snapped to the nearest point on a uniform grid using the given parameters.
//		/// </summary>
//		public static Vector3 RoundUniform
//		(
//			Vector3 v,
//			float   size)
//		{
//			return new Vector3(x: Mathf.Round(f: v.x / size) * size,
//			                   y: Mathf.Round(f: v.y / size) * size,
//			                   z: Mathf.Round(f: v.z / size) * size);
//		}
//
//
//		/// <summary>
//		///     This function will return a vector snapped to the nearest point on a dynamic non-uniform grid using the given
//		///     parameters.
//		/// </summary>
//		public static Vector3 RoundDynamic
//		(
//			Vector3 v,
//			float   xSize,
//			float   ySize,
//			float   zSize)
//		{
//			return new Vector3(x: Mathf.Round(f: v.x / xSize) * xSize,
//			                   y: Mathf.Round(f: v.y / ySize) * ySize,
//			                   z: Mathf.Round(f: v.z / zSize) * zSize);
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Quaternion Control
//		/// --------------------------------------------------------------------------------------
//		/// <summary>
//		///     This function will return a quaternion equal to the difference between inputs a and b
//		/// </summary>
//		public static Quaternion GetQuaternionDifference
//		(
//			Quaternion a,
//			Quaternion b)
//		{
//			return a * Quaternion.Inverse(b);
//		}
//
//
//		/// --------------------------------------------------------------------------------------
//		/// Parabolas
//		/// --------------------------------------------------------------------------------------
//		/// <summary>
//		///     This function will return a specific point on a parabola based on timeNormal (0-1)
//		/// </summary>
//		/// <param name="start">The starting position of the parabola</param>
//		/// <param name="end">The ending positon of the parabola</param>
//		/// <param name="height">The height of the parabola</param>
//		/// <param name="timeNormal">
//		///     Which point in the parabola are we getting? 0 being the start position and 1 being end
//		///     position.
//		/// </param>
//		/// <returns></returns>
//		public static Vector3 GetPositionInParabola
//		(
//			Vector3 start,
//			Vector3 end,
//			float   height,
//			float   timeNormal)
//		{
//			timeNormal = Mathf.Clamp01(timeNormal);
//
//			float Plot
//			(
//				float x)
//			{
//				return -4 * height * x * x + 4 * height * x;
//			}
//
//			var mid = Vector3.Lerp(a: start,
//			                       b: end,
//			                       t: timeNormal);
//
//			return new Vector3(x: mid.x,
//			                   y: Plot(x: timeNormal) + Mathf.Lerp(a: start.y,
//			                                                       b: end.y,
//			                                                       t: timeNormal),
//			                   z: mid.z);
//		}
//
//
//		/// <summary>
//		///     This function returns an entire array of Vector3s that make up a parabola using the given parameters.
//		/// </summary>
//		/// <param name="points"></param>
//		/// <param name="start">The starting position of the parabola</param>
//		/// <param name="end">The ending positon of the parabola</param>
//		/// <param name="height">The height of the parabola</param>
//		/// <param name="normalTimeStep">
//		///     How quickly should we iterate through the normal for calculations? This value should be
//		///     between 0-1, and the smaller it is, the more points will be calculated.
//		/// </param>
//		/// <returns></returns>
//		public static Vector3[] GetParabolaPositions
//		(
//			List<Vector3> points,
//			Vector3       start,
//			Vector3       end,
//			float         height,
//			float         normalTimeStep)
//		{
//			normalTimeStep = Mathf.Clamp(value: normalTimeStep,
//			                             min: 0.001f,
//			                             max: 1.0f);
//
//			points.Clear();
//
//			float i = 0;
//
//			float Plot
//			(
//				float x)
//			{
//				return -4 * height * x * x + 4 * height * x;
//			}
//
//			while (i < 1.0f)
//			{
//				var mid = Vector3.Lerp(a: start,
//				                       b: end,
//				                       t: i);
//
//				points.Add(item: new Vector3(x: mid.x,
//				                             y: Plot(x: i) + Mathf.Lerp(a: start.y,
//				                                                        b: end.y,
//				                                                        t: i),
//				                             z: mid.z));
//
//				i += normalTimeStep;
//			}
//
//			return points.ToArray();
//		}
//
//
//		/// <summary>
//		///     Returns a Vector3 smoothed out between a and b over time multipled by the power of the distance between them. The
//		///     effect is that output will be exaggerated based on the distance, so small distances will happen very slowly but big
//		///     distances will happen very quickly.
//		/// </summary>
//		/// 
//		/// <param name="a">
//		///     The 'from' vector.
//		/// </param>
//		/// 
//		/// <param name="b">
//		///     The 'to' vector.
//		/// </param>
//		public static Vector3 SmoothByDifference
//		(
//			Vector3 a,
//			Vector3 b)
//		{
//			return Vector3.MoveTowards(current: a,
//			                           target: b,
//			                           maxDistanceDelta: Time.deltaTime * Mathf.Pow(f: Vector3.Distance(a: a,
//			                                                                                            b: b) * 10,
//			                                                                        p: 2));
//		}
//
//	}
//
//}