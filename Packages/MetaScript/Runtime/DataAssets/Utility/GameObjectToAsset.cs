using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class GameObjectToAsset : MonoBehaviour
    {

        [SerializeField]
        public GameObjectAsset Asset;

        void OnEnable()
        {
            if (Asset        != null &&
                Asset.Source != null)
            {
                Asset.Source.Value = gameObject;
            }
        }


        void OnDisable()
        {
            if (ReferenceEquals(objA: Asset.Source.Value,
                                objB: gameObject)) Asset.Source.Value = null;
        }

    }

}