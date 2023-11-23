using System;

using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.NavStack
{
    
    [Serializable]
    public class NavStack : MonoBehaviour
    {

	    [Tooltip("Should NavStack_Window transitions await each-others completion or can they overlap? (i.e. cross-fading)")]
	    [SerializeField]
	    public bool asyncAwait = false;

	    [Tooltip("Should NavStack_Window transitions abide by scaled time or unscaled time? All NavStack_Windows driven by this stack will use this setting.")]
	    [SerializeField]
	    public bool useScaledTime = true;
	    
        [NonSerialized]
        public Stack<NavStack_Window> History = new Stack<NavStack_Window>();

        [NonSerialized]
        public DeltaBool StackIsEmpty;


        void Awake() => StackIsEmpty = new DeltaBool(name: $"[{gameObject.name}].NavStack.StackIsEmpty",
                                                     defaultValue: true);


        public async UniTask Nav_Open(NavStack_Window newWindow)
	    {
//		    LogStackContents();

		    if (History.TryPeek(out var current))
		    {
			    if (newWindow == current)
			    {
				    Debug.LogWarning("Can't open the same window twice - we're not animals.");
				    
				    return;
			    }
			    
			    if (current.RemainOpenUnderneath)
			    {
				    // Do nothing; this window is configured to remain as-is.
			    }
			    else
			    {
				    // Hide it

				    Debug.Log($"Hiding [{current.name}]");
				    
				    current._window.State.Active.Value = false;

				    if (asyncAwait)
				    {
					    var progress = current._window.State.Progress;

					    while (!progress.AtMin) await UniTask.Yield();
				    }
			    }
		    }

		    Debug.Log($"Opening [{newWindow.name}]");

		    newWindow._window.State.Active.Value = true;

		    History.Push(newWindow);

		    if (asyncAwait)
		    {
			    var progress = newWindow._window.State.Progress;

			    while (!progress.AtMax) await UniTask.Yield();
		    }

		    StackIsEmpty.Value = History.Count == 0;

//		    LogStackContents();
	    }

//		[ContextMenu("Log Stack Contents")]
//        public void LogStackContents()
//        {
//	        foreach (var w in History)
//	        {
//		        Debug.LogWarning(w);
//	        }
//        }

	    public void Nav_Back_Sync() => Nav_Back_Async().Forget();

	    public async UniTask Nav_Back_Async()
	    {
//		    LogStackContents();

		    if (!History.TryPop(out var current))
            {
                #if debug
                Debug.LogWarning("Can't navigate back; the History stack is empty.");
                #endif
                
                return;
            }
		    
		    // Handle deactivating the current panel

		    current._window.State.Active.Value = false;
		    
		    if (asyncAwait)
		    {
			    var progress = current._window.State.Progress;

			    while (!progress.AtMin) await UniTask.Yield();
		    }
		    
		    // Handle reactivating the previous panel

		    if (History.TryPeek(out var previous))
		    {
			    if (previous.RemainOpenUnderneath)
			    {
				    // Tbh, we don't really need to do this check.
				    // It's just potentially saving some execution.
				    // We could easily just run the {else} code below by itself.
			    }
			    else
			    {
				    previous._window.State.Active.Value = true;
				    
				    if (asyncAwait)
				    {
					    var progress = previous._window.State.Progress;

					    while (!progress.AtMax) await UniTask.Yield();
				    }
			    }
		    }
		    
		    StackIsEmpty.Value = History.Count == 0;

//		    LogStackContents();
	    }

    }
}
