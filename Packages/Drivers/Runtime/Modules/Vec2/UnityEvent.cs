//using System;
//
//using UnityEngine;
//using UnityEngine.Events;
//
//namespace Vanilla.Drivers.Snrubs.Vector2
//{
//
//	[Serializable]
//	public class UnityEvent : Vec2Snrub,
//	                          IEventSnrub<UnityEngine.Vector2>
//	{
//
//		[SerializeField]
//		public UnityEvent<UnityEngine.Vector2> onValueChange = new();
//		public UnityEvent<UnityEngine.Vector2> OnValueChange => onValueChange;
//
//		public override void OnValidate(UnityEngine.Vector2 value) => OnValueChange.Invoke(value);
//
//		public override void Init(Vec1DriverSocket vec1DriverSocket) { }
//
//		public override void HandleValueChange(UnityEngine.Vector2 value) => OnValueChange.Invoke(value);
//
//	}
//
//}