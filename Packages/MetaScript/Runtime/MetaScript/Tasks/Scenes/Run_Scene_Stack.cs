//using System;
//using System.Linq;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//using Vanilla.TypeMenu;
//
//namespace Vanilla.MetaScript
//{
//
//    [Serializable]
//    public class ContentSceneSlot
//    {
//
//        [SerializeReference]
//        [TypeMenu("red")]
//        public MetaTask sceneInit;
//
////        [SerializeField] public string sceneName;
//
//        [SerializeField] public LoadSceneMode loadMode = LoadSceneMode.Single;
//
//        [SerializeField] public bool unloadAfterCompletion = true;
//        
//        [SerializeField] public UnloadSceneOptions unloadOptions = UnloadSceneOptions.UnloadAllEmbeddedSceneObjects;
//
//        [SerializeReference]
//        [TypeMenu("red")]
//        public MetaTask sceneDeInit;
//
//    }
//    
//    [Serializable]
//    public class Run_Scene_Stack : MetaTask
//    {
//
//        [SerializeField] public int contentSceneStartingIndex = 3;
//
//        [SerializeReference]
//        [TypeMenu("green")]
//        public IScopeSource scopeSource = new Named_Scope_Source
//                                          {
//                                              name = "content"
//                                          };
//
//        [SerializeField] public MetaAction_Bool LoadOperationInProgress;
//        [SerializeField] public MetaAction_Float LoadOperationProgress;
//
//        [SerializeField]
//        public ContentSceneSlot[] ContentSceneSlots = Array.Empty<ContentSceneSlot>();
//
//        protected override bool Validate => ContentSceneSlots.All(c => c != null);
//
//        //        protected override bool Validate
////        {
////            get
////            {
////                foreach (var c in ContentSceneSlots)
////                {
////                    if (c == null) return false;
////
////                    if (string.IsNullOrEmpty(c.sceneName)) return false;
////                }
////
////                return true;
////            }
////
////        }
//
//        protected override string CreateAutoName() => "Load and run each scene";
//
//
//        protected override async UniTask<Scope> _Run(Scope scope)
//        {
////            var   s            = scope;
//            Scope contentScope = null;
//
//            var currentSceneIndex = contentSceneStartingIndex;
//            
//            for (var currentSceneIndex = contentSceneStartingIndex; currentSceneIndex < SceneManager.sceneCountInBuildSettings; currentSceneIndex++)
////            foreach (var c in ContentSceneSlots)
//            {
//                // Null check
//                
////                if (c == null || string.IsNullOrEmpty(c.sceneName)) continue;
//
//                contentScope = scopeSource.CreateScope(scope);
//
//                // Init
//                
//                if (c.sceneInit != null) contentScope = await c.sceneInit.Run(contentScope);
//                
//                // Load
//                
//                LoadOperationInProgress?.Invoke(true);
//                
//                LoadOperationProgress?.Invoke(0.0f);
//                
////                var loadOp = SceneManager.LoadSceneAsync(c.sceneName, c.loadMode);
//                var loadOp = SceneManager.LoadSceneAsync(currentSceneIndex, c.loadMode);
//
//                while (!loadOp.isDone)
//                {
//                    if (contentScope.Cancelled) return scope;
//                    
//                    // Show user load progress
//                    
//                    LoadOperationProgress?.Invoke(loadOp.progress);
//
//                    await UniTask.Yield();
//                }
//                
//                LoadOperationInProgress?.Invoke(false);
//                
//                // Jump to scene
//                
//                GameObject[] rootObjects = null;
//            
//                for (var i = 0;
//                     i < SceneManager.loadedSceneCount;
//                     i++)
//                {
//                    var scene = SceneManager.GetSceneAt(i);
//
////                    if (scene.name == c.sceneName)
//                    if (scene.buildIndex == currentSceneIndex)
//                    {
//                        rootObjects = scene.GetRootGameObjects();
//                    }
//                }
//
//                if (rootObjects == null)
//                {
////                    Debug.LogWarning($"[{c.sceneName}] scene is empty.");
//                    Debug.LogWarning($"[{SceneManager.GetSceneByBuildIndex(currentSceneIndex).name}] scene is empty.");
//                }
//                else
//                {
//                    MetaTaskInstance instance = null;
//
//                    foreach (var o in rootObjects)
//                    {
//                        instance = o.GetComponent<MetaTaskInstance>();
//
//                        if (instance != null) break;
//                    }
//
//                    if (instance == null)
//                    {
////                        Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{c.sceneName}] scene.");
//                        Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{SceneManager.GetSceneByBuildIndex(currentSceneIndex).name}] scene.");
//                    }
//                    else // We want to else this just in case no instance was found - the scene should still be unloaded.
//                    {
//                        if (instance.Task != null) await instance.Task.Run(contentScope);
//                    }
//                }
//
//                // Unload
//
//                LoadOperationInProgress?.Invoke(true);
//
//                if (c.unloadAfterCompletion)
//                {
////                    var unloadOp = SceneManager.UnloadSceneAsync(c.sceneName, c.unloadOptions);
//                    var unloadOp = SceneManager.UnloadSceneAsync(currentSceneIndex, c.unloadOptions);
//
//                    while (!unloadOp.isDone)
//                    {
//                        if (contentScope.Cancelled) return scope;
//                        
//                        LoadOperationProgress?.Invoke(unloadOp.progress);
//
//                        await UniTask.Yield();
//                    }
//                }
//                
//                // DeInit
//
//                if (c.sceneDeInit != null) contentScope = await c.sceneDeInit.Run(contentScope);
//                
//                contentScope?.Cancel();
//
//                contentSceneStartingIndex++;
//            }
//
//            contentScope?.Cancel();
//
//            return scope;
//        }
//
//    }
//}
