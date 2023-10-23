#if unity_addressables && vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;

using SceneManager = Vanilla.AddressableSceneManager.AddressableSceneManager;

namespace Vanilla.MetaScript.Addressables
{
    
	[Serializable]
	public class UnloadAddressableScene : MetaTask
	{

		[SerializeField]
		public AssetReference assRef;

		public override void OnValidate()
		{
			#if UNITY_EDITOR
			if (assRef.editorAsset != null) Debug.Log(assRef.editorAsset.GetType());

			base.OnValidate();
			#endif
		}
		
		
		protected override bool CanAutoName() => assRef.editorAsset;


		protected override string CreateAutoName() => $"Unload scene [{assRef.editorAsset.name}]";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var operation = SceneManager.TryUnloadSceneInstance(assRef).AsUniTask();

			while (operation.Status == UniTaskStatus.Pending)
			{
				if (scope.Cancelled) return scope;
//				if (tracer.HasBeenCancelled(this)) return tracer;

				await UniTask.Yield();
			}

			return scope;
		}

	}
}
#endif