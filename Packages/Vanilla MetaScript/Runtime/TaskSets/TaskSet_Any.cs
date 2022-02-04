using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class TaskSet_Any : TaskSet
    {

        public override    string GetDescription()   => "Proceed when any of these tasks complete";

        [ContextMenu(itemName: "Run")]
        public override async UniTask Run()
        {
            var tasks                  = new UniTask[taskRunners.Length];
            
            for (var i = 0;
                 i < taskRunners.Length;
                 i++)
            {
                tasks[i] = taskRunners[i].Run();
            }

            #if DEBUG_METASCRIPT
	            var f = await UniTask.WhenAny(tasks: tasks);

	            Debug.Log($"The first task to complete was [{taskRunners[f].task.GetDescription()}]");
            #else
				await UniTask.WhenAny(tasks);
            #endif
        }

//        [ContextMenu("Cancel")]
//        public override void Cancel()
//        {
//            base.Cancel();
//            
//            foreach (var t in taskRunners)
//            {
//                if (t.task.running)
//                {
//                    t.task.Cancel();
//                }
//            }
//        }

    }

}