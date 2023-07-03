using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class TaskSet_All : TaskSet
    {

        public override string GetDescription() => "Proceed when all of these tasks complete";


        [ContextMenu(itemName: "Run")]
//        public override async Task Run() => await Task.WhenAll(tasks: taskRunners.Select(t => t.Run()));
        public override async UniTask Run()
        {
	        var tasks = new UniTask[taskRunners.Length];

	        for (var i = 0;
	             i < taskRunners.Length;
	             i++)
	        {
		        tasks[i] = taskRunners[i].Run();
	        }

//	        var tasks = Enumerable.Select(source: taskRunners, selector: t => t.Run());

	        await UniTask.WhenAll(tasks: tasks);
        }

    }

}