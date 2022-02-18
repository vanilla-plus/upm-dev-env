using System;

using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Vanilla.Pools
{

	[Serializable]
	public abstract class DirectPrefabPool<PI> : Pool<PI>
		where PI : PoolItem
	{

		[SerializeField] protected GameObject _prefab;
		public                     GameObject Prefab => _prefab;

		#if !UNITY_EDITOR

		#else
		public override UniTask<PI> CreateNewItem()
		{
			#if UNITY_EDITOR
			var output = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: _prefab,
			                                             parent: _inactiveParent) as GameObject;
			#else
			var output = UnityEngine.Object.Instantiate(original: _prefab,
			                                            parent: _inactiveParent);
			#endif

			return new UniTask<PI>(result: output.GetComponentInChildren<PI>(includeInactive: true));
		}
		#endif


		public override void DestroyItem(PI item) { }

	}

}