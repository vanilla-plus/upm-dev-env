#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public abstract class SmartStruct<T> : SmartValue<T> where T : struct
	{

		public SmartStruct() { }

		public SmartStruct(string name) : base(name: name) { }

		public SmartStruct(string name,
		                   T defaultValue) : base(name: name,
		                                          defaultValue: defaultValue) { }

	}

}