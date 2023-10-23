#if vanilla_metascript
using System;

#if cysharp_unitask
using Cysharp.Threading.Tasks;
#endif

using UnityEngine;

using Vanilla.Roulette;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
    public class RandomFromRoulette : MetaTaskSet
    {

	    [SerializeField]
	    public RouletteTable<RouletteTableItem> Table = new RouletteTable<RouletteTableItem>();
	    
	    public override void OnValidate()
	    {
		    #if UNITY_EDITOR
		    base.OnValidate();

		    Table ??= new RouletteTable<RouletteTableItem>();

		    if (Table.items.Count != Tasks.Length)
		    {
			    while (Table.items.Count > Tasks.Length) Table.items.RemoveAt(Table.items.Count - 1);
			    while (Table.items.Count < Tasks.Length) Table.items.Add(new RouletteTableItem());
		    }
		    #endif
	    }


	    protected override string CreateAutoName() => "Run one of the following at random:";


        protected override async UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
        {
	        var randomTaskIndex = Table.SpinForAnIndex();
	        
            var task = Tasks[randomTaskIndex];

            await task.Run(trace);

            return trace;
        }

    }

}
#endif