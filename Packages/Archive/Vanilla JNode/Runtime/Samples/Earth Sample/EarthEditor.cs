using System;

using UnityEngine;

namespace Vanilla.JNode.Samples
{

	[Serializable]
	public class EarthEditor : JNodeEditor<Earth>
	{

		public SampleApp app;

		public KeyCode key;

		protected override string GetName() => data.Name;
		
		void Update()
		{
			if (Input.GetKeyDown(key)) app.earth.FromJson(stringOutput);
		}

	}

}