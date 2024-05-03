using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets.Initializers
{

    public interface IDataInitializer
    {

        void Initialize();

    }

    [Serializable]
    public abstract class InitializerBase<A,T,S> : IDataInitializer
        where A : DataAsset<T,S>
        where S : class, IDataSource<T>
    {

        [SerializeReference] public A Asset;

        [SerializeReference] public T InitialValue;

        public void Initialize()
        {
            if (Asset != null && Asset.Source != null) Asset.Source.Value = InitialValue;
        }

    }
    
}
