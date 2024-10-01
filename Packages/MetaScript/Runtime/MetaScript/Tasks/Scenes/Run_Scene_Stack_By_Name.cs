using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class NamedContentSceneSlot
    {

        [SerializeField] public string sceneName;

        [SerializeReference]
        [TypeMenu("red")]
        public MetaTask sceneInit;

        [SerializeField] public LoadSceneMode loadMode = LoadSceneMode.Additive;

        [SerializeField] public bool unloadAfterCompletion = true;
        
        [SerializeField] public UnloadSceneOptions unloadOptions = UnloadSceneOptions.UnloadAllEmbeddedSceneObjects;

        [SerializeReference]
        [TypeMenu("red")]
        public MetaTask sceneDeInit;

    }
    
    [Serializable]
    public class Run_Scene_Stack_By_Name : MetaTask
    {
        
        [SerializeReference]
        [TypeMenu("green")]
        public IScopeSource scopeSource = new Named_Scope_Source
                                          {
                                              name = "content"
                                          };

        [SerializeField] public MetaAction_Bool LoadOperationInProgress;
        [SerializeField] public MetaAction_Float LoadOperationProgress;

        [SerializeField]
        public NamedContentSceneSlot[] ContentSceneSlots = Array.Empty<NamedContentSceneSlot>();

        protected override bool Validate
        {
            get
            {
                foreach (var c in ContentSceneSlots)
                {
                    if (c == null) return false;

                    if (string.IsNullOrEmpty(c.sceneName)) return false;
                }

                return true;
            }

        }

        protected override string CreateAutoName() => "Load and run each scene";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            // We declare contentSceneScope early so it can be cancelled just-in-case at the end, outside of the scene loop.
            
            Scope contentSceneScope = null;
            
            foreach (var c in ContentSceneSlots)
            {
                // Init
                
                // Init and DeInit are considered under the scope of Run_Scene_Stack rather than our inner-more scope contentScope.
                // The reason for this is that contentSceneScope should encapsulate the behaviour of the scene only, while
                // Init and DeInit need to happen regardless of content-scene cancellation.

                if (c.sceneInit != null) scope = await c.sceneInit.Run(scope);
                
                // Scene-specific scope

                contentSceneScope = scopeSource.CreateScope(scope);
                
                // Load
                
                LoadOperationInProgress?.Invoke(true);
                
                LoadOperationProgress?.Invoke(0.0f);
                
                var loadOp = SceneManager.LoadSceneAsync(c.sceneName, c.loadMode);

                while (!loadOp.isDone)
                {
                    if (contentSceneScope.Cancelled) return scope;
                    
                    // Show user load progress
                    
                    LoadOperationProgress?.Invoke(loadOp.progress);

                    await UniTask.Yield();
                }
                
                LoadOperationInProgress?.Invoke(false);
                
                // Jump to scene
                
                GameObject[] rootObjects = null;
            
                for (var i = 0;
                     i < SceneManager.loadedSceneCount;
                     i++)
                {
                    var scene = SceneManager.GetSceneAt(i);

                    if (scene.name == c.sceneName)
                    {
                        rootObjects = scene.GetRootGameObjects();
                    }
                }

                if (rootObjects == null)
                {
                    Debug.LogWarning($"[{c.sceneName}] scene is empty.");
                }
                else
                {
                    MetaTaskInstance instance = null;

                    foreach (var o in rootObjects)
                    {
                        instance = o.GetComponent<MetaTaskInstance>();

                        if (instance != null) break;
                    }

                    if (instance == null)
                    {
                        Debug.LogWarning($"Couldn't find a MetaScriptInstance attached to any root GameObject in the [{c.sceneName}] scene.");
                    }
                    else // We want to else this just in case no instance was found - the scene should still be unloaded.
                    {
                        if (instance.Task != null) await instance.Task.Run(contentSceneScope);
                    }
                }

                contentSceneScope?.Cancel();

                // Unload

                LoadOperationInProgress?.Invoke(true);

                if (c.unloadAfterCompletion)
                {
                    var unloadOp = SceneManager.UnloadSceneAsync(c.sceneName, c.unloadOptions);

                    while (!unloadOp.isDone)
                    {
//                        if (contentScope.Cancelled) return scope;
                        
                        LoadOperationProgress?.Invoke(unloadOp.progress);

                        await UniTask.Yield();
                    }
                }
                
                // DeInit

                if (c.sceneDeInit != null) contentSceneScope = await c.sceneDeInit.Run(contentSceneScope);
            }

            contentSceneScope?.Cancel(); // This should never need to be called and is purely here for just-in-case-ism

            return scope;
        }

    }
}
