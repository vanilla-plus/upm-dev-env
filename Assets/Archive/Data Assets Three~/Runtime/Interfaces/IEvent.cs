using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.DataAssets.Three
{

    public interface IEvent
    {

        

    }
    
    public interface TypeEvent<TType> : IEvent
    {

        public UnityEvent<TType> Event
        {
            get;
//            set;
        }

        void Invoke(TType a);
        
    }
    
}