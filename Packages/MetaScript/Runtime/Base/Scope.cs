using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    /*
    In MetaScript, a Scope object is kind of like a relay baton.
    A new one is created whenever a new execution context is created (for example, loading a custom scene and jumping to it)
    and each scope should be cleaned up ('disposed') once that context is over.
    Scopes have a reference to their parent scope, if any, and check their parent for cancellation as well.
    So we should be careful when creating new scopes because they can accidentally stick around in memory
    if not disposed of at the right time.
    */
    
    [Serializable]
    public class Scope : IDisposable
    {

        [SerializeField]
        private bool _Continue = true;
        public bool Continue => _Continue;

        public Scope parent = null;

        [SerializeField]
        private string _Name;
        
        [SerializeField]
        private byte _Depth;
        public byte Depth
        {
            get => _Depth;
            set => _Depth = value;
        }

        [SerializeField]
        private byte activeTasks = 0;
        public byte ActiveTasks
        {
            get => activeTasks;
            set
            {
                activeTasks = value;

//                Debug.Log($"{_Name}.ActiveTasks is now [{activeTasks}]");
                
//                if (activeTasks != 0) return;

//                Debug.LogError($"[{Time.frameCount}] Execution is officially over with a status of [{(Continue ? "Completed" : "Cancelled")}]");

                // Funny little problem here.
                // activeTasks will temporarily be set to 0 before 
                
//                Dispose(); 
            }
        }


        private async UniTask EditorPlayModeSelfCancellation()
        {
            #if UNITY_EDITOR
            while (_Continue && Application.isPlaying)
            {
                await UniTask.Yield();
            }
            
            Cancel();
            #endif
        }

        public Scope(Scope parent, string taskName, string taskType) // Purely for debugging, delete taskName later
        {
            this.parent = parent;
            this.Depth  = (byte) (parent != null ? parent.Depth + 1 : 0);
            this._Name  = $"[{taskName}] ({taskType})";

//            var output = $"• Scope Created [{_Depth}] [{_Name}]";
//            
//            for (var i = 0;
//                 i < Depth;
//                 i++)
//            {
//                output = "    " + output;
//            }
            
//            Debug.LogWarning($"Scope Created [{Depth}] {_Name}");

//            Debug.LogWarning(output);

            #if debug
            var output = $"+ {_Name}";
            
            for (var i = 0;
                 i < Depth;
                 i++)
            {
                output = "    " + output;
            }
            
            Debug.Log(output);
            #endif
            
            #if UNITY_EDITOR
            EditorPlayModeSelfCancellation().Forget();
            #endif

            // Bummer, this seems to be the only (?) way to accurately check if the scope is ready for disposal.
            // The short version is that the perfect time is actually just checking when ActiveTasks == 0
            // But that is technically true in-between tasks, even if there are more to run!
            // So you get a situation where a scope for a Sequence will Dispose() itself after the first task completes.
            // The second and third and any more tasks will still run, but... yeah. Not ideal.
            // So instead, we do this dumb shit below just to make sure ActiveTasks is 0 across more than one frame 🤷🏻‍🤦🏻‍
            // But is there another way to check altogether..?
            
//            MonitorActiveTasks();
        }


//        private async UniTask MonitorActiveTasks()
//        {
//            await UniTask.WaitUntil(() => ActiveTasks == 0);
//            
//            Dispose();
//        }


        public void Cancel() => _Continue = false;

//        public void Cancel_With_Parent()
//        {
//            _Continue = false;
//            
//            parent?.Cancel();
//        }
        
        public void Cancel_Recursive()
        {
            _Continue = false;

            parent?.Cancel_Recursive();
        }

        public void Cancel_Upwards(int numberOfLayers)
        {
            _Continue = false;
            
//            Debug.LogError($"Scope Cancelled [{Depth}] {_Name}");

            numberOfLayers--;

            if (numberOfLayers <= 0) return;
            
            parent?.Cancel_Upwards(numberOfLayers);
        }

        public bool Cancelled => !Continue || (parent?.Cancelled ?? false);
//
//        public void Enter() => ActiveTasks++;
//
//        public void Exit() => ActiveTasks--;

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
//                var output = $"• Scope Disposed [{_Depth}] [{_Name}]";
//            
//                for (var i = 0;
//                     i < Depth;
//                     i++)
//                {
//                    output = "    " + output;
//                }
//            
//                Debug.LogError(output);

                #if debug
                var output = $"- {_Name}";
            
                for (var i = 0;
                     i < Depth;
                     i++)
                {
                    output = "    " + output;
                }
            
                Debug.Log(output);
                #endif

                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources.
                disposed = true;
            }
        }

        ~Scope() => Dispose(false);

    }
}
