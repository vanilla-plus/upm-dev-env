using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    /*
        Scopes are a concept borrowed from other languages like JavaScript.
        They act as a container for chains-of-execution, kind of like a thread.
        For example, if you only created one Scope right at the start of your application and never made another one,
        if that scope is cancelled, the entire application would close.
        However, if you created a new scope at the start of an intro animation and cancelled that when the user hits the spacebar,
        it would only cancel the intro animation.
        Carefully choosing when to create a new scope is a crucial thing to consider when developing with MetaScript!
    */
    
    [Serializable]
    public class Scope : IDisposable
    {

        private static Dictionary<string, Scope> Active = new Dictionary<string, Scope>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            foreach (var s in Active.Values) s.Cancel();

            Active.Clear();
        }


        public static Scope Get(string name)
        {
            Active.TryGetValue(key: name,
                               value: out var scope);

            return scope;
        }


        public static bool TryCancel(string name)
        {
            if (!Active.ContainsKey(key: name))
            {
                Debug.LogWarning($"No scope called [{name}].");

                return false;
            }

            Active.TryGetValue(key: name,
                               value: out var s);

            if (s == null)
            {
                Debug.LogError($"A scope by the name [{name}] was still considered Active even after being nullified.");
                
                Active.Remove(name);

                return false;
            }
            
            s.Cancel();
            
            Active.Remove(name);

            return true;
        }

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

        public Scope(Scope parent, string name) // Purely for debugging, delete taskName later
        {
            this.parent = parent;
            this._Name  = name;
            this.Depth  = (byte) (parent != null ? parent.Depth + 1 : 0);

            Active.Add(key: name, value: this);

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
//            EditorPlayModeSelfCancellation().Forget();
            #endif
        }
        
        public void Cancel()
        {
            _Continue = false;
            
            Active.Remove(_Name);
        }
        
        public bool Cancelled => !Continue || (parent?.Cancelled ?? false);

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
                
                Active.Remove(_Name);

                // Dispose unmanaged resources.
                disposed = true;
            }
        }

        ~Scope() => Dispose(false);

    }
}
