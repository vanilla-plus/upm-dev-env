using System;

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DataAssets
{

    [Serializable]
    public class ProtectedRangedColorSource : ColorSource,
                                             IProtectedSource<Color>,
                                             IRangedDataSource<Color>
    {
	    
	    [SerializeField]
	    private string _name = "Unnamed ProtectedRangedColorSource";
	    public string Name
	    {
		    get => _name;
		    set => _name = value;
	    }





	    [SerializeField]
        private Color _value;
        public override Color Value
        {
            get => _value;
            set
            {
	            value = new Color(r: Mathf.Clamp(value: value.r,
	                                             min: _min.r,
	                                             max: _max.r),
	                                g: Mathf.Clamp(value: value.g,
	                                               min: _min.g,
	                                               max: _max.g),
	                                b: Mathf.Clamp(value: value.b,
	                                               min: _min.b,
	                                               max: _max.b),
	                                a: Mathf.Clamp(value: value.a,
	                                               min: _min.a,
	                                               max: _max.a));

                if (_value == value) return;

                var outgoing = _value;

                _value = value;

                #if debug
                Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
                #endif
                
                // Hm, we don't check for an 'increase' or 'decrease' here because I suppose it's a little bit vague when it comes to Vectors?
                // The way more useful information here would be if each particular dimension had its own Min/Max/AtMin/AtMax but structuring that
                // would be hell...?
                
                // I think we can flub this check for now.

                OnSet?.Invoke(value);
                OnSetWithHistory?.Invoke(arg1: value,
                                         arg2: outgoing);
                
                _atMin.Value = VectorIsAtMin;
                _atMax.Value = VectorIsAtMax;
            }
        }

        [SerializeField]
		private Color _min = new Color(r: float.MinValue,
		                               g: float.MinValue,
		                               b: float.MinValue,
		                                 a: float.MinValue);
		public Color Min
		{
			get => _min;
			set => _min = value;
		}

		[SerializeField]
		private Color _max = new Color(r: float.MaxValue,
		                                   g: float.MaxValue,
		                                   b: float.MaxValue,
		                                   a: float.MaxValue);
		public Color Max
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
			_min = new Color(r: Mathf.Clamp(value: _min.r,
			                                min: float.MinValue,
			                                max: _max.r),
			                   g: Mathf.Clamp(value: _min.g,
			                                  min: float.MinValue,
			                                  max: _max.g),
			                   b: Mathf.Clamp(value: _min.b,
			                                  min: float.MinValue,
			                                  max: _max.b),
			                   a: Mathf.Clamp(value: _min.a,
			                                  min: float.MinValue,
			                                  max: _max.a));

			_max = new Color(r: Mathf.Clamp(value: _max.r,
			                                min: _min.r,
			                                max: float.MaxValue),
			                   g: Mathf.Clamp(value: _max.g,
			                                  min: _min.g,
			                                  max: float.MaxValue),
			                   b: Mathf.Clamp(value: _max.b,
			                                  min: _min.b,
			                                  max: float.MaxValue),
			                   a: Mathf.Clamp(value: _max.a,
			                                  min: _min.a,
			                                  max: float.MaxValue));

			AtMin.Name = $"{Name}.AtMin";
			AtMax.Name = $"{Name}.AtMax";

			Value = _value;

//			var old = _value;

//			Value = new Color(r: Mathf.Clamp(value: _value.r,
//			                                  min: _min.r,
//			                                  max: _max.r),
//			                   g: Mathf.Clamp(value: _value.g,
//			                                  min: _min.g,
//			                                  max: _max.g),
//			                   b: Mathf.Clamp(value: _value.b,
//			                                  min: _min.b,
//			                                  max: _max.b),
//			                   a: Mathf.Clamp(value: _value.a,
//			                                  min: _min.a,
//			                                  max: _max.a));
		}


		private bool VectorIsAtMin => Mathf.Abs(_value.r - _min.r) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.g - _min.g) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.b - _min.b) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.a - _min.a) < MinMaxEpsilon;
		private bool VectorIsAtMax => Mathf.Abs(_value.r - _max.r) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.g - _max.g) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.b - _max.b) < MinMaxEpsilon && 
		                              Mathf.Abs(_value.a - _max.a) < MinMaxEpsilon;
    }

}