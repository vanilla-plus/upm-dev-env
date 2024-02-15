using System;

using UnityEngine;

namespace Vanilla.Geocodes
{

	public static class MojitenUtility
	{

		// MapPoint means Vector2 between the bounds of (canvas.renderingDisplaySize / 2) both axes. Remember lat/long ordering is opposite to Vector2 which puts x/Horizontal first!
		// MapNormal means Vector2 between -1 to 1 on both axes.
		// LatLong means Vector2 between -Mojiten2.LONGITUDE_MAX, Mojiten2.LONGITUDE_MAX, etc


		public static Vector2 ValidateMapPoint(this Vector2 input, Canvas canvas)
		{
			var canvasPixelSize = canvas.renderingDisplaySize;
			var w               = canvasPixelSize.x * 0.5f;
			var h               = canvasPixelSize.y * 0.5f;

			input.x = Math.Clamp(input.x, -w, w);

			input.y = Math.Clamp(input.y, -h, h);

			return input;
		}
		
//		public static Vector2 ValidateMapNormal(this Vector2 input) => input.normalized;

		public static Vector2 ValidateLatLong(this Vector2 input)
		{
			input.x = (float) Math.Clamp(input.x,
			                             Mojiten2.LONGITUDE_MIN,
			                             Mojiten2.LONGITUDE_MAX);
			
			input.y = (float) Math.Clamp(input.y,
			                             Mojiten2.LATITUDE_MIN,
			                             Mojiten2.LATITUDE_MAX);

			return input;
		}


		public static Vector2 MapPointToMapNormal(this Vector2 input,
		                                          Canvas canvas)
		{
			var canvasPixelSize = canvas.renderingDisplaySize;

			input.x = (input.x / canvasPixelSize.x) * 2.0f;
			input.y = (input.y / canvasPixelSize.y) * 2.0f;

			return input;
		}
		
		public static Vector2 MapNormalToLatLong(this Vector2 input)
		{
			// This assumes that LONGITUDE_MIN and LONGITUDE_MAX are just inverse of each-other.
			// If this weren't the case, you would need to check if input is negative and lerp against LONGITUDE_MIN separately.
			input.x = Mathf.LerpUnclamped(0,
			                              (float) Mojiten2.LONGITUDE_MAX,
			                              input.x);

			input.y = Mathf.LerpUnclamped(0,
			                              (float) Mojiten2.LATITUDE_MAX,
			                              input.y);

			return input;
		}
		
		public static Vector2 LatLongToMapNormal(this Vector2 input)
		{
			input.x /= (float)Mojiten2.LONGITUDE_MAX;
			input.y /= (float)Mojiten2.LATITUDE_MAX;

			return input;
		}


		public static Vector2 MapNormalToMapPoint(this Vector2 input, Canvas canvas)
		{
			var canvasPixelSize = canvas.renderingDisplaySize;

			input.x = Mathf.LerpUnclamped(0,
			                              canvasPixelSize.x * 0.5f,
			                              input.x);

			input.y = Mathf.LerpUnclamped(0,
			                              canvasPixelSize.y * 0.5f,
			                              input.y);

			return input;
		}


		public static Vector2 LatLongToMapPoint(this Vector2 input, Canvas canvas) => input.ValidateLatLong().LatLongToMapNormal().MapNormalToMapPoint(canvas);


		public static Vector2 MapPointToLatLong(this Vector2 input,
		                                        Canvas canvas) => input.MapPointToMapNormal(canvas).MapNormalToLatLong().ValidateLatLong();

	}

}