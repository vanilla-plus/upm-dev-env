using System;

using UnityEngine;

namespace Vanilla.Easing
{

	/// <summary>
	///     This is a C# implementation of Michealangelo007s incredible easing formula Javascript library.
	///
	///     That can be found at the following address:
	///
	///     https://github.com/Michaelangel007/easing
	///
	///     That library is itself based on Robert Penners infamous easing library from 2001, variations of
	///     which can be found here:
	///
	///     http://robertpenner.com/easing/
	/// </summary>

	public static class Easing
	{

		// ---------------------------------------------------------------------------------------------------------------------------- Constants //

		#region Constants

		private const double K      = 1.70158;
		private const float  L0     = 7.5625f;
		private const double N      = 2.5949095;
		private const float  HalfPi = 1.57079637050629f;

		private const float A = 0.363636f;
		private const float B = 0.727272f;
		private const float C = 0.909090f;

		private const float X = 0.545454f;
		private const float Y = 0.818181f;
		private const float Z = 0.954545f;

		#endregion

		// ---------------------------------------------------------------------------------------------------------------------------------- Sin //

		#region Sin

		public static float InSine(this float t) => Mathf.Sin(HalfPi * (t - 1.0f)) + 1f;

		public static float OutSine(this float t) => Mathf.Sin(t * HalfPi);

		public static float InOutSine(this float t) => (Mathf.Sin(Mathf.PI * (t - 0.5f)) + 1.0f) * 0.5f;

		#endregion
		
		// ---------------------------------------------------------------------------------------------------------------------------------- Pow //

		#region Power

		public static float InPower(this float t,
		                            float power) => Mathf.Pow(f: t,
		                                                      p: power);


		public static float OutPower(this float t,
		                             float power) => 1.0f -
		                                             Mathf.Pow(f: 1.0f - t,
		                                                       p: power);


		public static float InOutPower(this float t,
		                               float power) => t < 0.5f ?
			                                               Mathf.Pow(f: t * 2.0f,
			                                                         p: power) *
			                                               0.5f :
			                                               1.0f -
			                                               Mathf.Pow(f: 2.0f - t * 2.0f,
			                                                         p: power) *
			                                               0.5f;

		#endregion

		// --------------------------------------------------------------------------------------------------------------------------------- Circ //

		#region Circle

		/// <summary>
		///     Ease in with the curvature of a perfect circle.
		/// </summary>
		public static float InCircle(this float t) => 1 - Mathf.Sqrt(f: 1.0f - t * t);


		/// <summary>
		///     Ease out with the curvature of a perfect circle.
		/// </summary>
		public static float OutCircle(this float t)
		{
			var m = t - 1.0f;

			return Mathf.Sqrt(f: 1.0f - m * m);
		}


		/// <summary>
		///     Ease in and then out with the curvature of a perfect circle.
		/// </summary>
		public static float InOutCircle(this float t)
		{
			var m = t - 1.0f;

			var p = t * 2.0f;

			return p < 1.0f ?
				       (1.0f - Mathf.Sqrt(f: 1.0f - p    * p))            * 0.5f :
				       (Mathf.Sqrt(f: 1.0f        - 4.0f * m * m) + 1.0f) * 0.5f;
		}

		#endregion

		// --------------------------------------------------------------------------------------------------------------------------------- Back //

		#region Back

		/// <summary>
		///     Ease in by dipping underneath the min value deliberately by 10%.
		/// </summary>
		public static float InBack(this float t) => (float)(t * t * (t * (K + 1.0f) - K));


		/// <summary>
		///     Ease out by overshooting our max value deliberately by 10%.
		/// </summary>
		public static float OutBack(this float t)
		{
			var m = t - 1.0f;

			return (float)(1.0f + m * m * (m * (K + 1.0f) + K));
		}


		/// <summary>
		///     Ease in and out by dipping under and then overshooting our min and max values respectively by 10%.
		/// </summary>
		public static float InOutBack(this float t)
		{
			var m = t - 1f;

			var p = t * 2f;

			return (float)(t < 0.5f ?
				               t * p * (p * (N + 1f) - N) :
				               1.0f + 2.0f * m * m * (2f * m * (N + 1f) + N));
		}

