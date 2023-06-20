using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public abstract class SmartStruct<T> : SmartValue<T> where T : struct
	{

		public SmartStruct() { }

		public SmartStruct(string name) : base(name) { }

		public SmartStruct(string name,
		                   T defaultValue) : base(name,
		                                          defaultValue) { }

	}

}