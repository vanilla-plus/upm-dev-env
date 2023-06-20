////#if DEVELOPMENT_BUILD
//#define debug
////#endif
//
//using System;
//using System.Collections.Generic;
//
//using UnityEngine;
//
//namespace Vanilla.SmartValues
//{
//
//	[Serializable]
//	public abstract class SmartValue<T> : BaseSmartValue<T>
//	{
//
////		[SerializeField]
////		private string _Name;
////		public override string Name
////		{
////			get => _Name;
////			set => _Name = value;
////		}
//
////		[SerializeField]
////		public T _Value;
////		public override T Value
////		{
////			get => _Value;
////			set
////			{
////				if (ValueEquals(_Value,
////				                value)) return;
////
////				var old = _Value;
////
////				_Value = value;
////				
////				#if debug
////				Debug.Log($"[{Name}] changed from [{old}] to [{value}]");
////				#endif
////
////				OnValueChanged?.Invoke(old,
////				                       value);
////			}
////		}
//
//		protected SmartValue(string name) : base(name) { }
//
//
//		protected SmartValue(string name,
//		            T defaultValue) : base(name,
//		                                   defaultValue) { }
//
//	}
//
//}