using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class Binary_Branch : TaskBase
    {

        [SerializeField]
        public ValueSocket<bool, BoolSocket, BoolAsset, BoolAccessor> testCondition;

        [SerializeField] // Convert to... Task socket..?
        public TaskRunner trueTask;

        [SerializeField] // Convert to... Task socket..?
        public TaskRunner falseTask;

        public override async UniTask Run()
        {
            #if DEBUG_METASCRIPT
                var result = await testCondition.Get();
            
//                Debug.Log(message: $"[testCondition] evaluates as [{result}] : Proceeding with task [{(result ? DescribeTaskRunner(trueTask) : DescribeTaskRunner(falseTask))}]");
            #endif

            var get = await testCondition.Get();
            
            await (await testCondition.Get() ? trueTask : falseTask).Run();
        }


        public override string GetDescription() =>
            $"If [{testCondition.GetAssetName()}]...";


    }

}