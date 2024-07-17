using System;
using System.Linq;
using System.Text;

namespace Vanilla.DotNetExtensions
{

	public static class StringExtensions
	{

		#region As Time
		
		public static string AsTimeFromHours(this int hours, StringBuilder sb)               => AsTimeFromMilliseconds(milliseconds: hours   * 360000, sb);
		public static string AsTimeFromMinutes(this int minutes, StringBuilder sb)           => AsTimeFromMilliseconds(milliseconds: minutes * 6000,   sb);
		public static string AsTimeFromSeconds(this int seconds, StringBuilder sb)           => AsTimeFromMilliseconds(milliseconds: seconds * 1000,   sb);
		public static string AsTimeFromMilliseconds(this int milliseconds, StringBuilder sb) => ((long) milliseconds).AsTimeFromMilliseconds(sb);


		public static string AsTimeFromMilliseconds(this long input,
		                                            StringBuilder sb)
		{
			const double DaysPerMonth  = 30.44;
			const double DaysPerYear   = 365.25;

			const string YearSingular        = " year";
			const string YearPlural          = " years";
			const string MonthSingular       = " month";
			const string MonthPlural         = " months";
			const string DaySingular         = " day";
			const string DayPlural           = " days";
			const string HourSingular        = " hour";
			const string HourPlural          = " hours";
			const string MinuteSingular      = " minute";
			const string MinutePlural        = " minutes";
			const string SecondSingular      = " second";
			const string SecondPlural        = " seconds";
			const string MillisecondSingular = " millisecond";
			const string MillisecondPlural   = " milliseconds";
			const string Appendage           = ", ";

			var timeSpan = TimeSpan.FromMilliseconds(input);

			sb ??= new StringBuilder();

			sb.Clear();

			var years = (int) (timeSpan.TotalDays / DaysPerYear);

			if (years > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(years);

				sb.Append(years > 1 ?
					          YearPlural :
					          YearSingular);
			}

			var months = (int) (timeSpan.TotalDays % DaysPerYear / DaysPerMonth);

			if (months > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(months);

				sb.Append(months > 1 ?
					          MonthPlural :
					          MonthSingular);
			}

			var days = (int) (timeSpan.TotalDays % DaysPerMonth);

			if (days > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(days);

				sb.Append(days > 1 ?
					          DayPlural :
					          DaySingular);
			}

			var hours = timeSpan.Hours;

			if (hours > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(hours);

				sb.Append(hours > 1 ?
					          HourPlural :
					          HourSingular);
			}

			var minutes = timeSpan.Minutes;

			if (minutes > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(minutes);

				sb.Append(minutes > 1 ?
					          MinutePlural :
					          MinuteSingular);
			}

			var seconds = timeSpan.Seconds;

			if (seconds > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(seconds);

				sb.Append(seconds > 1 ?
					          SecondPlural :
					          SecondSingular);
			}

			var milliseconds = timeSpan.Milliseconds;

			if (milliseconds > 0)
			{
				if (sb.Length > 0) sb.Append(Appendage);

				sb.Append(milliseconds);

				sb.Append(milliseconds > 1 ?
					          MillisecondPlural :
					          MillisecondSingular);
			}

			return sb.ToString();
		}

		#endregion

		#region As Frequency



		private const long   KHz             = 1000;
		private const long   MHz             = KHz * 1000;
		private const long   GHz             = MHz * 1000;
		private const long   THz             = GHz * 1000;
		private const long   PHz             = THz * 1000;
		private const long   EHz             = PHz * 1000;
		private const string HertzFormatSpecifier = "+0.##;-0.##;0";
		
		public static string AsFrequency<T>(this T hertz) where T : IConvertible
		{
			var longHertz = Convert.ToInt64(hertz);
			var absHertz  = Math.Abs(longHertz);

			return absHertz switch
			       {
				       < KHz => $"{longHertz.ToString(HertzFormatSpecifier)}hz",
				       < MHz => $"{(decimal) absHertz / KHz:HertzFormatSpecifier}kHz",
				       < GHz => $"{(decimal) absHertz / MHz:HertzFormatSpecifier}MHz",
				       < THz => $"{(decimal) absHertz / GHz:HertzFormatSpecifier}GHz",
				       < PHz => $"{(decimal) absHertz / THz:HertzFormatSpecifier}THz",
				       < EHz => $"{(decimal) absHertz / PHz:HertzFormatSpecifier}PHz",
				       _ => longHertz >= EHz ?
					            $"{(decimal) absHertz / EHz:HertzFormatSpecifier}EHz" :
					            "a whole bunch"
			       };
		}
		
		#endregion

		#region As Data

		private const long   Kb              = 1  * 1024;
		private const long   Mb              = Kb * 1024;
		private const long   Gb              = Mb * 1024;
		private const long   Tb              = Gb * 1024;
		private const long   Pb              = Tb * 1024;
		private const long   Eb              = Pb * 1024;
		private const string DataFormatSpecifier = "0.##";

		public static string AsData<T>(this T bytes) where T : unmanaged, IConvertible
		{
			var decimalBytes = Convert.ToDecimal(bytes);

			return decimalBytes switch
			       {
				       < Kb => $"{decimalBytes.ToString(DataFormatSpecifier)}b",
				       < Mb => $"{(decimalBytes / Kb).ToString(DataFormatSpecifier)}Kb",
				       < Gb => $"{(decimalBytes / Mb).ToString(DataFormatSpecifier)}Mb",
				       < Tb => $"{(decimalBytes / Gb).ToString(DataFormatSpecifier)}Gb",
				       < Pb => $"{(decimalBytes / Tb).ToString(DataFormatSpecifier)}Tb",
				       < Eb => $"{(decimalBytes / Pb).ToString(DataFormatSpecifier)}Pb",
				       _    => $"{(decimalBytes / Eb).ToString(DataFormatSpecifier)}Eb"
			       };
		}


		#endregion

		#region Encryption



		public static string Twist(this string input,
		                           int twist) => new(input.Select(c => (char) (c ^ twist)).ToArray());



		#endregion

	}

}