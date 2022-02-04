using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.UnityExtensions
{

	public static class PolygonExtensions
	{

		/// <summary>
		///     Returns the area of a concave 2D polygon.
		/// 
		///     The input positions are expected to be ordered in a clockwise manner. If the input is ordered
		///     counter-clockwise, simply negate the returned value.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>A float representing the area of the polygon in metres squared.</returns>
		public static float GetAreaOfConcavePolygon(this List<Vector2> input)
		{
			var output = 0.0f;

			var n = input.Count;

			var nMinusOne = n - 1;

			// By only going up to nMinusOne and handling the last iteration manually, we can avoid
			// using Wrap() 
			for (var i = 0;
			     i < nMinusOne;
			     i++) output += input[i].x * input[i + 1].y;

			output += input[nMinusOne].x * input[0].y;

			for (var i = 0;
			     i < nMinusOne;
			     i++) output -= input[i + 1].x * input[i].y;

			output -= input[0].x * input[nMinusOne].y;


			return -output / 2.0f;
		}


		/// <summary>
		///     Returns the area of a concave 2D polygon.
		/// 
		///     The input positions are expected to be ordered in a clockwise manner. If the input is ordered
		///     counter-clockwise, simply negate the returned value.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>A float representing the area of the polygon in metres squared.</returns>
		public static float GetAreaOfConcavePolygon(this Vector2[] input)
		{
			var output = 0.0f;

			var n = input.Length;

			var nMinusOne = n - 1;

			// By only going up to nMinusOne and handling the last iteration manually, we can avoid
			// using Wrap() 
			for (var i = 0;
			     i < nMinusOne;
			     i++) output += input[i].x * input[i + 1].y;

			output += input[nMinusOne].x * input[0].y;

			for (var i = 0;
			     i < nMinusOne;
			     i++) output -= input[i + 1].x * input[i].y;

			output -= input[0].x * input[nMinusOne].y;

			return -output / 2.0f;
		}

	}

}