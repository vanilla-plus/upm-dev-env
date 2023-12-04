using System;

using UnityEngine;

namespace Vanilla.MetaScript.Materials
{

	[Serializable]
	public class Lerp_Material_Float : Lerp
	{

		[SerializeField]
		public Material[] targetMaterials = Array.Empty<Material>();
		[SerializeField]
		public string propertyName = "_Opacity";
		[SerializeField]
		public float from;
		[SerializeField]
		public float to;
		[NonSerialized]
		private int propertyIndex = -1;

		protected override bool CanAutoName() => targetMaterials.Length > 0 && targetMaterials[0] != null && !string.IsNullOrEmpty(propertyName);

		protected override string CreateAutoName() => $"Lerp [{targetMaterials[0].name}.{propertyName}] from [{@from}] to [{to}]";


		protected override void Init() => propertyIndex = Shader.PropertyToID(propertyName);


		protected override void Frame(float normal,
		                              float easedNormal)
		{
			foreach (var m in targetMaterials)
			{
				if (m != null)
					m.SetFloat(nameID: propertyIndex,
					           value: Mathf.Lerp(a: @from,
					                             b: to,
					                             t: easedNormal));
			}
		}


		protected override void CleanUp()
		{
			foreach (var m in targetMaterials)
			{
				if (m != null)
					m.SetFloat(nameID: propertyIndex,
					           value: to);
			}
		}

	}

}