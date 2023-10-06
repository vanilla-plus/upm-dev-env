#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaUShort : DeltaRangedValue<ushort>
	{

		#region Properties



		#endregion

		#region Construction



		public DeltaUShort(string defaultName) : base(defaultName) { }


		public DeltaUShort(string defaultName,
		                   ushort defaultValue) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue) { }


		public DeltaUShort(string defaultName,
		                   ushort defaultValue,
		                   ushort defaultMin,
		                   ushort defaultMax) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue,
		                                             defaultMin: defaultMin,
		                                             defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(ushort a,
		                                 ushort b) => a == b;



		#endregion

		#region Math



		public override ushort TypeMinValue => ushort.MinValue;
		public override ushort TypeMaxValue => ushort.MaxValue;


		public override ushort GetClamped(ushort input,
		                                  ushort min,
		                                  ushort max) => Math.Clamp(value: input,
		                                                            min: min,
		                                                            max: max);


		public override ushort GetLesser(ushort input,
		                                 ushort other) => Math.Min(val1: input,
		                                                           val2: other);


		public override ushort GetGreater(ushort input,
		                                  ushort other) => Math.Max(val1: input,
		                                                            val2: other);


		public override bool LessThan(ushort a,
		                              ushort b) => a < b;


		public override bool GreaterThan(ushort a,
		                                 ushort b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}