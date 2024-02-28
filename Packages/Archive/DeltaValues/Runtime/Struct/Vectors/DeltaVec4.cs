using System;
using UnityEngine;

using Vanilla.UnityExtensions;

namespace Vanilla.DeltaValues
{
    [Serializable]
    public class DeltaVec4 : DeltaVec<Vector4>
    {
        
        #region Constructors

        public DeltaVec4() : base() { }

        public DeltaVec4(string defaultName) : base(defaultName) { }

        public DeltaVec4(string defaultName, Vector4 defaultValue) : base(defaultName: defaultName, defaultValue: defaultValue) { }

        public DeltaVec4(string defaultName, Vector4 defaultValue, float defaultChangeEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultChangeEpsilon: defaultChangeEpsilon) { }

        public DeltaVec4(string defaultName, Vector4 defaultValue, Vector4 defaultMin, Vector4 defaultMax) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax) { }

        public DeltaVec4(string defaultName, Vector4 defaultValue, Vector4 defaultMin, Vector4 defaultMax, float changeEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon) { }

        public DeltaVec4(string defaultName, Vector4 defaultValue, Vector4 defaultMin, Vector4 defaultMax, float changeEpsilon, float minMaxEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon, minMaxEpsilon: minMaxEpsilon) { }

        #endregion
        
        public override bool ValueEquals(Vector4 a, Vector4 b) => a == b;


        public override         Vector4 TypeMinValue => new Vector4(x: float.MinValue, y: float.MinValue, z: float.MinValue);

        public override Vector4 TypeMaxValue => new Vector4(x: float.MaxValue, y: float.MaxValue, z: float.MaxValue);

        public override Vector4 GetClamped(Vector4 input, Vector4 min, Vector4 max) 
            => new Vector4(
                x: Mathf.Clamp(value: input.x, min: min.x, max: max.x),
                y: Mathf.Clamp(value: input.y, min: min.y, max: max.y),
                z: Mathf.Clamp(value: input.z, min: min.z, max: max.z));

        public override Vector4 GetLesser(Vector4 input, Vector4 other) 
            => new Vector4(
                x: Math.Min(val1: input.x, val2: other.x),
                y: Math.Min(val1: input.y, val2: other.y),
                z: Math.Min(val1: input.z, val2: other.z));

        public override Vector4 GetGreater(Vector4 input, Vector4 other) 
            => new Vector4(
                x: Math.Max(val1: input.x, val2: other.x),
                y: Math.Max(val1: input.y, val2: other.y),
                z: Math.Max(val1: input.z, val2: other.z));

        public override bool LessThan(Vector4 a, Vector4 b) => a.x < b.x && a.y < b.y && a.z < b.z;

        public override bool GreaterThan(Vector4 a, Vector4 b) => a.x > b.x && a.y > b.y && a.z > b.z;

        public override bool ValueAtMin() => 
               Mathf.Abs(_Value.x - _Min.x) < MinMaxEpsilon 
            && Mathf.Abs(_Value.y - _Min.y) < MinMaxEpsilon 
            && Mathf.Abs(_Value.z - _Min.z) < MinMaxEpsilon;

        public override bool ValueAtMax() => 
               Mathf.Abs(_Value.x - _Max.x) < MinMaxEpsilon 
            && Mathf.Abs(_Value.y - _Max.y) < MinMaxEpsilon 
            && Mathf.Abs(_Value.z - _Max.z) < MinMaxEpsilon;



        #region Math



        public override Vector4 New(float initial) => new(x: initial,
                                                          y: initial,
                                                          z: initial);

        public override Vector4 Add(Vector4 a) => Value += a;


        public override Vector4 Sub(Vector4 a) => Value -= a;


        public override Vector4 Mul(Vector4 a) => Value = new Vector4(x: Value.x * a.x,
                                                                      y: Value.y * a.y,
                                                                      z: Value.z * a.z);

        public override Vector4 Div(Vector4 a) => Value = new Vector4(x: Value.x / a.x,
                                                                      y: Value.y / a.y,
                                                                      z: Value.z / a.z);

        #endregion
        
        #region Implicits

        public static implicit operator Vector4(DeltaVec4 input) => input?.Value ?? Vector4.zero;

        #endregion
		
        #region Operators
		

        public static Vector4 operator +(DeltaVec4 a, DeltaVec4 b) => a.Value + b.Value;
        public static Vector4 operator -(DeltaVec4 a, DeltaVec4 b) => a.Value - b.Value;
        

        public static Vector4 operator *(DeltaVec4 a,
                                         DeltaVec4 b) => a.Value.GetMul(b.Value);


        public static Vector4 operator /(DeltaVec4 a,
                                         DeltaVec4 b) => a.Value.GetDiv(b.Value);
        
        public static Vector4 operator *(DeltaVec4 a,
                                         float b) => a.Value * b;


        public static Vector4 operator /(DeltaVec4 a,
                                         float b) => a.Value / b;


        #endregion

        public override void Lerp(float normal) => Value = Vector4.Lerp(a: Min,
                                                                        b: Max,
                                                                        t: normal);


        public override void Lerp(float outgoing,
                                  float incoming) => Value = Vector4.Lerp(a: Min,
                                                                          b: Max,
                                                                          t: incoming);

    }
}