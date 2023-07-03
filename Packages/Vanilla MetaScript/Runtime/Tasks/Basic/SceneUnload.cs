//using System;
//using System.Threading;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine.AddressableAssets;
//
//using Vanilla.MetaScript;
//
//[Serializable]
//public class SceneUnload : MetaTask
//{
//
//	public AssetReference sceneReference;
//
//	#if UNITY_EDITOR
//		protected override bool   CanDescribe()  => sceneReference.editorAsset != null;
//
//		protected override string DescribeTask() => $"Unload [{sceneReference.editorAsset.name}] scene";
//	#else
//		protected override bool CanDescribe() => false;
//		protected override string DescribeTask() => string.Empty;
//	#endif
//
//	protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource) => await ContentSceneInstanceManager.TryUnloadSceneInstance(sceneReference);
//
//}