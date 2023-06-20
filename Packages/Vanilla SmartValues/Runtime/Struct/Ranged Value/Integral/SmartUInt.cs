#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartUInt : SmartRangedValue<uint>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartUInt(string defaultName) : base(name: defaultName) { }


		public SmartUInt(string defaultName,
		                 uint defaultValue) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue) { }


		public SmartUInt(string defaultName,
		                 uint defaultValue,
		                 uint defaultMin,
		                 uint defaultMax) : base(defaultName: defaultName,
		                                         defaultValue: defaultValue,
		                                         defaultMin: defaultMin,
		                                         defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(uint a,
		                                 uint b) => a == b;



		#endregion

		#region Math



		public override uint TypeMinValue => uint.MinValue;
		public override uint TypeMaxValue => uint.MaxValue;


		public override uint GetClamped(uint input,
		                                uint min,
		                                uint max) => Math.Clamp(value: input,
		                                                        min: min,
		                                                        max: max);


		public override uint GetLesser(uint input,
		                               uint other) => Math.Min(val1: input,
		                                                       val2: other);


		public override uint GetGreater(uint input,
		                                uint other) => Math.Max(val1: input,
		                                                        val2: other);


		public override bool LessThan(uint a,
		                              uint b) => a < b;


		public override bool GreaterThan(uint a,
		                                 uint b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}