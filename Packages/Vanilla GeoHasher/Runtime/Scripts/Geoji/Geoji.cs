using System;
using System.Text;

using UnityEngine;

namespace Vanilla.Geocodes
{

	// EmoGeo	?
	// Emogeo	?
	// Emojio	?
	// Geoji	?

	[Serializable]
	public class Geoji : GeoCode,
	                      ISerializationCallbackReceiver
	{

		public const char NORTH_WEST =	'↖';	public const char NORTH =	'↑';	public const char NORTH_EAST =	'↗';

		public const char WEST =		'←';										public const char EAST =		'→';

		public const char SOUTH_WEST =	'↙';	public const char SOUTH =	'↓';	public const char SOUTH_EAST =	'↘';
		
		protected override string Encode(double latitude,
		                                 double longitude) => Encode(latitude,longitude, Default_Precision);


		protected override string Encode(double latitude,
		                                 double longitude,
		                                 int precision)
		{
			if (latitude is < -90 or > 90 || longitude is < -180 or > 180) throw new ArgumentOutOfRangeException("Invalid latitude or longitude");

			double minLat = -90,
			       maxLat = 90;

			double minLon = -180,
			       maxLon = 180;

			var geocode = new StringBuilder();

			// Determine the initial hemisphere
			if (longitude < 0)
			{
				geocode.Append(WEST);
				maxLon = 0;
			}
			else
			{
				geocode.Append(EAST);
				minLon = 0;
			}

			for (var i = 0;
			     i < precision;
			     i++)
			{
				var midLat = (minLat + maxLat) / 2;
				var midLon = (minLon + maxLon) / 2;

				if (latitude >= midLat)
				{
					if (longitude >= midLon)
					{
						geocode.Append(NORTH_EAST);
						minLat = midLat;
						minLon = midLon;
					}
					else
					{
						geocode.Append(NORTH_WEST);
						minLat = midLat;
						maxLon = midLon;
					}
				}
				else
				{
					if (longitude >= midLon)
					{
						geocode.Append(SOUTH_EAST);
						maxLat = midLat;
						minLon = midLon;
					}
					else
					{
						geocode.Append(SOUTH_WEST);
						maxLat = midLat;
						maxLon = midLon;
					}
				}
			}

			return geocode.ToString();

		}


		protected override(double latitude, double longitude) Decode(string hash)
		{
			double minLat = -90,  maxLat = 90;
			double minLon = -180, maxLon = 180;

			foreach (var c in hash)
			{
				switch (c)
				{
					case WEST:
						maxLon = (minLon + maxLon) / 2;
						break;
					case EAST:
						minLon = (minLon + maxLon) / 2;
						break;
					case NORTH_WEST:
						minLat = (minLat + maxLat) / 2;
						maxLon = (minLon + maxLon) / 2;
						break;
					case NORTH_EAST:
						minLat = (minLat + maxLat) / 2;
						minLon = (minLon + maxLon) / 2;
						break;
					case SOUTH_WEST:
						maxLat = (minLat + maxLat) / 2;
						maxLon = (minLon + maxLon) / 2;
						break;
					case SOUTH_EAST:
						maxLat = (minLat + maxLat) / 2;
						minLon = (minLon + maxLon) / 2;
						break;
				}
			}
			return ((minLat + maxLat) / 2, (minLon + maxLon) / 2);
		}


		public override int Default_Precision => 6;

		public override int Min_Precision => 1;

		public override int Max_Precision => 20;


		public Geoji(double latitude,
		              double longitude) : base(latitude,
		                                       longitude) { }


		public Geoji(double latitude,
		              double longitude,
		              int precision) : base(latitude,
		                                    longitude,
		                                    precision) { }


		public Geoji(string hash) : base(hash) { }

		public void OnBeforeSerialize() { }

//		public void OnAfterDeserialize() { }

//		public void OnAfterDeserialize() => _hash = Encode(_latitude, _longitude, _precision);
		public void OnAfterDeserialize() => Hash = _hash;



	}

}