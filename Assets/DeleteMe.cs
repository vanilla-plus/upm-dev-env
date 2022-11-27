using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla;

namespace MagicalProject
{
    public class DeleteMe : MonoBehaviour
    {

        public SmartFloat chapter1 = new SmartFloat(0.0f);
        public SmartFloat chapter2 = new SmartFloat(0.0f);
        public SmartFloat chapter3 = new SmartFloat(0.0f);


        void Start()
        {
            chapter1.AtMax.onTrue += () => chapter2.Fill(null);
            chapter2.AtMax.onTrue += () => chapter3.Fill(null);

            Play();
        }


        public void Play()
        {
            chapter1.Value = 0.0f;
            chapter2.Value = 0.0f;
            chapter3.Value = 0.0f;
            
            chapter1.Fill(null);
        }
        
    }
}
