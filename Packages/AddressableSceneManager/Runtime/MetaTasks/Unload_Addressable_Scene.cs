#if unity_addressables && vanilla_metascript
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;

using SceneManager = Vanilla.AddressableSceneManager.AddressableSceneManager;

namespace Vanilla.MetaScript.Addressables
{
    
	[Serializable]
	public class Unload_Addressable_Scene : MetaTask
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
		
		
		protected override bool CanAutoName()
		{
			#if UNITY_EDITOR
			return assRef.editorAsset;
			#else
			return false;
			#endif
		}


		protected override string CreateAutoName()
		{
			#if UNITY_EDITOR
			return $"Unload scene [{assRef.editorAsset.name}]";
			#else
			return string.Empty;
			#endif
		}


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