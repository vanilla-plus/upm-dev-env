using System;

using UnityEngine;

namespace Vanilla.Easing
{
    public interface IEasingSlot
    {

        float Ease(float normal);

    }
    

    [Serializable]
    public class Power_In : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;

        public float Ease(float normal) => normal.InPower(power);

    }
    
    [Serializable]
    public class Power_Out : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;

        public float Ease(float normal) => normal.OutPower(power);

    }
    
    [Serializable]
    public class Power_In_Out : IEasingSlot
    {

        [SerializeField]
        public float power = 3.0f;
        
        public float Ease(float normal) => normal.InOutPower(power); 

    }

    [Serializable] public class Linear : IEasingSlot { public float Ease(float normal) => normal; }
    
    [Serializable] public class Circle_In : IEasingSlot { public float Ease(float normal) => normal.InCircle(); }
    
    [Serializable] public class Circle_Out : IEasingSlot { public float Ease(float normal) => normal.OutCircle(); }
    
    [Serializable] public class Circle_In_Out : IEasingSlot { public float Ease(float normal) => normal.InOutCircle(); }
    
    [Serializable] public class Sine_In : IEasingSlot { public float Ease(float normal) => normal.InSine(); }
    
    [Serializable] public class Sine_Out : IEasingSlot { public float Ease(float normal) => normal.OutSine(); }
    
    [Serializable] public class Sine_In_Out : IEasingSlot { public float Ease(float normal) => normal.InOutSine(); }
    
    [Serializable] public class Bounce : IEasingSlot 
    {
        [SerializeField]
        public int numberOfBounces = 3;
     
        public float Ease(float normal) => normal.Bounce(numberOfBounces); 
    }

    [Serializable] public class Elastic_In : IEasingSlot { public float Ease(float normal) => normal.InElastic(); }
    
    [Serializable] public class Elastic_Out : IEasingSlot { public float Ease(float normal) => normal.OutElastic(); }
    
    [Serializable] public class Elastic_In_Out : IEasingSlot { public float Ease(float normal) => normal.InOutElastic(); }
    
}