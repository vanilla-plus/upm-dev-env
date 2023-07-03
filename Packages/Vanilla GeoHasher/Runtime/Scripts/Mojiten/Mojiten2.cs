using System;

using UnityEngine;

namespace Vanilla.Geocodes
{
	
	// Moji = 'glyph'
	// Ten = 'point on a map'
	
	// Mojiten :A
	
	public static class Mojiten2
	{

		private const string HIRAGANA = "あいうえおかがきぎくぐけげこごさざしじすずせぜそぞただちぢつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもやゆよらりるれろわをん";
		private const string KATAKANA = "アイウエオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモヤユヨラリルレロワヲン";

		private const string KANA_MISC = "ー。";

		private const string CHARACTER_SET = HIRAGANA + KATAKANA + KANA_MISC; // 144! (12x12)

		public const double LATITUDE_MIN = -80.0;
		public const double LATITUDE_MAX = 80.0;

		public const double LONGITUDE_MIN = -180.0;
		public const  double LONGITUDE_MAX = 180.0;

		public const int Latitude_Rows = 9; // latitude grid size / rows
		public const int Longitude_Columns = 16; // longitude grid size / columns

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

			var geocode = "";
			var row     = 0;
			var column  = 0;

			for (var i = 0;
			     i < precision;
			     i++)
			{
				var latStep = (maxLat - minLat) / Latitude_Rows;
				var lonStep = (maxLon - minLon) / Longitude_Columns;

				row    = (int) ((maxLat    - latitude) / latStep);
				column = (int) ((longitude - minLon)   / lonStep);

				row = Math.Clamp(row,
				                 0,
				                 Latitude_Rows - 1);

				column = Math.Clamp(column,
				                    0,
				                    Longitude_Columns - 1);

//				row    = Mathf.FloorToInt((float) ((maxLat    - latitude) / latStep));
//				column = Mathf.FloorToInt((float) ((longitude - minLon)   / lonStep));
				
//				row = Math.Clamp(row, 0, )
				
//				Debug.Log(row);
//				Debug.Log(column);

				var index = row * Longitude_Columns + column;

//				Debug.Log(index);

				geocode += CHARACTER_SET[index];

				maxLat -= row * latStep;
				minLat =  maxLat - latStep;
				minLon += column * lonStep;
				maxLon =  minLon + lonStep;
			}

			return (row, column, geocode);
		}


		public static(double, double) Decode(string geocode)
		{
			if (string.IsNullOrEmpty(geocode))
			{
				throw new ArgumentException("Geocode cannot be null or empty.",
				                            nameof(geocode));
			}

			double minLat = LATITUDE_MIN,
			       maxLat = LATITUDE_MAX,
			       minLon = LONGITUDE_MIN,
			       maxLon = LONGITUDE_MAX;

			foreach (var character in geocode)
			{
				var latStep = (maxLat - minLat) / Latitude_Rows;
				var lonStep = (maxLon - minLon) / Longitude_Columns;

				var index = CHARACTER_SET.IndexOf(character);

				if (index == -1)
				{
					throw new ArgumentException($"Invalid character '{character}' in geocode.",
					                            nameof(geocode));
				}

				var row    = index / Longitude_Columns;
				var column = index % Longitude_Columns;

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