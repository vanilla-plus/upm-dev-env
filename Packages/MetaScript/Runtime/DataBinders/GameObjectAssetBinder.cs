using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources.GameObject;

namespace Vanilla.MetaScript.DataBinders
{
    
    [Serializable]
    public class GameObjectAssetBinder : ReferenceBinder<GameObject, IGameObjectSource, GameObjectAsset>
    {
        
        protected override GameObject Assign => gameObject;

    }
}
