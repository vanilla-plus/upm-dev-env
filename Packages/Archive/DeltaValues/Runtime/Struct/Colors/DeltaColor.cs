using System;
using UnityEngine;

namespace Vanilla.DeltaValues
{
	[Serializable]
	public class DeltaColor : DeltaVec<Color>
	{
		
//		[SerializeField]
//		public Gradient[] gradients = Array.Empty<Gradient>();
		
		#region Constructors

		public DeltaColor() : base() { }

		public DeltaColor(string defaultName) : base(defaultName) { }

		public DeltaColor(string defaultName, Color defaultValue) : base(defaultName, defaultValue) { }

		public DeltaColor(string defaultName, Color defaultValue, float defaultChangeEpsilon) 
			: base(defaultName, defaultValue, defaultChangeEpsilon) { }

		public DeltaColor(string defaultName, Color defaultValue, Color defaultMin, Color defaultMax) 
			: base(defaultName, defaultValue, defaultMin, defaultMax) { }

		public DeltaColor(string defaultName, Color defaultValue, Color defaultMin, Color defaultMax, float changeEpsilon) 
			: base(defaultName, defaultValue, defaultMin, defaultMax, changeEpsilon) { }

		public DeltaColor(string defaultName, Color defaultValue, Color defaultMin, Color defaultMax, float changeEpsilon, float minMaxEpsilon) 
			: base(defaultName, defaultValue, defaultMin, defaultMax, changeEpsilon, minMaxEpsilon) { }


		#endregion

		public override bool ValueEquals(Color a, Color b) => a.Equals(b);

		public override Color TypeMinValue => new Color(0f, 0f, 0f, 0f);

		public override Color TypeMaxValue => new Color(1f, 1f, 1f, 1f);

		public override Color GetClamped(Color input, Color min, Color max) 
			=> new Color(
			             Mathf.Clamp(input.r, min.r, max.r),
			             Mathf.Clamp(input.g, min.g, max.g),
			             Mathf.Clamp(input.b, min.b, max.b),
			             Mathf.Clamp(input.a, min.a, max.a));


		public override Color GetLesser(Color input,
		                                Color other) => default;


		public override Color GetGreater(Color input,
		                                 Color other) => default;


		public override bool LessThan(Color a,
		                              Color b) => false;


		public override bool GreaterThan(Color a,
		                                 Color b) => false;


		// Implement GetLesser, GetGreater, LessThan, GreaterThan using Color components

		public override bool ValueAtMin() => 
			Mathf.Abs(_Value.r - _Min.r) < MinMaxEpsilon 
		 && Mathf.Abs(_Value.g - _Min.g) < MinMaxEpsilon 
		 && Mathf.Abs(_Value.b - _Min.b) < MinMaxEpsilon
		 && Mathf.Abs(_Value.a - _Min.a) < MinMaxEpsilon;

		public override bool ValueAtMax() =>
			Mathf.Abs(_Value.r - _Max.r) < MinMaxEpsilon 
		 && Mathf.Abs(_Value.g - _Max.g) < MinMaxEpsilon 
		 && Mathf.Abs(_Value.b - _Max.b) < MinMaxEpsilon
		 && Mathf.Abs(_Value.a - _Max.a) < MinMaxEpsilon;

		#region Math

		public override Color Add(Color a) => default;


		public override Color Sub(Color a) => default;


		public override Color Mul(Color a) => default;


		public override Color Div(Color a) => default;

		#endregion
        
		#region Implicits

		public static implicit operator Color(DeltaColor input) => input?.Value ?? Color.white;

		#endregion
		
		#region Operators

		// Implement operators for Color

		#endregion


		public override Color New(float initial) => new(initial,
		                                                initial,
		                                                initial,
		                                                initial);


		public override void Lerp(float normal) => Value = Color.Lerp(Min,
		                                                              Max,
		                                                              normal);


		public override void Lerp(float outgoing,
		                          float incoming) => Value = Color.Lerp(Min,
		                                                                Max,
		                                                                incoming);


//		public override void Lerp(float normal) => Value = SampleGradients(normal);
//
//
//		public override void Lerp(float outgoing,
//		                          float incoming) => Value = SampleGradients(incoming);

//
//		public Color SampleGradients(float normal)
//		{
//			var gradientCount = gradients.Length;
//
//			if (gradientCount == 0) return Color.white;
//
//			var gradientBracket =
//				(int) Mathf.Clamp(value: Mathf.Floor(f: normal * gradientCount),
//				                  min: 0,
//				                  max: gradientCount - 1);
//
//			var output = normal * gradientCount - gradientBracket;
//
//			return gradients[gradientBracket].Evaluate(time: output);
//		}

	}
}