using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript
{
    
    // Isn't this dumb? You have to 'seal' the interface over for it to work as SerializableReference
    
    public interface IGetSetBool : IGetSetSource<bool> { }

    public interface IGettableBool : IGettableSource<bool> { }
    
    public interface ISettableBool : ISettableSource<bool> { }
    
    public interface IProtectedBool : IProtectedSource<bool> { }
    
    public interface IObservableBool : IObservableSource<bool> { }
}
