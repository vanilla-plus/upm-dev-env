using System;

using UnityEngine;

namespace Vanilla.MetaScript.MaterialTasks
{

	[Serializable]
	public class LerpMaterialFloat : Lerp
	{

		[SerializeField]
		public Material targetMaterial;
		[SerializeField]
		public string propertyName = "_Opacity";
		[SerializeField]
		public float from;
		[SerializeField]
		public float to;
		[NonSerialized]
		private int propertyIndex = -1;

		protected override bool CanAutoName() => !string.IsNullOrEmpty(propertyName);

		protected override string CreateAutoName() => $"Lerp [{targetMaterial.name}.{propertyName}] from [{@from}] to [{to}]";


		protected override void Init() => propertyIndex = Shader.PropertyToID(propertyName);


		protected override void Frame(float normal,
		                              float easedNormal) => targetMaterial.SetFloat(nameID: propertyIndex,
		                                                                            value: Mathf.Lerp(a: from,
		                                                                                              b: to,
		                                                                                              t: easedNormal));


		protected override void CleanUp() => targetMaterial.SetFloat(nameID: propertyIndex,
		                                                             value: to);

	}

}