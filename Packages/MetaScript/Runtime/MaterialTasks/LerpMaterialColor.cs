using System;

using UnityEngine;

namespace Vanilla.MetaScript.MaterialTasks
{

	[Serializable]
	public class LerpMaterialColor : Lerp
	{

		[SerializeField]
		public Material targetMaterial;
		[SerializeField]
		public string propertyName = "_BaseColor";
		[NonSerialized]
		private int propertyIndex = -1;
		[SerializeField]
		public Gradient[] gradients = Array.Empty<Gradient>();
		
		protected override bool CanAutoName() => !string.IsNullOrEmpty(propertyName);

		protected override string CreateAutoName() => $"Lerp [{targetMaterial.name}.{propertyName}]";


		protected override void Init() => propertyIndex = Shader.PropertyToID(propertyName);


		protected override void Frame(float normal,
		                              float easedNormal) => targetMaterial.SetColor(nameID: propertyIndex,
		                                                                            value: SampleGradients(easedNormal));


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


		protected override void CleanUp() { }

	}

}