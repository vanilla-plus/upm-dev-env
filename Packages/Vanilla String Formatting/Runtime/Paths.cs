using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vanilla.StringFormatting
{

	public static class Paths
	{

		/// <summary>
		///		Turns the string array into a single directory path using the correct slash for the platform.
		/// </summary>
		/// <param name="input">The list of sub-directories in the path</param>
		public static string AsDirectoryPath(this IEnumerable<string> input) =>
			input.Aggregate(seed: string.Empty,
			                func: (current,
			                       next) => current + next + Path.DirectorySeparatorChar);


		///  <summary>
		/// 		Turns the string array into a single directory path using a custom slash.
		///  </summary>
		///  <param name="input">The list of sub-directories in the path</param>
		///  <param name="slash">The slash to insert between each entry</param>
		public static string AsDirectoryPath(this IEnumerable<string> input,
		                                     char                     slash) =>
			input.Aggregate(seed: string.Empty,
			                func: (current,
			                       next) => current + next + slash);


		/// <summary>
		///		Turns the string array into a single directory path using the correct slash for the platform. The last entry will not have a slash added as it's assumed to be a file.
		/// </summary>
		/// <param name="input">The list of sub-directories leading to the file, including the file in the last index</param>
		public static string AsFilePath(this string[] input)
		{
			var output = new StringBuilder(64);

			for (var i = 0;
			     i < input.Length - 1;

			     i++)
			{
				output.Append(input[i]);
				output.Append(Path.DirectorySeparatorChar);
			}

			output.Append(input.Last());

			return output.ToString();
		}


		///  <summary>
		/// 		Turns the string array into a single directory path using a custom slash. The last entry will not have a slash added as it's assumed to be a file.
		///  </summary>
		///  <param name="input">The list of sub-directories leading to the file, including the file in the last index</param>
		///  <param name="slash">The slash to insert between each entry</param>
		public static string AsFilePath(this string[] input,
		                                char          slash)
		{
			var output = new StringBuilder(64);

			for (var i = 0;
			     i < input.Length - 1;

			     i++)
			{
				output.Append(input[i]);
				output.Append(slash);
			}

			output.Append(input.Last());

			return output.ToString();
		}

	}

}