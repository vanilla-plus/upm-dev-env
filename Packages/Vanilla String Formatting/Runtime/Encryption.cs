using System.Linq;

namespace Vanilla.StringFormatting
{

	public static class Encryption
	{

		public static string X(this string input,
		                       int twist) => new(input.Select(c => (char)(c ^ twist)).ToArray());

	}

}