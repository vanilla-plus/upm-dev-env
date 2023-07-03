//using System;
//using System.Threading;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.SceneManagement;
//
//using Vanilla.MetaScript;
//
//[Serializable]
//public class SceneLoad : MetaTask
//{
//
//	[SerializeField]
//	public AssetReference sceneReference;
//	
//	[SerializeField]
//	public LoadSceneMode loadMode = LoadSceneMode.Additive;
//
//	[SerializeField]
//	public bool activateOnLoad = true;
//
//	#if UNITY_EDITOR
//		protected override bool   CanDescribe()  => sceneReference.editorAsset != null;
//
//		protected override string DescribeTask() => $"Load [{sceneReference.editorAsset.name}] scene";
//	#else
//		protected override bool CanDescribe() => false;
//		protected override string DescribeTask() => string.Empty;
//	#endif
//
//
//	protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource) => await ContentSceneInstanceManager.TryLoadSceneInstance(sceneReference,
//	                                                                                                                                                 loadMode,
//	                                                                                                                                                 activateOnLoad);
//
//}
