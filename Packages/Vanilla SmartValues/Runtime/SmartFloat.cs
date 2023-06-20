//#if UNITY_EDITOR || DEVELOPMENT_BUILD
//#define debug
//#endif
//
//using System;
//
//using UnityEngine;
//
//namespace Vanilla.SmartValues
//{
//
//	[Serializable]
//	public class SmartFloat : SmartStruct<float>
//	{
//
//		#region Properties
//
//
//
//		[SerializeField]
//		public float ChangeEpsilon = Mathf.Epsilon;
//		
//		[SerializeField]
//		public float MinMaxEpsilon = 0.01f;
//
//		[SerializeField]
//		protected float _Min = float.MinValue;
//		public float Min
//		{
//			get => _Min;
//			set
//			{
//				value = Mathf.Clamp(value: value,
//				                    min: float.MinValue,
//				                    max: _Max);
//
//				_Min = value;
//
//				if (Value < _Min)
//				{
//					Value = _Min;
//				}
//			}
//		}
//
//		[SerializeField]
//		protected float _Max = float.MaxValue;
//		public float Max
//		{
//			get => _Max;
//			set
//			{
//				value = Mathf.Clamp(value: value,
//				                    min: _Min,
//				                    max: float.MaxValue);
//
//				_Max = value;
//
//				if (Value > _Max)
//				{
//					Value = _Max;
//				}
//			}
//		}
//
//		public override float Value
//		{
//			get => _Value;
//			set
//			{
//				value = Mathf.Clamp(value,
//				                    Min,
//				                    Max);
//
//				if (ValueEquals(_Value,
//				                value)) return;
//
//				var old = _Value;
//
//				_Value = value;
//
//				#if debug
//				Debug.Log($"[{Name}] changed from [{old}] to [{value}]");
//				#endif
//
//				if (old < value)
//				{
//					AtMin.Value = Mathf.Abs(Value - _Min) <= MinMaxEpsilon; // Set AtMin
//
//					OnValueChanged?.Invoke(_Value,
//					                       old);
//
//					onIncrease?.Invoke(_Value,
//					                   old);
//
//					AtMax.Value = Mathf.Abs(Value - _Max) <= MinMaxEpsilon; // Set AtMax
//				}
//				else
//				{
//					AtMax.Value = Mathf.Abs(Value - _Max) <= MinMaxEpsilon; // Set AtMax
//
//					OnValueChanged?.Invoke(_Value,
//					                       old);
//
//					onDecrease?.Invoke(_Value,
//					                   old);
//
//					AtMin.Value = Mathf.Abs(Value - _Min) <= MinMaxEpsilon; // Set AtMin
//				}
//			}
//		}
//
//		[SerializeField]
//		public SmartBool AtMin = new("AtMin",
//		                             false);
//
//		[SerializeField]
//		public SmartBool AtMax = new("AtMax",
//		                             false);
//
//		[NonSerialized]
//		public Action<float, float> onDecrease;
//
//		[NonSerialized]
//		public Action<float, float> onIncrease;
//
//
//
//		#endregion
//
//		#region Overrides
//
//		public override bool ValueEquals(float a,
//		                                 float b) => Mathf.Abs(a - b) <= ChangeEpsilon;
//
//
//
//		#endregion
//
//		#region Serialization
//
//
//
//		public override void OnBeforeSerialize() { }
//
//
//		public override void OnAfterDeserialize()
//		{
//			ValidateMin();
//			ValidateMax();
//
//			ValidateValue();
//
//			AtMin.Name = $"{Name}.AtMin";
//
//			ValidateAtMin();
//
//			AtMax.Name = $"{Name}.AtMax";
//
//			ValidateAtMax();
//		}
//
//
//		private void ValidateMin() => _Min = Mathf.Clamp(value: _Min,
//		                                                 min: float.MinValue,
//		                                                 max: _Max);
//
//
//		private void ValidateMax() => _Max = Mathf.Clamp(value: _Max,
//		                                                 min: _Min,
//		                                                 max: float.MaxValue);
//
//
//		private void ValidateValue() => _Value = Mathf.Clamp(value: _Value,
//		                                                     min: _Min,
//		                                                     max: _Max);
//
//
//		private void ValidateAtMin() => AtMin._Value = Mathf.Abs(_Value - _Min) <= MinMaxEpsilon;
//
//
//		private void ValidateAtMax() => AtMax._Value = Mathf.Abs(_Value - _Max) <= MinMaxEpsilon;
//
//
//
//		#endregion
//
//		#region Construction
//
//
//
//		public SmartFloat(string name) : base(name) { }
//
//
//		public SmartFloat(string name,
//		                  float defaultValue) : base(name,
//		                                             defaultValue) { }
//
//
//		public SmartFloat(string name,
//		                  float defaultValue,
//		                  float defaultMin,
//		                  float defaultMax) : base(name,
//		                                           defaultValue)
//		{
//			Min = defaultMin;
//			Max = defaultMax;
//		}
//
//
//		public SmartFloat(string name,
//		                  float defaultValue,
//		                  float defaultChangeEpsilon) : base(name,
//		                                               defaultValue) => ChangeEpsilon = defaultChangeEpsilon;
//
//
//		public SmartFloat(string name,
//		                  float defaultValue,
//		                  float defaultMin,
//		                  float defaultMax,
//		                  float defaultChangeEpsilon) : base(name,
//		                                               defaultValue)
//		{
//			Min     = defaultMin;
//			Max     = defaultMax;
//			ChangeEpsilon = defaultChangeEpsilon;
//		}
//
//
//
//		#endregion
//
//	}
//
//}