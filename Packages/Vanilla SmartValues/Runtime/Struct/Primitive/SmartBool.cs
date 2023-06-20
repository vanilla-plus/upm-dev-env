#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartBool : SmartStruct<bool>
	{

		#region Overrides

		public override bool ValueEquals(bool a,
		                                 bool b) => a == b;
		
		#endregion
		
		#region Construction
		
		public SmartBool() { }
		
		public SmartBool(string name) : base(name) { }

		public SmartBool(string name,
		                 bool defaultValue) : base(name,
		                                           defaultValue) { }

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