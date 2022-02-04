using UnityEngine;

namespace Vanilla.Geodetics
{

	public static class GeodeticExtensions
	{


		/// <summary>
		///		Converts a 'geodetic' Vector3 (longitude/latitude/radius) to a 'cartesian' Vector3 (x/y/z)
		/// </summary>
		/// <returns>X = longitude, Y = latitude, Z = radius</returns>
		public static Vector3 GeodeticToCartesian(this Vector3 geo) => Quaternion.Euler(x: -geo.y,
		                                                                                y: -geo.x,
		                                                                                z: 0.0f) *
		                                                               new Vector3(x: 0,
		                                                                           y: 0,
		                                                                           z: geo.z);


		/// <summary>
		///		Converts a 'cartesian' Vector3 (x/y/z) to a 'geodetic' Vector3 (longitude/latitude/radius)
		/// </summary>
		/// <returns>X = longitude, Y = latitude, Z = radius</returns>
		public static Vector3 CartesianToGeodetic(this Vector3 cartesian)
		{
			var radius = cartesian.magnitude;

			return new Vector3(x: Mathf.Rad2Deg *
			                      -Mathf.Atan2(y: cartesian.x,
			                                   x: cartesian.z),
			                   y: Mathf.Rad2Deg *
			                      Mathf.Atan2(y: cartesian.y,
			                                  x: new Vector2(x: cartesian.x,
			                                                 y: cartesian.z).magnitude),
			                   z: radius);
		}

	}

}