#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaClass<T> : DeltaValue<T> where T : class
	{

		public DeltaClass(string name) : base(name: name) { }


		public DeltaClass(string name,
		                  T defaultValue) : base(name: name,
		                                         defaultValue: defaultValue) { }


		public override bool ValueEquals(T a,
		                                 T b) => ReferenceEquals(a,b);

	}

}