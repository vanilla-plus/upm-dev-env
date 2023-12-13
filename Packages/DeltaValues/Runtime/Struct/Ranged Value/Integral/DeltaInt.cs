#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaInt : DeltaRangedValue<int>
	{

		#region Properties



		#endregion

		#region Construction



		public DeltaInt() : base(name: "Unknown DeltaInt")
		{
			_Min = TypeMinValue;
			_Max = TypeMaxValue;

			ValidateRangedValue();
		}


		public DeltaInt(string defaultName) : base(name: defaultName) { }


		public DeltaInt(string defaultName,
		                int defaultValue) : base(defaultName: defaultName,
		                                         defaultValue: defaultValue) { }


		public DeltaInt(string defaultName,
		                int defaultValue,
		                int defaultMin,
		                int defaultMax) : base(defaultName: defaultName,
		                                       defaultValue: defaultValue,
		                                       defaultMin: defaultMin,
		                                       defaultMax: defaultMax) { }



		#endregion

		#region Overrides



		public override bool ValueEquals(int a,
		                                 int b) => a == b;



		#endregion

		#region Math



		public override int TypeMinValue => int.MinValue;
		public override int TypeMaxValue => int.MaxValue;


		public override int GetClamped(int input,
		                               int min,
		                               int max) => Math.Clamp(value: input,
		                                                      min: min,
		                                                      max: max);


		public override int GetLesser(int input,
		                              int other) => Math.Min(val1: input,
		                                                     val2: other);


		public override int GetGreater(int input,
		                               int other) => Math.Max(val1: input,
		                                                      val2: other);


		public override bool LessThan(int a,
		                              int b) => a < b;


		public override bool GreaterThan(int a,
		                                 int b) => a > b;


		public override bool ValueAtMin() => _Value == _Min;


		public override bool ValueAtMax() => _Value == _Max;



		#endregion

		#region implicits



		public static implicit operator int(DeltaInt input) => input?.Value ?? 0;



		#endregion

	}

}