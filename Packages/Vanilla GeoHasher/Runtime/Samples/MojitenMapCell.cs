using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Geocodes
{

	public class MojitenMapCell : MonoBehaviour
	{

		public int   depth;
		public int   column;
		public int   row;
		
		public float WidthMin;
		public float WidthMax;
		public float HeightMin;
		public float HeightMax;

		public float LatitudeMin;
		public float LatitudeMax;

		public float LongitudeMin;
		public float LongitudeMax;

//		public float LatitudeMin => (float) (HeightMin * Mojiten2.LATITUDE_MIN);
//		public float LatitudeMax => (float) (HeightMax * Mojiten2.LATITUDE_MAX);
//
//		public float LongitudeMin => (float) (WidthMin * Mojiten2.LONGITUDE_MIN);
//		public float LongitudeMax => (float) (WidthMax * Mojiten2.LONGITUDE_MAX);
		
	}

}