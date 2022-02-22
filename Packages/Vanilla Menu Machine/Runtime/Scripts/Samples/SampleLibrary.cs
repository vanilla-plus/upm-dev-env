using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Arrangement;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary.Samples
{

    [Serializable]
    public class SampleLibrary : ArrangedLibrary2D<SampleCatalogue, SampleCatalogueItem, SampleLibraryItem, DirectPrefabPool<SampleLibraryItem>, Arrangement2DFlush<SampleLibraryItem>>
    {
        
        [ContextMenu("Fill")]
        private void FillPool() => _pool.CreateAll();


        [ContextMenu("Force Arrange")]
        private void ForceArrange() => Arrangement.InvokeArrangement();

        protected override void SubscribeToMonoSelection() => SampleLibraryItem.OnSelectedChange += HandleMonoSelection;

        public override async UniTask Construct()
        {
            _catalogue = SampleApp.catalogue;

            await base.Construct();
        }

    }

}