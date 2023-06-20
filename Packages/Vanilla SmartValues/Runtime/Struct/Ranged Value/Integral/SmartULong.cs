#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartULong : SmartRangedValue<ulong>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartULong(string defaultName) : base(name: defaultName) { }


		public SmartULong(string defaultName,
		                  ulong defaultValue) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue) { }


		public SmartULong(string defaultName,
		                  ulong defaultValue,
		                  ulong defaultMin,
		                  ulong defaultMax) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue,
		                                           defaultMin: defaultMin,
		                                           defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(ulong a,
		                                 ulong b) => a == b;



		#endregion

		#region Math



		public override ulong TypeMinValue => ulong.MinValue;
		public override ulong TypeMaxValue => ulong.MaxValue;


		public override ulong GetClamped(ulong input,
		                                 ulong min,
		                                 ulong max) => Math.Clamp(value: input,
		                                                          min: min,
		                                                          max: max);


		public override ulong GetLesser(ulong input,
		                                ulong other) => Math.Min(val1: input,
		                                                         val2: other);


		public override ulong GetGreater(ulong input,
		                                 ulong other) => Math.Max(val1: input,
		                                                          val2: other);


		public override bool LessThan(ulong a,
		                              ulong b) => a < b;


		public override bool GreaterThan(ulong a,
		                                 ulong b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}