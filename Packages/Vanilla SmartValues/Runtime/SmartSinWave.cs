using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.SmartValues
{
	
	[Serializable]
    public class SmartSinWave : SmartFloat
    {

	    [SerializeField]
	    public float Frequency = 1.0f;

	    [Range(0,1.0f)]
	    [SerializeField]
	    private float _Time = 0.0f;
	    public float Time
	    {
		    get => _Time;
		    set
		    {
			    _Time = Mathf.Repeat(value, 1.0f);

			    Value = Amplitude * (float) Math.Sin(2 * Math.PI * _Time) + VerticalShift;
		    }
	    }

	    [SerializeField]
	    private float Amplitude;

	    [SerializeField]
	    private float VerticalShift;
	    
	    public override void OnAfterDeserialize()
	    {
		    base.OnAfterDeserialize();
		    
		    Amplitude     = (Max - Min) / 2.0f;
		    VerticalShift = (Max + Min) / 2.0f;
	    }


	    public void Update(float deltaTime) => Time += deltaTime * Frequency;

	    
	    #region Construction
	    
//        public SmartSinWave(string name) : base(name) { }
//
//
//        public SmartSinWave(string name,
//                 float defaultValue) : base(name,
//                                            defaultValue) { }
//
//
//        public SmartSinWave(string name,
//                 float defaultValue,
//                 float defaultMin,
//                 float defaultMax) : base(name,
//                                          defaultValue,
//                                          defaultMin,
//                                          defaultMax) { }
//
//        public SmartSinWave(string name,
//                            float defaultValue,
//                            float defaultChangeEpsilon) : base(name,
//                                                         defaultValue,
//                                                         defaultChangeEpsilon) { }
//
        public SmartSinWave(string name,
                 float defaultValue,
                 float defaultMin,
                 float defaultMax,
                 float defaultChangeEpsilon) : base(name,
                                              defaultValue,
                                              defaultMin,
                                              defaultMax,
                                              defaultChangeEpsilon) { }




        #endregion

    }
}
