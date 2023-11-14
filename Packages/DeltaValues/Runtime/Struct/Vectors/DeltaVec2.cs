using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.DeltaValues
{
	
	[Serializable]
    public class DeltaVec2 : DeltaVec<Vector2>
    {

	    #region Constructors
	    
	    public DeltaVec2() : base() { }

	    public DeltaVec2(string defaultName) : base(defaultName) { }

	    public DeltaVec2(string defaultName, Vector2 defaultValue) : base(defaultName: defaultName, defaultValue: defaultValue) { }

	    public DeltaVec2(string defaultName, Vector2 defaultValue, float defaultChangeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultChangeEpsilon: defaultChangeEpsilon) { }

	    public DeltaVec2(string defaultName, Vector2 defaultValue, Vector2 defaultMin, Vector2 defaultMax) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax) { }

	    public DeltaVec2(string defaultName, Vector2 defaultValue, Vector2 defaultMin, Vector2 defaultMax, float changeEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon) { }

	    public DeltaVec2(string defaultName, Vector2 defaultValue, Vector2 defaultMin, Vector2 defaultMax, float changeEpsilon, float minMaxEpsilon) : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon, minMaxEpsilon: minMaxEpsilon) { }
	    
	    #endregion
	    
	    public override bool ValueEquals(Vector2 a,
	                                     Vector2 b) => a == b;


	    public override Vector2 Div(Vector2 a) => default;

	    public override Vector2 TypeMinValue => new Vector2(x: float.MinValue,
	                                                        y: float.MinValue);

	    public override Vector2 TypeMaxValue => new Vector2(x: float.MaxValue,
	                                                        y: float.MaxValue);
	    
	    public override Vector2 GetClamped(Vector2 input,
	                                       Vector2 min,
	                                       Vector2 max) => new Vector2(x: Mathf.Clamp(value: input.x,
	                                                                                  min: min.x,
	                                                                                  max: max.x),
	                                                                   y: Mathf.Clamp(value: input.y,
	                                                                                  min: min.y,
	                                                                                  max: max.y));


	    public override Vector2 GetLesser(Vector2 input,
	                                      Vector2 other) => new Vector2(x: Math.Min(val1: input.x,
	                                                                                val2: other.x),
	                                                                    y: Math.Min(val1: input.y,
	                                                                                val2: other.y));


	    public override Vector2 GetGreater(Vector2 input,
	                                       Vector2 other) => new Vector2(x: Math.Max(val1: input.x,
	                                                                                 val2: other.x),
	                                                                     y: Math.Max(val1: input.y,
	                                                                                 val2: other.y));


	    public override bool LessThan(Vector2 a,
	                                  Vector2 b) => a.x < b.x && a.y < b.y;


	    public override bool GreaterThan(Vector2 a,
	                                     Vector2 b) => a.x > b.x && a.y > b.y;


	    public override bool ValueAtMin() => Mathf.Abs(_Value.x - _Min.x) < MinMaxEpsilon && Mathf.Abs(_Value.y - _Min.y) < MinMaxEpsilon;


	    public override bool ValueAtMax() => Mathf.Abs(_Value.x - _Max.x) < MinMaxEpsilon && Mathf.Abs(_Value.y - _Max.y) < MinMaxEpsilon;
	    

	    
	    #region Math



	    public override Vector2 New(float initial) => new(x: initial,
	                                                      y: initial);


	    public override Vector2 Add(Vector2 a) => Value += a;


	    public override Vector2 Sub(Vector2 a) => Value -= a;


	    public override Vector2 Mul(Vector2 a) => Value *= a;



	    #endregion
	    
	    #region Implicits



	    public static implicit operator Vector2(DeltaVec2 input) => input?.Value ?? Vector2.zero;



	    #endregion
		
	    #region Operators

	    public static Vector2 operator +(DeltaVec2 a, DeltaVec2 b) => a.Value + b.Value;
	    public static Vector2 operator -(DeltaVec2 a, DeltaVec2 b) => a.Value - b.Value;
	    public static Vector2 operator *(DeltaVec2 a, DeltaVec2 b) => a.Value * b.Value;
	    public static Vector2 operator /(DeltaVec2 a, DeltaVec2 b) => a.Value / b.Value;
	    public static Vector2 operator *(DeltaVec2 a, float b) => a.Value * b;
	    public static Vector2 operator /(DeltaVec2 a, float b) => a.Value / b;


	    #endregion


	    public override void Lerp(float normal) => Value = Vector2.Lerp(a: Min,
	                                                                    b: Max,
	                                                                    t: normal);


	    public override void Lerp(float outgoing,
	                              float incoming) => Value = Vector2.Lerp(a: Min,
	                                                                      b: Max,
	                                                                      t: incoming);

    }
}