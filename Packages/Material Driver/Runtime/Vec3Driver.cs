using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.MaterialDriver
{

	[Serializable]
	public class Vec3Driver : PropertyDriver
	{

		[SerializeField]
		public DeltaVec3 Property = new DeltaVec3();


		public override void Init(float normal)
		{
			Property.OnValueChanged += HandleValueChange;

//			Property.Value = InvertNormal ?
//				                 Property.Max :
//				                 Property.Min;

			Lerp(normal);
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
			foreach (var m in TargetMaterials)
			{
				if (m != null)
					m.SetVector(nameID: PropertyId,
					            value: Property.Value);
			}
			#endif
		}


		private void HandleValueChange(Vector3 outgoing,
		                               Vector3 incoming)
		{
			foreach (var m in TargetMaterials)
			{
				if (m != null)
					m.SetVector(nameID: PropertyId,
					            value: incoming);
			}
		}


		public override void Lerp(float normal) => Property.Lerp(InvertNormal ?
			                                                         1.0f - normal :
			                                                         normal);


		public override void Lerp(float outgoing,
		                          float incoming) => Property.Lerp(InvertNormal ?
			                                                           1.0f - incoming :
			                                                           incoming);


	}

}