#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartBool : SmartStruct<bool>
	{

		#region Properties
		
		public Action OnTrue;
		
		public Action OnFalse;
		
		#endregion
		
		#region Overrides

		
		public override bool Value
		{
			get => _Value;
			set
			{
				if (ValueEquals(a: _Value,
				                b: value)) return;

				var old = _Value;

				_Value = value;

				#if debug
				Debug.Log(message: $"[{Name}] changed from [{old}] to [{value}]");
				#endif

				OnValueChanged?.Invoke(arg1: old,
				                       arg2: value);

				if (_Value)
				{
					OnTrue?.Invoke();
				}
				else
				{
					OnFalse?.Invoke();
				}
			}
		}


		public override bool ValueEquals(bool a,
		                                 bool b) => a == b;
		
		#endregion
		
		#region Operators
		
		public static implicit operator bool(SmartBool input) => input is
		                                                             {
			                                                             Value: true
		                                                             };
		
		#endregion
		
		#region Construction
		
		public SmartBool() { }
		
		public SmartBool(string name) : base(name: name) { }

		public SmartBool(string name,
		                 bool defaultValue) : base(name: name,
		                                           defaultValue: defaultValue) { }

		#endregion
		
		#region Utility

		public void Flip() => Value = !_Value;
		
		public void Invoke()
		{
			_Value = !_Value;

			Value = !_Value;
		}
		
		#endregion

	}

}