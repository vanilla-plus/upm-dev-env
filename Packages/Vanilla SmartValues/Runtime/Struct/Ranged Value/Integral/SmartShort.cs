#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartShort : SmartRangedValue<short>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartShort(string defaultName) : base(defaultName) { }


		public SmartShort(string defaultName,
		                  short defaultValue) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue) { }


		public SmartShort(string defaultName,
		                  short defaultValue,
		                  short defaultMin,
		                  short defaultMax) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue,
		                                           defaultMin: defaultMin,
		                                           defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(short a,
		                                 short b) => a == b;



		#endregion

		#region Math



		public override short TypeMinValue => short.MinValue;
		public override short TypeMaxValue => short.MaxValue;


		public override short GetClamped(short input,
		                                 short min,
		                                 short max) => Math.Clamp(value: input,
		                                                          min: min,
		                                                          max: max);


		public override short GetLesser(short input,
		                                short other) => Math.Min(val1: input,
		                                                         val2: other);


		public override short GetGreater(short input,
		                                 short other) => Math.Max(val1: input,
		                                                          val2: other);


		public override bool LessThan(short a,
		                              short b) => a < b;


		public override bool GreaterThan(short a,
		                                 short b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}