//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.Arrangement;
//using Vanilla.Pools;
//
//namespace Vanilla.MediaLibrary.Samples
//{
//
//    [Serializable]
//    public class SampleLibrary : ArrangedLibrary2D<SampleCatalogue, 
//        SampleCatalogueItem, 
//        SampleLibraryItem, 
//        IPrefabStockAsyncPool<,>, 
//        Arrangement2DFlush<SampleLibraryItem>>
//    {
//        
//        [ContextMenu(itemName: "Fill")]
//        private void FillPool() => _pool.CreateAll();
//        
////        protected override void SubscribeToMonoSelection() => SampleLibraryItem.OnSelectedChange += HandleMonoSelection;
//
//        public override async UniTask Construct()
//        {
//            _catalogue = SampleApp.catalogue;
//
////            Arrangement.ForceArrangement = true;
//            
////            Arrangement.InvokeArrangement();
//
//            await base.Construct();
//        }
//
//    }
//
//}