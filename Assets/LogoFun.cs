using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla
{
	public class LogoFun : MonoBehaviour
	{

		[SerializeField]
		public float sinSpeed = 1.0f;

		[SerializeField]
		public float defaultOffset = -0.1f;

		[SerializeField]
		public float localZMin = 0.0f;

		[SerializeField]
		public float localZMax = -4.0f;

		[SerializeField]
		public RectTransform[] letterRects = new RectTransform[8];

		[SerializeField]
		public float[] sinOffsets = new float[8];


		void Update()
		{
			for (var i = 0;
			     i < letterRects.Length;
			     i++)
			{
//                var sin = (1.0f + Mathf.Sin(Time.time * sinSpeed + defaultOffset * i)) * 0.5f;
				var sin = Mathf.Sin(Time.time * sinSpeed + defaultOffset * i);

				sinOffsets[i] = sin;
                
//                if (i == 0) Debug.Log(sin);
                
//                sinOffsets[i] = sin + (defaultOffset * i);

				var p = letterRects[i].localPosition;

				p.z = Mathf.Lerp(localZMin,
				                 localZMax,
				                 sin);

				letterRects[i].localPosition = p;
			}
		}

	}
}