using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.MaterialDriver
{

	[Serializable]
	public class Vec1Driver : PropertyDriver
	{

		[SerializeField]
		public DeltaVec1 Property = new DeltaVec1();


		public override void Init(float normal)
		{
			Property.OnValueChanged += HandleValueChange;

			Lerp(normal: normal);
		}


		public override void DeInit() => Property.OnValueChanged -= HandleValueChange;

		public override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();

			Property.Name = Name;

			// Can't set a Material assets value from here :( Unity gets mad.
		}


		public override void OnValidate(float normal)
		{
			#if UNITY_EDITOR
			Property.Value = Mathf.Lerp(a: Property.Min,
			                            b: Property.Max,
			                            t: normal);
			
			foreach (var m in TargetMaterials)
			{
				if (m != null) m.SetFloat(nameID: PropertyId,
				                          value: Property.Value);
			}
			#endif
		}


		private void HandleValueChange(float outgoing,
		                               float incoming)
		{
			foreach (var m in TargetMaterials)
			{
				if (m != null) m.SetFloat(nameID: PropertyId,
				                          value: incoming);
			}
		}


		public override void Lerp(float normal) => Property.Lerp(InvertNormal ? 1.0f - normal : normal);

		public override void Lerp(float outgoing, float incoming) => Property.Lerp(InvertNormal ? 1.0f - incoming : incoming);


	}

}