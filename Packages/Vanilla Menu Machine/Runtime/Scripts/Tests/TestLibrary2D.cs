//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using Newtonsoft.Json.Linq;
//
//using UnityEngine;
//
//using Vanilla.Catalogue;
//using Vanilla.Catalogue.Samples;
//using Vanilla.Arrangement;
//using Vanilla.Pools;
//
//namespace Vanilla.MediaLibrary
//{
//
//    public static class TestApp
//    {
//
//        public static SampleCatalogue catalogue = new SampleCatalogue();
//        
//        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//        public static async UniTask Init() => await CatalogueBuilder.FetchViaRemoteConfig<SampleCatalogue, SampleCatalogueItem>(catalogue: catalogue,
//                                                                                                                                fallback: string.Empty);
//
//    }
//    
//    [Serializable]
//    public class TestLibrary2D : Library2D<SampleCatalogue, SampleCatalogueItem, TestLibraryItem2D, Arrangement2DHorizontal, IArrangementItem, Pool<TestLibraryItem2D>>
//    {
//
//        [ContextMenu("Fill")]
//        private void FillPool() => _pool.Fill();
//        
//        protected override void HandleNewCatalogue()
//        {
//            Catalogue = TestApp.catalogue;
//            
//            base.HandleNewCatalogue();
//        }
//
//    }
//
//}