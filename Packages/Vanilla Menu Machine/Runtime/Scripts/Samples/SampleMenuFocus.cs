using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Vanilla.MediaLibrary.Samples
{
    public class SampleMenuFocus : MonoBehaviour
    {

        public SampleLibrary library;

        public RectTransform _rect;
        
        public RectTransform targetRect;

        void Awake()
        {
            _rect = (RectTransform) transform;
            
            
        }

        void LateUpdate()
        {
            
        }

    }
}
