using UnityEngine;
using UnityEngine.UI;

using Vanilla.TypeMenu;

namespace Vanilla.Easing.Samples
{

	public class VanillaEasingPlot : MonoBehaviour
	{

		public RectTransform graph;

		public RectTransform rect;

		public Text valueText;
		public Text timeText;
		
		public float speed = 0.5f;
		
		[TypeMenu]
		[Only(typeof(IEasingSlot))]
		[SerializeReference]
		public IEasingSlot easingSlot;

		private void Update()
		{
			var t = Mathf.PingPong(t: Time.time * speed,
			                       length: 1.0f);

			var v = easingSlot.Ease(t);
			
			timeText.text  = t.ToString(format: "F");
			valueText.text = v.ToString(format: "F");

			var sizeDelta = graph.sizeDelta;

			rect.anchoredPosition = new Vector2(x: t * sizeDelta.x,
			                                    y: v * sizeDelta.y);
		}

	}

}