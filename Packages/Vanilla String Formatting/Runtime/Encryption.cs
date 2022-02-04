using System.Linq;

namespace Vanilla.StringFormatting
{

	public static class Encryption
	{

		public static string Xor(this string input,
		                         int         key) =>
			new string(input.Select(c => (char) (c ^ key)).ToArray());

	}

}