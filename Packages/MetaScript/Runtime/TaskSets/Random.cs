using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
    public class Random : MetaTaskSet
    {
	    
	    protected override string CreateAutoName() => "Run one of the following at random:";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
	        var randomTaskIndex = UnityEngine.Random.Range(0,
	                                                       Tasks.Length);
	        
            var task = Tasks[randomTaskIndex];

            await task.Run(scope);

            return scope;
        }

    }

}