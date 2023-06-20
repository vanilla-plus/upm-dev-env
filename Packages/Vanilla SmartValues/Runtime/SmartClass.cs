using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public abstract class SmartClass<T> : SmartValue<T> where T : class
	{

		protected SmartClass(string name) : base(name) { }


		protected SmartClass(string name,
		            T defaultValue) : base(name,
		                                   defaultValue) { }

	}

}