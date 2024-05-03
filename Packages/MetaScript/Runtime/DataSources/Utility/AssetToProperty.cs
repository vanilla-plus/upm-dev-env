//using System;
//
//using System.Reflection;
//
//using UnityEngine;
//
//using Object = UnityEngine.Object;
//
//namespace Vanilla.MetaScript
//{
//    
//    [Serializable]
//    public abstract class AssetToProperty : MonoBehaviour
//    {
//
//        [SerializeField]
//        public Object target;
//        
//        [SerializeField]
//        public string propertyName;
//
//        [SerializeField]
//        private PropertyInfo propertyInfo = null;
//
//        [SerializeField]
//        public bool propertyInfoIsNull;
//
//        void OnValidate()
//        {
//            #if UNITY_EDITOR
//            if (target == null) return;
//            
//            if (!string.IsNullOrEmpty(propertyName)) propertyInfo = target.GetType().GetProperty(propertyName);
//
//            propertyInfoIsNull = propertyInfo == null;
//            #endif
//        }
//
//
//        void TrySet()
//        {
//            propertyInfo.SetValue(target, newValue);
//        }
//        
//    }
//}
