using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MediaLibrary
{

    [Serializable]
    public abstract class TileGroup<I, T> : MonoBehaviour
        where I : Tile<I, T>
        where T : Transform
    {

        public T Transform;
        
        private HashSet<I> _items;
        public  HashSet<I> Items => _items;

        [SerializeReference]
        [TypeMenu]
        [Only(typeof(ITileGroupModule))]
        private ITileGroupModule[] _modules = Array.Empty<ITileGroupModule>();
        public ITileGroupModule[] Modules;
        
        protected virtual void Awake()
        {
            Transform = transform as T;

            foreach (var m in _modules)
            {
//                m.Populate(_items);
                
                // Argh! So close... again...
                // It looks as though TypeMenu can't use generic type parameters (I/T/etc)
                // Any filters have to be finalized.
                // In this way, how can you ever refer to someModuleInstance.Items?
                
//                m._items = _items;
            }
        }

    }
    

}