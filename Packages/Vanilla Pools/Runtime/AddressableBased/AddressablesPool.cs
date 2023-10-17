//#if ADDRESSABLES
//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//
//namespace Vanilla.Pools
//{
//
//    [Serializable]
//    public abstract class AddressablesPool<PI> : Pool<PI>
//        where PI : PoolItem
//    {
//
//        [SerializeField] public AssetReferenceGameObject prefabReference;
//
//
//        public override async UniTask<PI> CreateNewItem()
//        {
//            // Is this asset itself (the prefab) already loaded?
//            if (!prefabReference.IsValid())
//            {
//                await LoadPrefabAsset();
//            }
//
//            // This makes a special instance which is tracked internal to Addressables.
//            var output = await prefabReference.InstantiateAsync(parent: _inactiveParent,
//                                                                instantiateInWorldSpace: false).Task;
//
//            return output.GetComponentInChildren<PI>(includeInactive: true);
//        }
//
//
//        public override void DestroyItem(PI item)
//        {
//            if (_active.Contains(item: item)) _active.Remove(item: item);
//            if (_inactive.Contains(item: item)) _inactive.Remove(item: item);
//
//            prefabReference.ReleaseInstance(obj: item.gameObject);
//        }
//
//
//        public async UniTask LoadPrefabAsset()
//        {
//            // Can Addressables find the associated asset?
//            if (prefabReference.RuntimeKeyIsValid())
//            {
//                await prefabReference.LoadAssetAsync();
//            }
//            else
//            {
//                Debug.LogError(message: "The runtime key associated iwth prefabReference is invalid. This most likely means the asset was unreachable or not in the expected location.");
//            }
//        }
//
//        public void OnDestroy()
//        {
//            // Clean up any instances and loaded assets.
//
//            while (Inactive.Count > 0) DestroyItem(item: Inactive[index: 0]);
//            while (Active.Count   > 0) DestroyItem(item: Active[index: 0]);
//
//            UnloadPrefabAsset();
//        }
//
//        public void UnloadPrefabAsset() => prefabReference.ReleaseAsset();
//
//    }
//
//}
//#endif