using UnityEngine;

namespace Vanilla.UnityExtensions
{

	public static class QuaternionExtensions
	{
		
		// -------------------------------------------------------------------------------------------------------------------------- Comparisons //

		/// <summary>
		///     This function will return a quaternion equal to the difference between inputs a and b
		/// </summary>
		public static Quaternion GetQuaternionDifference(Quaternion a,
		                                                 Quaternion b) => a * Quaternion.Inverse(b);

	}

}