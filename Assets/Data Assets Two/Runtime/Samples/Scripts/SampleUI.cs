using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.DataAssets.Samples
{

	public class SampleUI : SampleBase
	{

		public Text boolText;
		public Text floatText;
		public Text intText;
		public Text vector3Text;
		public Text stringText;
		public Text eventText;
		public Text gameObjectText;

		protected override void OnEnable()
		{
			base.OnEnable();

			// Even if we make the values we're tracking Broadcast themselves automatically, we still want
			// behaviour like this. 
			
			BoolChanged(value: boolBinding.value);
			FloatChanged(value: floatBinding.value);
			IntChanged(value: intBinding.value);
			Vector3Changed(value: vector3Binding.value);
			StringChanged(value: stringBinding.value);
		}

		protected override void BoolChanged(bool value) => boolText.text = $"Bool: {value}";

		protected override void FloatChanged(float value) => floatText.text = $"Float: {value}";

		protected override void IntChanged(int value) => intText.text = $"Int: {value}";

		protected override void Vector3Changed(Vector3 value) => vector3Text.text = $"Vector3: {value}";

		protected override void StringChanged(string value) => stringText.text = $"String: {value}";

		protected override void EventInvoked()
		{
			eventText.text = "Event: ";
			
			switch (Random.Range(minInclusive: 0, maxExclusive: 4))
			{
				case 0:
					eventText.text += "Biff";
					break;

				case 1:
					eventText.text += "Bang";
					break;
				
				case 2:
					eventText.text += "Whack";
					break;
				
				case 3:
					eventText.text += "Zap";
					break;
			}
		}


		protected override void GameObjectAssigned(GameObject incoming) => 
			gameObjectText.text = $"GameObject: {gameObjectBinding.value.name}";

	}

}