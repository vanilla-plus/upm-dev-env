#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaDouble : DeltaRangedValue<double>
	{

		#region Properties



		[SerializeField]
		public double ChangeEpsilon = double.Epsilon;

		[SerializeField]
		public double MinMaxEpsilon = 0.01f;



		#endregion

		#region Construction



		public DeltaDouble(string defaultName) : base(name: defaultName) { }


		public DeltaDouble(string defaultName,
		                   double defaultValue) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue) { }


		public DeltaDouble(string defaultName,
		                   double defaultValue,
		                   double defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                       defaultValue: defaultValue)
		{
			ChangeEpsilon = defaultChangeEpsilon;
		}


		public DeltaDouble(string defaultName,
		                   double defaultValue,
		                   double defaultMin,
		                   double defaultMax) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue,
		                                             defaultMin: defaultMin,
		                                             defaultMax: defaultMax) { }


		public DeltaDouble(string defaultName,
		                   double defaultValue,
		                   double defaultMin,
		                   double defaultMax,
		                   double changeEpsilon) : base(defaultName: defaultName,
		                                                defaultValue: defaultValue,
		                                                defaultMin: defaultMin,
		                                                defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
		}


		public DeltaDouble(string defaultName,
		                   double defaultValue,
		                   double defaultMin,
		                   double defaultMax,
		                   double changeEpsilon,
		                   double minMaxEpsilon) : base(defaultName: defaultName,
		                                                defaultValue: defaultValue,
		                                                defaultMin: defaultMin,
		                                                defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = minMaxEpsilon;
		}



		#endregion

		#region Overrides



		public override bool ValueEquals(double a,
		                                 double b) => Math.Abs(value: a - b) < ChangeEpsilon;



		#endregion

		#region Math



		public override double TypeMinValue => double.MinValue;
		public override double TypeMaxValue => double.MaxValue;


		public override double GetClamped(double input,
		                                  double min,
		                                  double max) => Math.Clamp(value: input,
		                                                            min: min,
		                                                            max: max);


		public override double GetLesser(double input,
		                                 double other) => Math.Min(val1: input,
		                                                           val2: other);


		public override double GetGreater(double input,
		                                  double other) => Math.Max(val1: input,
		                                                            val2: other);


		public override bool LessThan(double a,
		                              double b) => a < b;


		public override bool GreaterThan(double a,
		                                 double b) => a > b;


		public override bool ValueAtMin() => Math.Abs(value: _Value - _Min) < MinMaxEpsilon;


		public override bool ValueAtMax() => Math.Abs(value: _Value - _Max) < MinMaxEpsilon;



		#endregion

	}

}