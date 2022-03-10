using System;

using UnityEngine;

namespace Vanilla.JNode.Samples
{

	[Serializable]
	public class EarthEditor : JNodeEditor<Earth>
	{

		public SampleApp app;
		
		protected override void OnValidate()
		{
			base.OnValidate();
			
			if (Application.isPlaying && app != null)
			{
				app.earth.FromJson(json: stringOutput);
			}
		}

	}

}