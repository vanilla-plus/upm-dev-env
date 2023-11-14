using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

#if vanilla_metascript
using Vanilla.MetaScript;
#endif

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaVec1 : DeltaVec<float>
	{
		
		#region Constructors
	    
		public DeltaVec1() : base() { }
		public DeltaVec1(string defaultName) : base(defaultName) { }
		public DeltaVec1(string defaultName, float defaultValue) : base(defaultName: defaultName, defaultValue: defaultValue) { }
		public DeltaVec1(string defaultName, float defaultValue, float defaultChangeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultChangeEpsilon: defaultChangeEpsilon) { }
		public DeltaVec1(string defaultName, float defaultValue, float defaultMin, float defaultMax) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax) { }
		public DeltaVec1(string defaultName, float defaultValue, float defaultMin, float defaultMax, float changeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon) { }
		public DeltaVec1(string defaultName, float defaultValue, float defaultMin, float defaultMax, float changeEpsilon, float minMaxEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon, minMaxEpsilon: minMaxEpsilon) { }

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
		

		public override float New(float initial) => initial;

		public override float Add(float a) => Value += a;


		public override float Sub(float a) => Value -= a;


		public override float Mul(float a) => Value *= a;


		public override float Div(float a) => Value /= a;
		

		#endregion
		
		#region Implicits



		public static implicit operator float(DeltaVec1 input) => input?.Value ?? 0f;



		#endregion
		
		#region Operators
		

		public static float operator +(DeltaVec1 a, DeltaVec1 b) => a.Value + b.Value;
		public static float operator -(DeltaVec1 a, DeltaVec1 b) => a.Value - b.Value;
		public static float operator *(DeltaVec1 a, DeltaVec1 b) => a.Value * b.Value;
		public static float operator /(DeltaVec1 a, DeltaVec1 b) => a.Value / b.Value;


		#endregion


		public override void Lerp(float normal) => Value = Mathf.Lerp(a: Min,
		                                                              b: Max,
		                                                              t: normal);


		public override void Lerp(float outgoing,
		                          float incoming) => Value = Mathf.Lerp(a: Min,
		                                                                b: Max,
		                                                                t: incoming);

	}

}