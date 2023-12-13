//using System;
//
//using UnityEngine;
//using UnityEngine.Events;
//
//namespace Vanilla.Drivers.Snrubs.Color
//{
//
//	[Serializable]
//	public class UnityEvent : ColorSnrub,
//	                          IEventSnrub<UnityEngine.Color>
//	{
//
//		[SerializeField]
//		public UnityEvent<UnityEngine.Color> onValueChange = new();
//		public UnityEvent<UnityEngine.Color> OnValueChange => onValueChange;
//
//		public override void OnValidate(UnityEngine.Color value) => OnValueChange.Invoke(value);
//
//		public override void Init(Vec1DriverSocket vec1DriverSocket) { }
//
//		public override void HandleValueChange(UnityEngine.Color value) => OnValueChange.Invoke(value);
//
//	}
//
//}