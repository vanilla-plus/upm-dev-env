#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartLong : SmartRangedValue<long>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartLong(string defaultName) : base(name: defaultName) { }


		public SmartLong(string defaultName,
		                 long defaultValue) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue) { }


		public SmartLong(string defaultName,
		                 long defaultValue,
		                 long defaultMin,
		                 long defaultMax) : base(defaultName: defaultName,
		                                         defaultValue: defaultValue,
		                                         defaultMin: defaultMin,
		                                         defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(long a,
		                                 long b) => a == b;



		#endregion

		#region Math



		public override long TypeMinValue => long.MinValue;
		public override long TypeMaxValue => long.MaxValue;


		public override long GetClamped(long input,
		                                long min,
		                                long max) => Math.Clamp(value: input,
		                                                        min: min,
		                                                        max: max);


		public override long GetLesser(long input,
		                               long other) => Math.Min(val1: input,
		                                                       val2: other);


		public override long GetGreater(long input,
		                                long other) => Math.Max(val1: input,
		                                                        val2: other);


		public override bool LessThan(long a,
		                              long b) => a < b;


		public override bool GreaterThan(long a,
		                                 long b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}