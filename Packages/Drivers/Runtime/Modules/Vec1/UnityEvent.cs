//using System;
//
//using UnityEngine;
//using UnityEngine.Events;
//
//namespace Vanilla.Drivers.Snrubs.Vector1
//{
//
//	[Serializable]
//	public class UnityEvent : Vec1Snrub,
//	                          IEventSnrub<float>
//	{
//
//		[SerializeField]
//		public UnityEvent<float> onValueChange = new();
//		public UnityEvent<float> OnValueChange => onValueChange;
//
//		public override void OnValidate(float value) => OnValueChange.Invoke(value);
//
//		public override void Init(Vec1DriverSocket vec1DriverSocket) { }
//
//		public override void HandleValueChange(float value) => OnValueChange.Invoke(value);
//
//	}
//
//}