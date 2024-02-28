using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.MetaScript.Flow;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class If_Bool_Source : Switch
	{

		[SerializeReference]
		[TypeMenu("red")]
		public BoolSource source = new AssetBoolSource();

		protected override bool CanAutoName() => source != null && _tasks is
		                                         {
			                                         Length: >= 2
		                                         };

		protected override string CreateAutoName() => $"If ({(source is AssetBoolSource boolSource ? boolSource.Asset.name : source.GetType().Name)}) ? {_tasks[0]?.Name} : {_tasks[1]?.Name}";

		public override    int    Evaluate()       => source.Value ? 0 : 1;

	}
}