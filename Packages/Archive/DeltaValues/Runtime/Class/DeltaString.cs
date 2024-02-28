#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaString : DeltaValue<string>
	{

		public DeltaString() : base(name: "Unknown DeltaString") { }

		public DeltaString(string name) : base(name: name) { }


		public DeltaString(string name,
		                   string defaultValue) : base(name: name,
		                                          defaultValue: defaultValue) { }


		public override bool ValueEquals(string a,
		                                 string b) => string.Equals(a: a,
		                                                            b: b);

	}

}