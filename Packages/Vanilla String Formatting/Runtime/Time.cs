using System;
using System.Text;

namespace Vanilla.StringFormatting
{


	public static class Time
	{

		public static string AsTimeFromHours(this int hours,
		                                     bool     verbose = true) =>
			AsTimeFromMilliseconds(milliseconds: hours * 360000,
			                       verbose: verbose);


		public static string AsTimeFromMinutes(this int minutes,
		                                       bool     verbose = true) =>
			AsTimeFromMilliseconds(milliseconds: minutes * 6000,
			                       verbose: verbose);


		public static string AsTimeFromSeconds(this int seconds,
		                                       bool     verbose = true) =>
			AsTimeFromMilliseconds(milliseconds: seconds * 1000,
			                       verbose: verbose);


		public static string AsTimeFromMilliseconds(this int milliseconds,
		                                            bool     verbose = true) =>
			((long) milliseconds).AsTimeFromMilliseconds(verbose: verbose);


		public static string AsTimeFromMilliseconds(this long milliseconds,
		                                            bool      verbose = true)
		{
			if (milliseconds == 0) return "0";

			if (verbose)
			{
				// If we're only talking about milliseconds, spit out a basic string early on.
				if (milliseconds < 1000 &&
				    milliseconds > -1000)
					return milliseconds switch
					       {
						       1  => "1 millisecond",
						       -1 => "-1 millisecond",
						       _  => $"{milliseconds} milliseconds"
					       };
			}
			else
			{
				// If we're only talking about milliseconds, spit out a basic string early on.
				if (milliseconds < 1000 &&
				    milliseconds > -1000) return $"{milliseconds}ms";
			}

			// Oh wise guy eh...

			var sb = new StringBuilder(capacity: 64);

			if (milliseconds < 0)
			{
				sb.Append(value: "-");

				milliseconds = -milliseconds;
			}

			var t = TimeSpan.FromMilliseconds(value: milliseconds);

			if (verbose)
			{
				if (t.Days > 0)
					sb.Append(value: t.Days > 1 ?
						                 $"{t.Days} days, " :
						                 "1 day, ");

				if (t.Hours > 0)
					sb.Append(value: t.Hours > 1 ?
						                 $"{t.Hours} hours, " :
						                 "1 hour, ");

				if (t.Minutes > 0)
					sb.Append(value: t.Minutes > 1 ?
						                 $"{t.Minutes} minutes, " :
						                 "1 minute, ");

				if (t.Seconds > 0)
					sb.Append(value: t.Seconds > 1 ?
						                 $"{t.Seconds} seconds, " :
						                 "1 second, ");

				sb.Remove(startIndex: sb.Length - 2,
				          length: 2);
			}
			else
			{
				if (t.Days    > 0) sb.Append(value: $"{t.Days}d ");
				if (t.Hours   > 0) sb.Append(value: $"{t.Hours}h ");
				if (t.Minutes > 0) sb.Append(value: $"{t.Minutes}m ");
				if (t.Seconds > 0) sb.Append(value: $"{t.Seconds}s ");

				sb.Remove(startIndex: sb.Length - 1,
				          length: 1);
			}

			return sb.ToString();
		}


	}

}