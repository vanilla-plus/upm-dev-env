using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Four
{
    [Serializable]
    public abstract class TaskSetPlayer_Base : MonoBehaviour
    {

        [SerializeField]
        public TaskSetAsset taskSetAsset;
        
        public async UniTask Play() => await taskSetAsset.set.Run();

    }
}
