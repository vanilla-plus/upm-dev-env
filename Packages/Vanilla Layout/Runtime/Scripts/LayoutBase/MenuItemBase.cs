using System;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

    public abstract class MenuItemBase<M, I> : MonoBehaviour
        where M : MenuBase<M, I>
        where I : MenuItemBase<M, I>
    {

        private static I _current;
        public static I Current
        {
            get => _current;
            set
            {
                if (ReferenceEquals(objA: _current,
                                    objB: value)) return;

                var old = _current;

                _current = value;

                onNewSelection?.Invoke(arg1: old,
                                       arg2: _current);
            }
        }

        public static Action<I, I> onNewSelection;

        public Toggle preview;
        public Toggle selected;

    }

}