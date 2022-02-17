using UnityEngine;

using Vanilla.Easing;

namespace Vanilla.Layout
{

	public class LayoutItem3D : LayoutItem<Transform>
	{

		[SerializeField]
		public Normal expansion;

		[SerializeField]
		public KeyCode testKey = KeyCode.Alpha1;


		void Awake()
		{
			Selected.onTrue += () => expansion.Fill(conditional: Selected);

			Selected.onFalse += () => expansion.Drain(conditional: Selected,
			                                          targetCondition: false);

			var originalSize = _transform.localScale.x;

			expansion.OnChange += n => _transform.localScale = new Vector3(x: originalSize * n.InOutPower(power: 1.25f),
			                                                               y: originalSize,
			                                                               z: originalSize);

			expansion.Empty.onFalse += () => Dirty.State = true;
			expansion.Full.onFalse  += () => Dirty.State = true;

			expansion.Full.onTrue  += () => Dirty.State = false;
			expansion.Empty.onTrue += () => Dirty.State = false;

			expansion.OnChange?.Invoke(0.0f);
		}


		void Update()
		{
			if (Input.GetKeyDown(key: testKey)) Selected.Flip();
		}

	}

}