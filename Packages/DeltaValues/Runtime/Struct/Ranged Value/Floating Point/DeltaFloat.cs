#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaFloat : DeltaRangedValue<float>
	{

		#region Properties



		[SerializeField]
		public float ChangeEpsilon = Mathf.Epsilon;

		[SerializeField]
		public float MinMaxEpsilon = 0.01f;



		#endregion

		#region Construction



		public DeltaFloat(string defaultName) : base(name: defaultName)
		{
			_Min = float.MinValue;
			_Max = float.MaxValue;
			
			Min = _Min;
			Max = _Max;
			
			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaFloat(string defaultName,
		                  float defaultValue) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue)
		{
			_Min = float.MinValue;
			_Max = float.MaxValue;
			
			Min = _Min;
			Max = _Max;
			
			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaFloat(string defaultName,
		                  float defaultValue,
		                  float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                     defaultValue: defaultValue)
		{
			_Min = float.MinValue;
			_Max = float.MaxValue;
			
			Min = _Min;
			Max = _Max;
			
			ChangeEpsilon = defaultChangeEpsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaFloat(string defaultName,
		                  float defaultValue,
		                  float defaultMin,
		                  float defaultMax) : base(defaultName: defaultName,
		                                           defaultValue: defaultValue,
		                                           defaultMin: defaultMin,
		                                           defaultMax: defaultMax)
		{
			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaFloat(string defaultName,
		                  float defaultValue,
		                  float defaultMin,
		                  float defaultMax,
		                  float changeEpsilon) : base(defaultName: defaultName,
		                                              defaultValue: defaultValue,
		                                              defaultMin: defaultMin,
		                                              defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaFloat(string defaultName,
		                  float defaultValue,
		                  float defaultMin,
		                  float defaultMax,
		                  float changeEpsilon,
		                  float minMaxEpsilon) : base(defaultName: defaultName,
		                                              defaultValue: defaultValue,
		                                              defaultMin: defaultMin,
		                                              defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = minMaxEpsilon;
		}



		#endregion

		#region Overrides



		public override bool ValueEquals(float a,
		                                 float b) => Math.Abs(value: a - b) < ChangeEpsilon;



		#endregion

		#region Math



		public override float TypeMinValue => float.MinValue;
		public override float TypeMaxValue => float.MaxValue;


		public override float GetClamped(float input,
		                                 float min,
		                                 float max) => Math.Clamp(value: input,
		                                                          min: min,
		                                                          max: max);


		public override float GetLesser(float input,
		                                float other) => Math.Min(val1: input,
		                                                         val2: other);


		public override float GetGreater(float input,
		                                 float other) => Math.Max(val1: input,
		                                                          val2: other);


		public override bool LessThan(float a,
		                              float b) => a < b;


		public override bool GreaterThan(float a,
		                                 float b) => a > b;


		public override bool ValueAtMin() => Math.Abs(value: _Value - _Min) < MinMaxEpsilon;


		public override bool ValueAtMax() => Math.Abs(value: _Value - _Max) < MinMaxEpsilon;



		#endregion

	}

}