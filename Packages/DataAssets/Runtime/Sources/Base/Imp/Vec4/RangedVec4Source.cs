using System;

using UnityEngine;

namespace Vanilla.DataSources
{

	[Serializable]
	public class RangedVec4Source : Vec4Source,
	                                IRangedDataSource<Vector4>
	{

		[SerializeField]
		private Vector4 _value;
		public override Vector4 Value
		{
			get => _value;
			set
			{
				value = new Vector4(x: Mathf.Clamp(value: value.x,
				                                   min: _min.x,
				                                   max: _max.x),
				                    y: Mathf.Clamp(value: value.y,
				                                   min: _min.y,
				                                   max: _max.y),
				                    z: Mathf.Clamp(value: value.z,
				                                   min: _min.z,
				                                   max: _max.z),
				                    w: Mathf.Clamp(value: value.w,
				                                   min: _min.w,
				                                   max: _max.w));

				var old = _value;

				_value = value;

				// Hm, we don't check for an 'increase' or 'decrease' here because I suppose it's a little bit vague when it comes to Vectors?
				// The way more useful information here would be if each particular dimension had its own Min/Max/AtMin/AtMax but structuring that
				// would be hell...?

				// I think we can flub this check for now.

				OnSet?.Invoke(_value);

				OnSetWithHistory?.Invoke(arg1: _value,
				                         arg2: old);

				_atMin.Value = VectorIsAtMin;
				_atMax.Value = VectorIsAtMax;
			}
		}

		[SerializeField]
		private Vector4 _min = new Vector4(x: float.MinValue,
		                                   y: float.MinValue,
		                                   z: float.MinValue,
		                                   w: float.MinValue);
		public Vector4 Min
		{
			get => _min;
			set => _min = value;
		}

		[SerializeField]
		private Vector4 _max = new Vector4(x: float.MaxValue,
		                                   y: float.MaxValue,
		                                   z: float.MaxValue,
		                                   w: float.MaxValue);
		public Vector4 Max
		{
			get => _max;
			set => _max = value;
		}

		[SerializeField]
		private ProtectedBoolSource _atMin = new ProtectedBoolSource();
		public ProtectedBoolSource AtMin
		{
			get => _atMin;
			set => _atMin = value;
		}

		[SerializeField]
		private ProtectedBoolSource _atMax = new ProtectedBoolSource();
		public ProtectedBoolSource AtMax
		{
			get => _atMax;
			set => _atMax = value;
		}

		[SerializeField]
		private float _minMaxEpsilon = 0.0001f;
		public float MinMaxEpsilon
		{
			get => _minMaxEpsilon;
			set => _minMaxEpsilon = value;
		}

		public override void OnBeforeSerialize() { }


		public override void OnAfterDeserialize()
		{
			_min = new Vector4(x: Mathf.Clamp(value: _min.x,
			                                  min: float.MinValue,
			                                  max: _max.x),
			                   y: Mathf.Clamp(value: _min.y,
			                                  min: float.MinValue,
			                                  max: _max.y),
			                   z: Mathf.Clamp(value: _min.z,
			                                  min: float.MinValue,
			                                  max: _max.z),
			                   w: Mathf.Clamp(value: _min.w,
			                                  min: float.MinValue,
			                                  max: _max.w));

			_max = new Vector4(x: Mathf.Clamp(value: _max.x,
			                                  min: _min.x,
			                                  max: float.MaxValue),
			                   y: Mathf.Clamp(value: _max.y,
			                                  min: _min.y,
			                                  max: float.MaxValue),
			                   z: Mathf.Clamp(value: _max.z,
			                                  min: _min.z,
			                                  max: float.MaxValue),
			                   w: Mathf.Clamp(value: _max.w,
			                                  min: _min.w,
			                                  max: float.MaxValue));

//			AtMin.Name = $"{Name}.AtMin";
//			AtMax.Name = $"{Name}.AtMax";
			
			Value = new Vector4(x: Mathf.Clamp(value: _value.x,
			                                    min: _min.x,
			                                    max: _max.x),
			                     y: Mathf.Clamp(value: _value.y,
			                                    min: _min.y,
			                                    max: _max.y),
			                     z: Mathf.Clamp(value: _value.z,
			                                    min: _min.z,
			                                    max: _max.z),
			                     w: Mathf.Clamp(value: _value.w,
			                                    min: _min.w,
			                                    max: _max.w));
		}


		private bool VectorIsAtMin => Mathf.Abs(_value.x - _min.x) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.y - _min.y) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.z - _min.z) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.w - _min.w) < MinMaxEpsilon;
		private bool VectorIsAtMax => Mathf.Abs(_value.x - _max.x) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.y - _max.y) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.z - _max.z) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.w - _max.w) < MinMaxEpsilon;

	}

}