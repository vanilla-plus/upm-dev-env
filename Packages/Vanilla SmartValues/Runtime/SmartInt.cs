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
//    public class SmartInt : SmartStruct<int>
//    {
//
//	    #region Properties
//
//	    [SerializeField]
//	    protected int _Min = int.MinValue;
//	    public int Min
//	    {
//		    get => _Min;
//		    set
//		    {
//			    value = Mathf.Clamp(value: value,
//			                        min: int.MinValue,
//			                        max: _Max);
//
//			    _Min = value;
//
//			    if (Value < _Min)
//			    {
//				    Value = _Min;
//			    }
//		    }
//	    }
//	    
//	    [SerializeField]
//	    protected int _Max = int.MaxValue;
//	    public int Max
//	    {
//		    get => _Max;
//		    set
//		    {
//			    value = Mathf.Clamp(value: value,
//			                        min: _Min,
//			                        max: int.MaxValue);
//
//			    _Max = value;
//
//			    if (Value > _Max)
//			    {
//				    Value = _Max;
//			    }
//		    }
//	    }
//	    
//	    public override int Value
//	    {
//		    get => _Value;
//		    set
//		    {
//			    value = Mathf.Clamp(value,
//			                        Min,
//			                        Max);
//
//			    if (ValueEquals(_Value,
//			                    value)) return;
//
//			    var old = _Value;
//
//			    _Value = value;
//
//			    #if debug
//				Debug.Log($"[{Name}] changed from [{old}] to [{value}]");
//			    #endif
//
//			    if (old < value)
//			    {
//				    AtMin.Value = _Value == _Min; // Set AtMin
//
//				    OnValueChanged?.Invoke(_Value,
//				                           old);
//
//				    onIncrease?.Invoke(_Value,
//				                       old);
//
//				    AtMax.Value = _Value == _Max; // Set AtMax
//			    }
//			    else
//			    {
//				    AtMax.Value = _Value == _Max; // Set AtMax
//
//				    OnValueChanged?.Invoke(_Value,
//				                           old);
//
//				    onDecrease?.Invoke(_Value,
//				                       old);
//
//				    AtMin.Value = _Value == _Min; // Set AtMin
//			    }
//		    }
//	    }
//	    
//	    [SerializeField]
//	    public SmartBool AtMin = new("AtMin",
//	                                 false);
//
//	    [SerializeField]
//	    public SmartBool AtMax = new("AtMax",
//	                                 false);
//
//	    [NonSerialized]
//	    public Action<int, int> onDecrease;
//
//	    [NonSerialized]
//	    public Action<int, int> onIncrease;
//	    
//	    #endregion
//
//	    #region Overrides
//
//        public override bool ValueEquals(int a,
//                                         int b) => a == b;
//        
//        #endregion
//
//        #region Serialization
//
//
//        public override void OnBeforeSerialize() { }
//
//
//        public override void OnAfterDeserialize()
//        {
//	        ValidateMin();
//	        ValidateMax();
//
//	        ValidateValue();
//
//	        AtMin.Name = $"{Name}.AtMin";
//
//	        ValidateAtMin();
//
//	        AtMax.Name = $"{Name}.AtMax";
//
//	        ValidateAtMax();
//        }
//
//
//        private void ValidateMin() => _Min = Mathf.Clamp(value: _Min,
//                                                         min: int.MinValue,
//                                                         max: _Max);
//
//
//        private void ValidateMax() => _Max = Mathf.Clamp(value: _Max,
//                                                         min: _Min,
//                                                         max: int.MaxValue);
//
//
//        private void ValidateValue() => _Value = Mathf.Clamp(value: _Value,
//                                                             min: _Min,
//                                                             max: _Max);
//
//
//        private void ValidateAtMin() => AtMin._Value = _Value == _Min;
//
//
//        private void ValidateAtMax() => AtMax._Value = _Value == _Max;
//        
//
//
//
//        #endregion
//        
//        #region Construction
//
//
//
//        public SmartInt(string name) : base(name) { }
//
//
//        public SmartInt(string name,
//                        int defaultValue) : base(name,
//                                                   defaultValue) { }
//
//
//        public SmartInt(string name,
//                        int defaultValue,
//                        int defaultMin,
//                        int defaultMax) : base(name,
//                                                 defaultValue)
//        {
//	        Min = defaultMin;
//	        Max = defaultMax;
//        }
//        
//        #endregion
//        
//    }
//}
