//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.TypeMenu;
//
//namespace Vanilla.DataAssets.Three
//{
//	[Serializable]
//    public class Test : MonoBehaviour, IInitiable
//    {
//	    
//	    [SerializeField]
//	    private bool _initialized;
//	    public bool Initialized
//	    {
//		    get => _initialized;
//		    set => _initialized = value;
//	    }
//
//	    public float debug;
//
//	    [SerializeReference]
//	    [TypeMenu]
//	    private FloatSource source;
//
//	    [ContextMenu("Get")]
//        public void Get() => debug = source.Get();
//        
//        [ContextMenu("Set")]
//        public void Set() => source?.Set(debug);
//
//
//        void OnValidate() => source?.Validate();
//
//        public async UniTask Initialize()
//        {
//	        Initialized = true;
//	        
//	        await source.Initialize();
//        }
//
//        public async UniTask DeInitialize() => await source.DeInitialize();
//
//    }
//    
//}
