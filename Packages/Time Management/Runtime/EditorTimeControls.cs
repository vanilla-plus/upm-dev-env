using System;

using UnityEngine;

namespace Vanilla.TimeManagement
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
			
			if (Input.GetKeyDown(testPause)) Hourglass.TryPause();

			if (Input.GetKeyDown(slowMotionKey)) Hourglass.SetTimeScaleSlow();
			if (Input.GetKeyUp(slowMotionKey)) Hourglass.SetTimeScaleNormal();

			if (Input.GetKeyDown(fastForwardKey)) Hourglass.SetTimeScaleFast();
			if (Input.GetKeyUp(fastForwardKey)) Hourglass.SetTimeScaleNormal();
			#endif
		}

	}

}