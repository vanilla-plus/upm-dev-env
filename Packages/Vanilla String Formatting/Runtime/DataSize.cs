namespace Vanilla.StringFormatting
{

	public static class DataSize
	{

		public const long Kb = 1  * 1024;
		public const long Mb = Kb * 1024;
		public const long Gb = Mb * 1024;
		public const long Tb = Gb * 1024;
		public const long Pb = Tb * 1024;
		public const long Eb = Pb * 1024;


		public static string AsDataSize(this long bytes) => bytes switch
		                                                    {
			                                                    < Kb => $"{bytes:0.##}b",
			                                                    < Mb => $"{(double)bytes / Kb:0.##}Kb",
			                                                    < Gb => $"{(double)bytes / Mb:0.##}Mb",
			                                                    < Tb => $"{(double)bytes / Gb:0.##}Gb",
			                                                    < Pb => $"{(double)bytes / Tb:0.##}Tb",
			                                                    < Eb => $"{(double)bytes / Pb:0.##}Pb",
			                                                    _    => $"{(double)bytes / Eb:0.##}Eb"
		                                                    };


		public static string AsDataSize(this ulong bytes) => bytes switch
		                                                     {
			                                                     < Kb => $"{bytes:0.##}b",
			                                                     < Mb => $"{(double)bytes / Kb:0.##}Kb",
			                                                     < Gb => $"{(double)bytes / Mb:0.##}Mb",
			                                                     < Tb => $"{(double)bytes / Gb:0.##}Gb",
			                                                     < Pb => $"{(double)bytes / Tb:0.##}Tb",
			                                                     < Eb => $"{(double)bytes / Pb:0.##}Pb",
			                                                     _    => $"{(double)bytes / Eb:0.##}Eb"
		                                                     };

	}

}