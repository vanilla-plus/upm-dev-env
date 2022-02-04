//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.DataAssets;
//
//using Vanilla.UnityExtensions;
//
//namespace MagicalProject
//{
//    [Serializable]
//    public class Test3 : MonoBehaviour
//    {
//
//        [SerializeField]
//        public ValueSocket<float, FloatAsset> floatSocket = new ValueSocket<float, FloatAsset>();
//
//        [SerializeField]
//        public ValueSocket<int, IntAsset> intSocket = new ValueSocket<int, IntAsset>();
//        
//        [SerializeField]
//        public ValueSocket<bool, BoolAsset> boolSocket = new ValueSocket<bool, BoolAsset>();
//
//        [SerializeField]
//        public RefSocket<GameObject, GameObjectAsset> gameObjectSocket = new RefSocket<GameObject, GameObjectAsset>();
//
//        [SerializeField]
//        public RefSocket<MonoBehaviour, MonoBehaviourAsset> monoBehaviourSocket = new RefSocket<MonoBehaviour, MonoBehaviourAsset>();
//        
//        [SerializeField]
//        public StructSocket<Vector3, Vector3Asset> vector3Socket = new StructSocket<Vector3, Vector3Asset>();
//        
//        public float Float;
//        public int   Int;
//        public bool  Bool;
//
//        public GameObject GameObjectTarget;
//
//        public MonoBehaviour monoBehaviour;
//
//        public Vector3 vector3;
//
//        public bool getMode;
//
//       
//        [ContextMenu(itemName: "Do It")]
//        public async UniTask RunTheTrap()
//        {
//            if (getMode)
//            {
//                Float            = floatSocket.Get();
//                Int              = intSocket.Get();
//                Bool             = boolSocket.Get();
//                GameObjectTarget = gameObjectSocket.Get();
//                monoBehaviour    = monoBehaviourSocket.Get();
//                vector3          = vector3Socket.Get();
//            }
//            else
//            {
//                floatSocket.Set(value: Float);
//                intSocket.Set(value: Int);
//                boolSocket.Set(value: Bool);                
//                gameObjectSocket.Set(value: GameObjectTarget);
//                monoBehaviourSocket.Set(value: this);
//                vector3Socket.Set(value: vector3);
//            }
//        }
//
//
//        [ContextMenu(itemName: "Toggle MonoBehaviour from Socket")]
//        public void ToggleMonobehaviour() => monoBehaviourSocket.Get().ToggleEnabled();
//
//
//        void Update()
//        {
//            
//        }
//        
//    }
//}