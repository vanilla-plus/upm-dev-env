#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public abstract class DeltaClass<T> : DeltaValue<T> where T : class
	{

		protected DeltaClass(string name) : base(name: name) { }


		protected DeltaClass(string name,
		            T defaultValue) : base(name: name,
		                                   defaultValue: defaultValue) { }

	}

}