using System;
using UnityEngine;

using Vanilla.UnityExtensions;

namespace Vanilla.DeltaValues
{
    [Serializable]
    public class DeltaVec3 : DeltaVec<Vector3>
    {
        
        #region Constructors

        public DeltaVec3() : base() { }

        public DeltaVec3(string defaultName) : base(defaultName) { }

        public DeltaVec3(string defaultName, Vector3 defaultValue) : base(defaultName: defaultName, defaultValue: defaultValue) { }

        public DeltaVec3(string defaultName, Vector3 defaultValue, float defaultChangeEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultChangeEpsilon: defaultChangeEpsilon) { }

        public DeltaVec3(string defaultName, Vector3 defaultValue, Vector3 defaultMin, Vector3 defaultMax) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax) { }

        public DeltaVec3(string defaultName, Vector3 defaultValue, Vector3 defaultMin, Vector3 defaultMax, float changeEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon) { }

        public DeltaVec3(string defaultName, Vector3 defaultValue, Vector3 defaultMin, Vector3 defaultMax, float changeEpsilon, float minMaxEpsilon) 
            : base(defaultName: defaultName, defaultValue: defaultValue, defaultMin: defaultMin, defaultMax: defaultMax, changeEpsilon: changeEpsilon, minMaxEpsilon: minMaxEpsilon) { }

        #endregion
        
        public override bool ValueEquals(Vector3 a, Vector3 b) => a == b;


        public override         Vector3 TypeMinValue => new Vector3(x: float.MinValue, y: float.MinValue, z: float.MinValue);

        public override Vector3 TypeMaxValue => new Vector3(x: float.MaxValue, y: float.MaxValue, z: float.MaxValue);

        public override Vector3 GetClamped(Vector3 input, Vector3 min, Vector3 max) 
            => new Vector3(
                x: Mathf.Clamp(value: input.x, min: min.x, max: max.x),
                y: Mathf.Clamp(value: input.y, min: min.y, max: max.y),
                z: Mathf.Clamp(value: input.z, min: min.z, max: max.z));

        public override Vector3 GetLesser(Vector3 input, Vector3 other) 
            => new Vector3(
                x: Math.Min(val1: input.x, val2: other.x),
                y: Math.Min(val1: input.y, val2: other.y),
                z: Math.Min(val1: input.z, val2: other.z));

        public override Vector3 GetGreater(Vector3 input, Vector3 other) 
            => new Vector3(
                x: Math.Max(val1: input.x, val2: other.x),
                y: Math.Max(val1: input.y, val2: other.y),
                z: Math.Max(val1: input.z, val2: other.z));

        public override bool LessThan(Vector3 a, Vector3 b) => a.x < b.x && a.y < b.y && a.z < b.z;

        public override bool GreaterThan(Vector3 a, Vector3 b) => a.x > b.x && a.y > b.y && a.z > b.z;

        public override bool ValueAtMin() => 
               Mathf.Abs(_Value.x - _Min.x) < MinMaxEpsilon 
            && Mathf.Abs(_Value.y - _Min.y) < MinMaxEpsilon 
            && Mathf.Abs(_Value.z - _Min.z) < MinMaxEpsilon;

        public override bool ValueAtMax() => 
               Mathf.Abs(_Value.x - _Max.x) < MinMaxEpsilon 
            && Mathf.Abs(_Value.y - _Max.y) < MinMaxEpsilon 
            && Mathf.Abs(_Value.z - _Max.z) < MinMaxEpsilon;



        #region Math



        public override Vector3 New(float initial) => new(x: initial,
                                                          y: initial,
                                                          z: initial);

        public override Vector3 Add(Vector3 a) => Value += a;


        public override Vector3 Sub(Vector3 a) => Value -= a;


        public override Vector3 Mul(Vector3 a) => Value = new Vector3(x: Value.x * a.x,
                                                                      y: Value.y * a.y,
                                                                      z: Value.z * a.z);

        public override Vector3 Div(Vector3 a) => Value = new Vector3(x: Value.x / a.x,
                                                                      y: Value.y / a.y,
                                                                      z: Value.z / a.z);

        #endregion
        
        #region Implicits

        public static implicit operator Vector3(DeltaVec3 input) => input?.Value ?? Vector3.zero;

        #endregion
		
        #region Operators
		

        public static Vector3 operator +(DeltaVec3 a, DeltaVec3 b) => a.Value + b.Value;
        public static Vector3 operator -(DeltaVec3 a, DeltaVec3 b) => a.Value - b.Value;
        

        public static Vector3 operator *(DeltaVec3 a,
                                         DeltaVec3 b) => a.Value.GetMul(b.Value);


        public static Vector3 operator /(DeltaVec3 a,
                                         DeltaVec3 b) => a.Value.GetDiv(b.Value);
        
        public static Vector3 operator *(DeltaVec3 a,
                                         float b) => a.Value * b;


        public static Vector3 operator /(DeltaVec3 a,
                                         float b) => a.Value / b;


        #endregion

        public override void Lerp(float normal) => Value = Vector3.Lerp(a: Min,
                                                                        b: Max,
                                                                        t: normal);


        public override void Lerp(float outgoing,
                                  float incoming) => Value = Vector3.Lerp(a: Min,
                                                                          b: Max,
                                                                          t: incoming);

    }
}