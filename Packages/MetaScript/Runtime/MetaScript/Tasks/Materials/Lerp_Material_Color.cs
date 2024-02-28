using System;

using UnityEngine;

namespace Vanilla.MetaScript.Materials
{

	[Serializable]
	public class Lerp_Material_Color : Lerp
	{

		[SerializeField]
		public Material[] targetMaterials = Array.Empty<Material>();
		[SerializeField]
		public string propertyName = "_BaseColor";
		[NonSerialized]
		private int propertyIndex = -1;
		[SerializeField]
		public Gradient[] gradients = Array.Empty<Gradient>();
		
		protected override bool CanAutoName() => targetMaterials.Length > 0 && targetMaterials[0] != null && !string.IsNullOrEmpty(propertyName);

		protected override string CreateAutoName() => $"Lerp [{targetMaterials[0].name}.{propertyName}]";


		protected override void Init() => propertyIndex = Shader.PropertyToID(propertyName);


		protected override void Frame(float normal,
		                              float easedNormal)
		{
			var c = SampleGradients(easedNormal);
			
			foreach (var m in targetMaterials)
			{
				if (m != null)
					m.SetColor(nameID: propertyIndex,
					           value: c);
			}
		}


		private Color SampleGradients(float n)
		{
			var gradientCount = gradients.Length;

			var gradientBracket =
				(int) Mathf.Clamp(value: Mathf.Floor(f: n * gradientCount),
				                  min: 0,
				                  max: gradientCount - 1);

			var output = n * gradientCount - gradientBracket;

			return gradients[gradientBracket].Evaluate(time: output);
		}


		protected override void CleanUp()
		{
			var c = SampleGradients(1.0f);

			foreach (var m in targetMaterials)
			{
				if (m != null)
					m.SetColor(nameID: propertyIndex,
					           value: c);
			}
		}

	}

}