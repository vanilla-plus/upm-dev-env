using System;
using System.Reflection;

using UnityEngine;

namespace Vanilla.MaterialDriver
{

	[Serializable]
	public class ColorDriver : PropertyDriver
	{

		[SerializeField]
		public Gradient[] gradients = Array.Empty<Gradient>();

		public override void Init(float normal) => Lerp(normal);

		public override void DeInit() { }


		public override void OnValidate(float normal)
		{
			#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				var c = SampleGradients(n: InvertNormal ?
					                           1.0f - normal :
					                           normal);

				// Warning - This is hacktown, USA.
				// Unity is like "cant call SetColorImp during serialization
				// so we don't, we call SetColor, without telling mummy.
				// And it works fine? So like shut up Unity?? Just kill the imp
				foreach (var m in TargetMaterials)
				{
					if (m == null) continue;

					// Get the SetColor method
					var setColorMethod = typeof(Material).GetMethod(name: "SetColor",
					                                                types: new[]
					                                                       {
						                                                       typeof(string),
						                                                       typeof(Color)
					                                                       });

					// Invoke the method with the property name and the new color
					setColorMethod?.Invoke(obj: m,
					                       parameters: new object[]
					                                   {
						                                   Name,
						                                   c
					                                   });
				}
			}
			#endif
		}


		public override void Lerp(float normal)
		{
			var c = SampleGradients(n: InvertNormal ? 1.0f - normal : normal);

			foreach (var m in TargetMaterials)
			{
				if (m != null)
					m.SetColor(nameID: PropertyId,
					           value: c);
			}
		}


		public override void Lerp(float outgoing,
		                          float incoming) => Lerp(normal: incoming);


		private Color SampleGradients(float n)
		{
			var gradientCount = gradients.Length;

			if (gradientCount == 0) return Color.white;
			
			var gradientBracket =
				(int) Mathf.Clamp(value: Mathf.Floor(f: n * gradientCount),
				                  min: 0,
				                  max: gradientCount - 1);

			var output = n * gradientCount - gradientBracket;

			return gradients[gradientBracket].Evaluate(time: output);
		}

	}

}