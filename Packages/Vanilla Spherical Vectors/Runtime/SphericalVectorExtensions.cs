using UnityEngine;

namespace Vanilla.SphericalVectors
{

	public static class SphericalVectorExtensions
	{


//		/// <summary>
//		///		Converts a 'spherical' Vector3 (longitude/latitude/altitude) to a 'cartesian' Vector3 (x/y/z)
//		/// </summary>
//		/// <returns>X = longitude, Y = latitude, Z = altitude</returns>
//		public static Vector3 ToCartesian(this Vector3 spherical) => Quaternion.Euler(x: -spherical.y,
//		                                                                              y: -spherical.x,
//		                                                                              z: 0.0f)
//		                                                           * new Vector3(x: 0,
//		                                                                         y: 0,
//		                                                                         z: spherical.z);
//
//
//		/// <summary>
//		///		Converts a 'cartesian' Vector3 (x/y/z) to a 'spherical' Vector3 (longitude/latitude/altitude)
//		/// </summary>
//		/// <returns>X = longitude, Y = latitude, Z = altitude</returns>
//		public static Vector3 ToSpherical(this Vector3 cartesian)
//		{
//			var altitude = cartesian.magnitude;
//
//			return new Vector3(x: Mathf.Rad2Deg
//			                    * -Mathf.Atan2(y: cartesian.x,
//			                                   x: cartesian.z),
//			                   y: Mathf.Rad2Deg
//			                    * Mathf.Atan2(y: cartesian.y,
//			                                  x: new Vector2(x: cartesian.x,
//			                                                 y: cartesian.z).magnitude),
//			                   z: altitude);
//		}
		
		/// <summary>
		///     Converts a 'spherical' Vector3 (longitude/latitude/altitude) to a 'cartesian' Vector3 (x/y/z)
		/// </summary>
		/// <returns>X = longitude, Y = latitude, Z = altitude</returns>
		public static Vector3 ToCartesian(this Vector3 spherical)
		{
			float latitude  = Mathf.Deg2Rad * (spherical.y);
			float longitude = Mathf.Deg2Rad * (-spherical.x);

			float sinLatitude  = Mathf.Sin(latitude);
			float cosLatitude  = Mathf.Cos(latitude);
			float sinLongitude = Mathf.Sin(longitude);
			float cosLongitude = Mathf.Cos(longitude);

			return new Vector3(spherical.z * cosLatitude * sinLongitude,
			                   spherical.z * sinLatitude,
			                   spherical.z * cosLatitude * cosLongitude);
		}

		/// <summary>
		///     Converts a 'cartesian' Vector3 (x/y/z) to a 'spherical' Vector3 (longitude/latitude/altitude)
		/// </summary>
		/// <returns>X = longitude, Y = latitude, Z = altitude</returns>
		public static Vector3 ToSpherical(this Vector3 cartesian)
		{
			float altitude     = cartesian.magnitude;
			float latitudeRad  = Mathf.Asin(cartesian.y / altitude);
			float longitudeRad = Mathf.Atan2(-cartesian.x, cartesian.z);

			return new Vector3(Mathf.Rad2Deg * longitudeRad,
			                   Mathf.Rad2Deg * latitudeRad,
			                   altitude);
		}

	}

}