using System;

using UnityEngine;

namespace Vanilla.RayDetectors
{
    
    /// <summary>
    ///     Only one 'T' fits at a time, where T is a reference type.
    /// 
    ///     We're able to listen for when an old T is outgoing or when a new T is incoming to the slot.
    /// </summary>
    [Serializable]
    public class ReferenceSlot<T>
		where T : class
    {

	    [SerializeField]
	    public T _current;
	    public virtual T current
	    {
		    get => _current;
		    set
		    {
			    if (ReferenceEquals(objA: _current,
			                        objB: value))
				    return;

			    onDetectedChange?.Invoke(arg1: _current,
			                             arg2: value);

			    _current = value;
		    }
	    }

	    public Action<T, T> onDetectedChange;

    }
    
}