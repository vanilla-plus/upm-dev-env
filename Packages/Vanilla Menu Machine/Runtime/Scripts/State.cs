#if DEBUG && MEDIA_LIBRARY
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

//    [Serializable]
//    public class State
//    {
//
//        [SerializeField] private SmartBool smartBool;
//
//        [SerializeField] private SmartFloat smartFloat;
//        public                   SmartBool SmartBool => smartBool;
//        public                   SmartFloat SmartFloat => smartFloat;
//        
//        public                   float  seconds = 1.0f;
//
//         private float _speed = 1.0f;
//
//        public State(bool startingState,
//                     float startingValue)
//        {
//            smartBool = new SmartBool(startingValue: startingState);
//
//            smartFloat = new SmartFloat(startingValue: startingValue);
//        }
//
//
//        internal void OnValidate() => _speed = 1.0f / seconds;
//
//
//        public void Init()
//        {
//            smartBool.onTrue += () => smartFloat.Fill(conditional: smartBool,
//                                                 targetCondition: true,
//                                                 speed: _speed);
//
//            smartBool.onFalse += () => smartFloat.Drain(conditional: smartBool,
//                                                   targetCondition: false,
//                                                   secondsToTake: _speed);
//        }
//
//
//    }

}