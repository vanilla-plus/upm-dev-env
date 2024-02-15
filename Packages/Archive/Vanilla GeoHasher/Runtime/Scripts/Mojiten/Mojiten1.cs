using System;

namespace Vanilla.Geocodes
{

	// Moji = 'glyph'
	// Ten = 'point on a map'
	
	// Mojiten :A
	
	public static class Mojiten1
	{

		private const string HIRAGANA = "あいうえおかがきぎくぐけげこごさざしじすずせぜそぞただちぢつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもやゆよらりるれろわをん";
		private const string KATAKANA = "アイウエオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモヤユヨラリルレロワヲン";

		private const string KANA_MISC = "ー。";

		private const string KANA = HIRAGANA + KATAKANA + KANA_MISC; // 144! (12x12)

		public const int GridSize = 12; // 12x12 grid

		public const double LATITUDE_MIN = -70.0;
		public const  double LATITUDE_MAX = 70.0;

		public const double LONGITUDE_MIN = -180.0;
		public const double LONGITUDE_MAX = 180.0;


		public static(int row, int column, string geocode) Encode(double latitude,
		                                                          double longitude,
		                                                          int precision)
		{
			latitude = Math.Clamp(latitude,
			                      LATITUDE_MIN,
			                      LATITUDE_MAX);
			
			double minLat = LATITUDE_MIN,
			       maxLat = LATITUDE_MAX,
			       minLon = LONGITUDE_MIN,
			       maxLon = LONGITUDE_MAX;

			var row    = 0;
			var column = 0;
			
			var geocode = "";

			for (var i = 0;
			     i < precision;
			     i++)
			{
//                var latStep = (maxLat - minLat) / GridSizeLat;
//                var lonStep = (maxLon - minLon) / GridSizeLon;
				var latStep = (maxLat - minLat) / GridSize;
				var lonStep = (maxLon - minLon) / GridSize;

				row    = (int) ((maxLat    - latitude) / latStep);
				column = (int) ((longitude - minLon)   / lonStep);

//                var index = row * GridSizeLon + column;
				var index = row * GridSize + column;
				geocode += KANA[index];

				maxLat -= row * latStep;
				minLat =  maxLat - latStep;
				minLon += column * lonStep;
				maxLon =  minLon + lonStep;
			}

			return (row, column, geocode);
		}


		public static(double, double) Decode(string geocode)
		{
			double minLat = LATITUDE_MIN,
			       maxLat = LATITUDE_MAX,
			       minLon = LONGITUDE_MIN,
			       maxLon = LONGITUDE_MAX;

			foreach (var character in geocode)
			{
//                var latStep = (maxLat - minLat) / GridSizeLat;
//                var lonStep = (maxLon - minLon) / GridSizeLon;
				var latStep = (maxLat - minLat) / GridSize;
				var lonStep = (maxLon - minLon) / GridSize;

				var index = KANA.IndexOf(character);

//                var row    = index / GridSizeLon;
//                var column = index % GridSizeLon;
				var row    = index / GridSize;
				var column = index % GridSize;

				maxLat -= row * latStep;
				minLat =  maxLat - latStep;
				minLon += column * lonStep;
				maxLon =  minLon + lonStep;
			}

			var latitude  = (minLat + maxLat) / 2.0;
			var longitude = (minLon + maxLon) / 2.0;

			return (latitude, longitude);
		}


	}

}