using System;
using System.Collections.Generic;
using System.Linq;

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

        public enum FlowState
        {

            Continue,
            Cancelled,
            FastForward

        }
        
        private static Dictionary<string, Scope> Active = new Dictionary<string, Scope>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void StaticReset()
        {
            foreach (var s in Active.Values) s._Continue = false;

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
            #if debug
            Debug.Log($"Scope cancellation requested for [{name}]");
            #endif
            
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
            
            // Okay I think I got it!
            // You might need to change the return value of this from a bool to a Scope.
            // Then iterate recursively until you find a parent that isn't cancelled and return that.
            // Or even a new function altogether that does this specifically? So weird...
            // The reason is, think about it, if you have a running task that is checking for cancellation
            // and detects it being true for the current scope, it should immediately delegate itself back
            // to the previous not-cancelled scope automatically.
            // If you don't do this, TaskSets like Sequences or While Loops don't know how to return control
            // back to the last active parent scope. But of course they should!

//            s.Dispose();
//            
//            Active.Remove(name);

            return true;
        }

        public Scope GetLastActiveScope() => Continue ? this : parent?.GetLastActiveScope();

        
        [SerializeField]
        private bool _Continue = true;
        public bool Continue => _Continue;

        public Scope parent = null;

        [SerializeField]
        private string _Name;
        public string Name => _Name;
        
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
            set => activeTasks = value;
        }

        public Scope(Scope parent, string name)
        {
            this.parent = parent;
            
            _Name  = name;

            Depth = (byte) (parent != null ?
                                parent.Depth + 1 :
                                0);

            Active.Add(key: name, value: this);

            #if debug
            PrintScopeOpened();
            
//            PrintActiveScopes();
            #endif
        }
        
        public void Cancel()
        {
            #if debug
            Debug.Log($"Scope [{Name}] cancelled");
            #endif
            
            _Continue = false;

            Active.Remove(_Name);

            Dispose();
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
                PrintScopeClosed();
                #endif

                if (disposing) { /* Dispose managed resources. */ }
                
                Active.Remove(_Name);

                #if debug
//                PrintActiveScopes();
                #endif

                // Dispose unmanaged resources.
                disposed = true;
            }
        }


        private void PrintScopeOpened()
        {
            var output = $"+ {_Name}";
            
            for (var i = 0;
                 i < Depth;
                 i++)
            {
                output = "    " + output;
            }
            
            Debug.LogWarning($"MetaScript Scope Opened: {output}");
        }

        private void PrintScopeClosed()
        {
            var output = $"- {_Name}";
            
            for (var i = 0;
                 i < Depth;
                 i++)
            {
                output = "    " + output;
            }
            
            Debug.LogWarning($"MetaScript Scope Closed: {output}");
        }

        private void PrintActiveScopes()
        {
            var output = Active.Aggregate(seed: "Active Scopes:\n\n",
                                          func: (current,
                                                 s) => current + $"name:{s.Key} - parent:[{s.Value.parent}]\n");
            
            Debug.LogWarning(output);
        }


        public override string ToString() => Name;

        ~Scope() => Dispose(false);

    }
}
