using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Int", menuName = "Vanilla/MetaScript/MetaActions/Int")]
	public class MetaAction_Int : MetaAction_Base<int>
	{

//		[TypeMenu("red")]
//		[SerializeReference]
//		public IntSource Source;
        
//		[NonSerialized] public Action<int> OnInvoke;

//		[ContextMenu("Debug Invoke")]
//		public override void Invoke() => OnInvoke?.Invoke(Source.Value);


	}
}