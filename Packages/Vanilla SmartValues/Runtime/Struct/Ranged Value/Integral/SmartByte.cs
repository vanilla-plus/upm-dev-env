#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartByte : SmartRangedValue<byte>
	{

		#region Properties



		#endregion

		#region Construction



		public SmartByte(string defaultName) : base(name: defaultName) { }


		public SmartByte(string defaultName,
		                 byte defaultValue) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue) { }


		public SmartByte(string defaultName,
		                 byte defaultValue,
		                 byte defaultMin,
		                 byte defaultMax) : base(defaultName: defaultName,
		                                         defaultValue: defaultValue,
		                                         defaultMin: defaultMin,
		                                         defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(byte a,
		                                 byte b) => a == b;



		#endregion

		#region Math



		public override byte TypeMinValue => byte.MinValue;
		public override byte TypeMaxValue => byte.MaxValue;


		public override byte GetClamped(byte input,
		                                byte min,
		                                byte max) => Math.Clamp(value: input,
		                                                        min: min,
		                                                        max: max);


		public override byte GetLesser(byte input,
		                               byte other) => Math.Min(val1: input,
		                                                       val2: other);


		public override byte GetGreater(byte input,
		                                byte other) => Math.Max(val1: input,
		                                                        val2: other);


		public override bool LessThan(byte a,
		                              byte b) => a < b;


		public override bool GreaterThan(byte a,
		                                 byte b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

	}

}