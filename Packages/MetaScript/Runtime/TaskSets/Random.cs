#if vanilla_roulette
using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Roulette;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
    public class Random : MetaTaskSet
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


        protected override async UniTask<Tracer> _Run(Tracer tracer)
        {
	        var randomTaskIndex = Table.SpinForAnIndex();
	        
            var task = Tasks[randomTaskIndex];

            await task.Run(tracer);

            return tracer;
        }

    }

}
#endif