		#endregion

		// ------------------------------------------------------------------------------------------------------------------------------- Bounce //

		#region Bounce



		/// <summary>
		///     Ease in with a bounce-like animation.
		/// </summary>
		public static float InBounce(this float t, int numberOfBounces) => 1f - OutBounce(t: t, numberOfBounces);


		/// <summary>
		///     Ease out with a bounce-like animation.
		/// </summary>
//		public static float OutBounce(this float t)
//		{
//			switch (t)
//			{
//				case < A: return L0 * t * t;
//				case < B: return L0 * (t -= X) * t + 0.75f;
//				case < C: return L0 * (t -= Y) * t + 0.9375f;
//				default:  return L0 * (t -= Z) * t + 0.984375f;
//			}
//		}
		
		public static float OutBounce(this float t, int bounces = 5)
		{
			if (bounces < 2) bounces = 2;
			if (bounces > 8) bounces = 8;

			float totalDuration  = 1.0f;
			float bounceDuration = totalDuration / bounces;

			// Adjust the bounceHeight formula to make the first bounce reach the specified amplitude
//			float bounceHeight = amplitude / (1.0f - Mathf.Pow(0.5f, bounces - 1));
			float bounceHeight = 1.0f - Mathf.Pow(0.5f, bounces - 1);

			int currentBounce = Mathf.FloorToInt(t / bounceDuration);
			float timeInBounce  = (t - currentBounce * bounceDuration) / bounceDuration;

			float currentBounceHeight = bounceHeight * Mathf.Pow(0.5f, currentBounce);

			// Use a parabolic function to calculate the vertical position of the bounce
			float bouncePosition = currentBounceHeight * (1.0f - Mathf.Pow(2 * timeInBounce - 1, 2));

			float maxBounceHeight          = bounceHeight;
			float normalizedBouncePosition = bouncePosition / maxBounceHeight;

			return Math.Abs(t - 1) < Mathf.Epsilon ? 1 : normalizedBouncePosition;
		}





		/// <summary>
		///     Ease in and out with a bounce-like animation.
		/// </summary>
		public static float InOutBounce(this float t, int numberOfBounces)
		{
			var p = t * 2f;

			return p < 1f ?
				       0.5f - 0.5f * OutBounce(t: 1f - p,  numberOfBounces) :
				       0.5f + 0.5f * OutBounce(t: p  - 1f, numberOfBounces);
		}



		#endregion

		// ------------------------------------------------------------------------------------------------------------------------------ Elastic //

		#region Elastic



		/// <summary>
		///     Ease in with an elastic wobble.
		/// </summary>
		public static float InElastic(this float t)
		{
			var m = t - 1;

			return -Mathf.Pow(f: 2f,
			                  p: 10f * m) *
			       Mathf.Sin(f: (m * 40f - 3f) * Mathf.PI / 6f);
		}


		/// <summary>
		///     Ease out with an elastic wobble.
		/// </summary>
		public static float OutElastic(this float t) => 1 +
		                                                Mathf.Pow(f: 2f,
		                                                          p: 10f * -t) *
		                                                Mathf.Sin(f: (-t * 40f - 3f) * Mathf.PI / 6f);




		/// <summary>
		///     Ease in and out with an elastic wobble.
		/// </summary>
		public static float InOutElastic(this float t)
		{
			var s = 2f             * t - 1f;         // remap: [0.0, 0.5] -> [-1.0, 0.0]
			var l = (80f * s - 9f) * Mathf.PI / 18f; // and    [0.5, 1.0] -> [0.0, 1.0]

			return s < 0f ?
				       -0.5f *
				       Mathf.Pow(f: 2f,
				                 p: 10f * s) *
				       Mathf.Sin(f: l) :
				       1f +
				       0.5f *
				       Mathf.Pow(f: 2f,
				                 p: -10f * s) *
				       Mathf.Sin(f: l);
		}



		#endregion

	}

}