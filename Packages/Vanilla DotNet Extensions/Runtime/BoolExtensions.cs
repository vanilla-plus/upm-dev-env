namespace Vanilla.DotNetExtensions
{

	public static class BoolExtensions
	{

		public static void Toggle(this ref bool input) => input = !input;

	}

}