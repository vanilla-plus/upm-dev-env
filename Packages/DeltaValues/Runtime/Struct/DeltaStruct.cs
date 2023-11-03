#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public abstract class DeltaStruct<T> : DeltaValue<T> where T : struct
	{

		public DeltaStruct() { }

		public DeltaStruct(string name) : base(name: name) { }


		public DeltaStruct(string name,
		                   T defaultValue) : base(name: name,
		                                          defaultValue: defaultValue) => _Value = defaultValue;

	}

}