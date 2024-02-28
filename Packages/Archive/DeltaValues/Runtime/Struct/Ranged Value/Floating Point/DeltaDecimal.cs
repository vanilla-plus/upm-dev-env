#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaDecimal : DeltaRangedValue<decimal>
	{

		#region Properties



		[SerializeField]
		public decimal ChangeEpsilon = 0.000001m;

		[SerializeField]
		public decimal MinMaxEpsilon = 0.01m;



		#endregion

		#region Construction



		public DeltaDecimal(string defaultName) : base(name: defaultName) { }


		public DeltaDecimal(string defaultName,
		                    decimal defaultValue) : base(defaultName: defaultName,
		                                                 defaultValue: defaultValue) { }

		public DeltaDecimal(string defaultName,
		                    decimal defaultValue,
		                    decimal defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                         defaultValue: defaultValue)
		{
			ChangeEpsilon = defaultChangeEpsilon;
		}

		public DeltaDecimal(string defaultName,
		                    decimal defaultValue,
		                    decimal defaultMin,
		                    decimal defaultMax) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue,
		                                               defaultMin: defaultMin,
		                                               defaultMax: defaultMax) { }

		public DeltaDecimal(string defaultName,
		                    decimal defaultValue,
		                    decimal defaultMin,
		                    decimal defaultMax,
		                    decimal changeEpsilon) : base(defaultName: defaultName,
		                                                  defaultValue: defaultValue,
		                                                  defaultMin: defaultMin,
		                                                  defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
		}

		public DeltaDecimal(string defaultName,
		                    decimal defaultValue,
		                    decimal defaultMin,
		                    decimal defaultMax,
		                    decimal changeEpsilon,
		                    decimal minMaxEpsilon) : base(defaultName: defaultName,
		                                                  defaultValue: defaultValue,
		                                                  defaultMin: defaultMin,
		                                                  defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = minMaxEpsilon;
		}



		#endregion

		#region Overrides



		public override bool ValueEquals(decimal a,
		                                 decimal b) => Math.Abs(value: a - b) < ChangeEpsilon;



		#endregion

		#region Math



		public override decimal TypeMinValue => decimal.MinValue;
		public override decimal TypeMaxValue => decimal.MaxValue;


		public override decimal GetClamped(decimal input,
		                                   decimal min,
		                                   decimal max) => Math.Clamp(value: input,
		                                                              min: min,
		                                                              max: max);


		public override decimal GetLesser(decimal input,
		                                  decimal other) => Math.Min(val1: input,
		                                                             val2: other);


		public override decimal GetGreater(decimal input,
		                                   decimal other) => Math.Max(val1: input,
		                                                              val2: other);


		public override bool LessThan(decimal a,
		                              decimal b) => a < b;


		public override bool GreaterThan(decimal a,
		                                 decimal b) => a > b;


		public override bool ValueAtMin() => Math.Abs(value: _Value - _Min) < MinMaxEpsilon;


		public override bool ValueAtMax() => Math.Abs(value: _Value - _Max) < MinMaxEpsilon;



		#endregion

	}

}