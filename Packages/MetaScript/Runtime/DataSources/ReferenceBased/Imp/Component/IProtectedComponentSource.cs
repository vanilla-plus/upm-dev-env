using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataSources.GenericComponent;

namespace Vanilla.MetaScript
{
    
//	[Serializable]
	public interface IProtectedComponentSource<T,S> : IComponentSource<T,S> 
		where T : Component
		where S : IComponentSource<T, S>
	{
        
//		[SerializeField]
//		private T _value;
//		public T Value
//		{
//			get => _value;
//			set
//			{
//				if (ReferenceEquals(objA: _value,
//				                    objB: value)) return;
//				
//				var old = _value;
//                
//				_value = value;
//                
//				OnSet?.Invoke(_value);
//				OnSetWithHistory?.Invoke(_value, old);
//			}
//		}
//
//		public Action<T> OnSet
//		{
//			get;
//			set;
//		}
//
//		public Action<T, T> OnSetWithHistory
//		{
//			get;
//			set;
//		}
//
//		public void OnBeforeSerialize() { }
//
//		public void OnAfterDeserialize() { }

	}
}