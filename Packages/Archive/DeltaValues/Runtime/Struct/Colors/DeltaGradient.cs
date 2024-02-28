using System;

using UnityEngine;

#if vanilla_metascript
using Vanilla.MetaScript;
#endif

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaGradient : DeltaVec<float>
	{

		[SerializeField]
		public DeltaColor Color = new DeltaColor(defaultName: "DeltaGradient.Color",
		                                         defaultValue: UnityEngine.Color.white,
		                                         defaultMin: UnityEngine.Color.black,
		                                         defaultMax: UnityEngine.Color.white);
		
		#region Constructors
	    
		public DeltaGradient() : base() { }
		public DeltaGradient(string defaultName) : base(defaultName) { }
		public DeltaGradient(string defaultName, float defaultValue) : base(defaultName: defaultName, defaultValue: defaultValue) { }
		public DeltaGradient(string defaultName, float defaultValue, float defaultChangeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultChangeEpsilon: defaultChangeEpsilon) { }
		public DeltaGradient(string defaultName, float defaultValue, float defaultMin, float defaultMax) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax) { }
		public DeltaGradient(string defaultName, float defaultValue, float defaultMin, float defaultMax, float changeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon) { }
		public DeltaGradient(string defaultName, float defaultValue, float defaultMin, float defaultMax, float changeEpsilon, float minMaxEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon, minMaxEpsilon: minMaxEpsilon) { }

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



		public static implicit operator Color(DeltaGradient input) => input?.Color?.Value ?? UnityEngine.Color.white;



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