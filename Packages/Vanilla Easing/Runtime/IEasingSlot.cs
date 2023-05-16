using System;

using UnityEngine;

namespace Vanilla.Easing
{
    public interface IEasingSlot
    {

        float Ease(float normal);

    }
    

    [Serializable]
    public class EasingSlot_Power_In : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;

        public float Ease(float normal) => normal.InPower(power);

    }
    
    [Serializable]
    public class EasingSlot_Power_Out : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;

        public float Ease(float normal) => normal.OutPower(power);

    }
    
    [Serializable]
    public class EasingSlot_Power_InOut : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;
        
        public float Ease(float normal) => normal.InOutPower(power); 

    }

    [Serializable] public class EasingSlot_Linear : IEasingSlot { public float Ease(float normal) => normal; }
    
    [Serializable] public class EasingSlot_Circle_In : IEasingSlot { public float Ease(float normal) => normal.InCircle(); }
    
    [Serializable] public class EasingSlot_Circle_Out : IEasingSlot { public float Ease(float normal) => normal.OutCircle(); }
    
    [Serializable] public class EasingSlot_Circle_InOut : IEasingSlot { public float Ease(float normal) => normal.InOutCircle(); }
    
    [Serializable] public class EasingSlot_Sine_In : IEasingSlot { public float Ease(float normal) => normal.InSine(); }
    
    [Serializable] public class EasingSlot_Sine_Out : IEasingSlot { public float Ease(float normal) => normal.OutSine(); }
    
    [Serializable] public class EasingSlot_Sine_InOut : IEasingSlot { public float Ease(float normal) => normal.InOutSine(); }
    
    [Serializable] public class EasingSlot_Bounce_In : IEasingSlot 
    {
        [SerializeField]
        public int numberOfBounces = 3;
     
        public float Ease(float normal) => normal.InBounce(numberOfBounces); 
    }
    
    [Serializable] public class EasingSlot_Bounce_Out : IEasingSlot 
    {
        [SerializeField]
        public int numberOfBounces = 3;
     
        public float Ease(float normal) => normal.OutBounce(numberOfBounces); 
    }
    
    [Serializable] public class EasingSlot_Bounce_InOut : IEasingSlot 
    {
        [SerializeField]
        public int numberOfBounces = 3;
     
        public float Ease(float normal) => normal.InOutBounce(numberOfBounces); 
    }
    
    [Serializable] public class EasingSlot_Elastic_In : IEasingSlot { public float Ease(float normal) => normal.InElastic(); }
    
    [Serializable] public class EasingSlot_Elastic_Out : IEasingSlot { public float Ease(float normal) => normal.OutElastic(); }
    
    [Serializable] public class EasingSlot_Elastic_InOut : IEasingSlot { public float Ease(float normal) => normal.InOutElastic(); }
    
}
