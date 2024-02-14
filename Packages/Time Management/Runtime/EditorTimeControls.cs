using System;

using UnityEngine;

namespace Vanilla.Hourglass
{

	[Serializable]
	public class EditorTimeControls : MonoBehaviour
	{

		[SerializeField]
		public KeyCode slowMotionKey = KeyCode.F1;
		[SerializeField]
		public KeyCode fastForwardKey = KeyCode.F2;
		[SerializeField]
		public KeyCode testPause = KeyCode.F4;


		void Update()
		{
			#if UNITY_EDITOR
			
			if (Input.GetKeyDown(testPause)) HourGlass.TryPause();

			if (Input.GetKeyDown(slowMotionKey)) HourGlass.SetTimeScaleSlow();
			if (Input.GetKeyUp(slowMotionKey)) HourGlass.SetTimeScaleNormal();

			if (Input.GetKeyDown(fastForwardKey)) HourGlass.SetTimeScaleFast();
			if (Input.GetKeyUp(fastForwardKey)) HourGlass.SetTimeScaleNormal();
			#endif
		}

	}

}