using System;

using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.Easing.Samples
{

	public class VanillaEasingPlot : MonoBehaviour
	{

		public RectTransform graph;

		public RectTransform rect;

		public Text valueText;
		public Text timeText;
		
		public float speed = 0.5f;
		
		public UnityEasing easing;

		public float floatPower = 2.0f;

		private void Update()
		{
			var t = Mathf.PingPong(t: Time.time * speed,
			                       length: 1.0f);

			var v = Ease(t: t);

			timeText.text  = t.ToString(format: "F");
			valueText.text = v.ToString(format: "F");

			var sizeDelta = graph.sizeDelta;

			rect.anchoredPosition = new Vector2(x: t * sizeDelta.x,
			                                    y: v * sizeDelta.y);
		}


		private float Ease(float t) => easing switch
		                               {
			                               UnityEasing.InSine       => t.InSine(),
			                               UnityEasing.OutSine      => t.OutSine(),
			                               UnityEasing.InOutSine    => t.InOutSine(),
			                               UnityEasing.InPower      => t.InPower(power: floatPower),
			                               UnityEasing.OutPower     => t.OutPower(power: floatPower),
			                               UnityEasing.InOutPower   => t.InOutPower(power: floatPower),
			                               UnityEasing.InBounce     => t.InBounce(),
			                               UnityEasing.OutBounce    => t.OutBounce(),
			                               UnityEasing.InOutBounce  => t.InOutBounce(),
			                               UnityEasing.InElastic    => t.InElastic(),
			                               UnityEasing.OutElastic   => t.OutElastic(),
			                               UnityEasing.InOutElastic => t.InOutElastic(),
			                               UnityEasing.InBack       => t.InBack(),
			                               UnityEasing.OutBack      => t.OutBack(),
			                               UnityEasing.InOutBack    => t.InOutBack(),
			                               UnityEasing.InCirc       => t.InCircle(),
			                               UnityEasing.OutCirc      => t.OutCircle(),
			                               UnityEasing.InOutCirc    => t.InOutCircle(),
			                               _                        => throw new ArgumentOutOfRangeException()
		                               };


		public enum UnityEasing
		{

			InSine,
			OutSine,
			InOutSine,
			InPower,
			OutPower,
			InOutPower,
			InBounce,
			OutBounce,
			InOutBounce,
			InElastic,
			OutElastic,
			InOutElastic,
			InBack,
			OutBack,
			InOutBack,
			InCirc,
			OutCirc,
			InOutCirc

		}

	}

}