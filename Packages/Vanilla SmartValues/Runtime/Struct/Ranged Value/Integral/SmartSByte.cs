#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartSByte : SmartRangedValue<sbyte>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartSByte(string defaultName) : base(name: defaultName) { }


		public SmartSByte(string defaultName,
		                  sbyte defaultValue) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue) { }


		public SmartSByte(string defaultName,
		                  sbyte defaultValue,
		                  sbyte defaultMin,
		                  sbyte defaultMax) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue,
		                                           defaultMin: defaultMin,
		                                           defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(sbyte a,
		                                 sbyte b) => a == b;



		#endregion

		#region Math



		public override sbyte TypeMinValue => sbyte.MinValue;
		public override sbyte TypeMaxValue => sbyte.MaxValue;


		public override sbyte GetClamped(sbyte input,
		                                 sbyte min,
		                                 sbyte max) => Math.Clamp(value: input,
		                                                          min: min,
		                                                          max: max);


		public override sbyte GetLesser(sbyte input,
		                                sbyte other) => Math.Min(val1: input,
		                                                         val2: other);


		public override sbyte GetGreater(sbyte input,
		                                 sbyte other) => Math.Max(val1: input,
		                                                          val2: other);


		public override bool LessThan(sbyte a,
		                              sbyte b) => a < b;


		public override bool GreaterThan(sbyte a,
		                                 sbyte b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}