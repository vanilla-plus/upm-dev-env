namespace Vanilla.StringFormatting
{

	public static class Frequency
	{

		public const long KHz = 1000;
		public const long MHz = KHz * 1000;
		public const long GHz = MHz * 1000;
		public const long THz = GHz * 1000;
		public const long PHz = THz * 1000;
		public const long EHz = PHz * 1000;


		public static string AsFrequency(this long hertz)
		{
			var negative = hertz < 0;

			if (negative) hertz = -hertz;

			if (hertz < KHz) return $"{(negative ? "-" : string.Empty)}{hertz:0.##}hz";

			if (hertz >= KHz &&
			    hertz < MHz) return $"{(negative ? "-" : string.Empty)}{(double) hertz / KHz:0.##}kHz";

			if (hertz >= MHz &&
			    hertz < GHz) return $"{(negative ? "-" : string.Empty)}{(double) hertz / MHz:0.##}MHz";

			if (hertz >= GHz &&
			    hertz < THz) return $"{(negative ? "-" : string.Empty)}{(double) hertz / GHz:0.##}GHz";

			if (hertz >= THz &&
			    hertz < PHz) return $"{(negative ? "-" : string.Empty)}{(double) hertz / THz:0.##}THz";

			if (hertz >= PHz &&
			    hertz < EHz) return $"{(negative ? "-" : string.Empty)}{(double) hertz / PHz:0.##}PHz";

			return hertz >= EHz ? $"{(negative ? "-" : string.Empty)}{(double) hertz / EHz:0.##}EHz" : "a whole bunch";
		}

	}

	
}