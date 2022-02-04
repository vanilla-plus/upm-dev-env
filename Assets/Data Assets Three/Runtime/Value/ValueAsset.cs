using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

    [Serializable]
    public abstract class ValueAsset<TType, TSource> : GenericAsset<TType, TSource>
        where TType : unmanaged
        where TSource : ValueSource<TType>
    {

        [SerializeField]
        public TType initializationValue;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            source?.Set(initializationValue);
        }

    }

}