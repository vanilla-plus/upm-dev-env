using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla;

namespace MagicalProject
{
    public class DeleteMe : MonoBehaviour
    {

        public Normal chapter1 = new Normal(0.0f);
        public Normal chapter2 = new Normal(0.0f);
        public Normal chapter3 = new Normal(0.0f);


        void Start()
        {
            chapter1.Full.onTrue += () => chapter2.Fill(null);
            chapter2.Full.onTrue += () => chapter3.Fill(null);

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
