using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaNormal : DeltaVec<float>
	{

		public override bool ValueEquals(float a,
		                                 float b) => Mathf.Abs(a - b) < ChangeEpsilon;


		public override float TypeMinValue => 0.0f;

		public override float TypeMaxValue => 1.0f;


		public override float GetClamped(float input,
		                                 float min,
		                                 float max) => Mathf.Clamp01(input);


		public override float GetLesser(float input,
		                                float other) => Mathf.Min(input,
		                                                          other);


		public override float GetGreater(float input,
		                                 float other) => Mathf.Max(input,
		                                                           other);


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


		public override void Lerp(float normal) => Value = normal;


		public override void Lerp(float outgoing,
		                          float incoming) => Value = incoming;

	}

}