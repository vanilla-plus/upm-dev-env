using System;

using UnityEngine;

namespace Vanilla
{
    [Serializable]
    public class SmartSin : SmartFloat
    {

        public SmartSin(float startingValue) : base(startingValue) => _value = startingValue;

        public float Offset = 0.5f;
        
        public float Speed = 2.0f;
        
        public float Scale = 0.5f;
        
        public float t = 0.0f;
        
        public void Update(float deltaTime)
        {
            t = (t + deltaTime);
            
            Value = Offset + Mathf.Sin(t * Speed) * Scale;
        }


        public override void OnValidate()
        {
            base.OnValidate();

            MinMaxEpsilon = Mathf.Clamp(value: MinMaxEpsilon,
                                  min: Mathf.Epsilon,
                                  max: Scale);
        }

    }
}
