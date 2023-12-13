//using System;
//
//using UnityEngine;
//using UnityEngine.Events;
//
//namespace Vanilla.Drivers.Snrubs.Vector4
//{
//
//	[Serializable]
//	public class UnityEvent : Vec4Snrub,
//	                          IEventSnrub<UnityEngine.Vector4>
//	{
//
//		[SerializeField]
//		public UnityEvent<UnityEngine.Vector4> onValueChange = new();
//		public UnityEvent<UnityEngine.Vector4> OnValueChange => onValueChange;
//
//		public override void OnValidate(UnityEngine.Vector4 value) => OnValueChange.Invoke(value);
//
//		public override void Init(Vec1DriverSocket vec1DriverSocket) { }
//
//		public override void HandleValueChange(UnityEngine.Vector4 value) => OnValueChange.Invoke(value);
//
//	}
//
//}