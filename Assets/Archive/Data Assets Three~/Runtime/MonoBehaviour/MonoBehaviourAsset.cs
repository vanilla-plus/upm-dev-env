using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{
    [CreateAssetMenu(fileName = "[MonoBehaviour] ",
                     menuName = "Vanilla/Data Assets/3/Ref/MonoBehaviour")]
    [Serializable]
    public class MonoBehaviourAsset : RefAsset<MonoBehaviour, MonoBehaviourSource>
    {
        
    }
}
