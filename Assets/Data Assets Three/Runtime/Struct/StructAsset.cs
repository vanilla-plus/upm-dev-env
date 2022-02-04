using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

    [Serializable]
    public abstract class StructAsset<TType, TSource> : GenericAsset<TType, TSource>
        where TType : struct
        where TSource : StructSource<TType>
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