//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.MetaScript.DataSources;
//using Vanilla.TypeMenu;
//
//using Object = UnityEngine.Object;
//
//namespace Vanilla.MetaScript
//{
//    
//    [Serializable]
//    public abstract class Source_To_Property<T> : MetaTask
//    {
//
//        [SerializeField]
//        public Object target;
//        
//        [SerializeField]
//        public string propertyName;
//        
//        public abstract IDataSource<T> Source
//        {
//            get;
//        }
//
//        protected override bool Valid => Source != null && target.GetType().GetProperty(propertyName) != null;
//
//        protected override string CreateAutoName() => $"Set [{target.GetType().GetProperty(propertyName)}] to [{Source}]";
//
//
//        protected void PerformSet()
//        {
//            if (_valid)
//            {
//                target.GetType()
//                      .GetProperty(propertyName)
//                      ?.SetValue(target,
//                                 Source.Value);
//            }
//            else
//            {
//                Debug.LogWarning("No set undertaken since this task is valid.");
//            }
//        }
//
//
//        protected override UniTask<Scope> _Run(Scope scope)
//        {
//            PerformSet();
//            
//            return UniTask.FromResult(scope);
//        }
//
//    }
//}
