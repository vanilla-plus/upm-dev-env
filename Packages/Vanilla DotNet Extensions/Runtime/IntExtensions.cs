namespace Vanilla.DotNetExtensions
{

	public static class IntExtensions
	{

		// --------------------------------------------------------------------------------------------------------------------------- Validation //

		public static bool IsPositive(this int input) => input > 0;

		public static bool IsPositiveOrZero(this int input) => input >= 0;
		
		public static bool IsNegative(this int input) => input < 0;

		public static bool IsNegativeOrZero(this int input) => input <= 0;

		/// <summary>
		///     Returns true if the input value is odd. However, this function is currently untested. It also only works
		///     for an int, so consider rounding off if you need to check a float.
		/// </summary>
		public static bool IsOdd(this int input) => ( input & 1 ) == 1;


		/// <summary>
		///     Returns true if the input value is even. However, this function is currently untested. It also only
		///     works for an int, so consider rounding off if you need to check a float.
		/// </summary>
		public static bool IsEven(this int input) => ( input & 1 ) == 0;
		
	}

}