using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace MagicalProject
{
    public class MagicalTask : TaskBase
    {

        public override string GetDescription() => "It's magic";


        public override UniTask Run() => default;

    }
}
