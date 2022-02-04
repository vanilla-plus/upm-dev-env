//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//using Vanilla.DataAssets;
//
//namespace MagicalProject
//{
//    public class SocketTest : MonoBehaviour
//    {
//
//        public ValueSocket<float, FloatAsset> floatSocket;
//
//        public float thisIsSoStupid = 5.0f;
//        
////        private void OnValidate() => floatSocket.Validate();
//
//
//        void OnEnable()
//        {
////            floatSocket.OnEnable();
//            
//            floatSocket.Broadcast += Yo;
//        }
//
//
//        public void Yo(float newThing) => Debug.Log(newThing);
//
//
//        private void OnDisable()
//        {
////            floatSocket.OnDisable();
//
//            floatSocket.Broadcast -= Yo;
//        }
//
//
//        void Update()
//        {
//            floatSocket.Set(thisIsSoStupid);
//        }
//        
//    }
//}
