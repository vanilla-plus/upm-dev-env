using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Bool", menuName = "Vanilla/MetaScript/MetaActions/Bool")]
	public class MetaAction_Bool : MetaAction_Base<bool>
	{

//		[TypeMenu("red")]
//		[SerializeReference]
//		public BoolSource Source;
//        
//		[NonSerialized] public Action<bool> OnInvoke;
//		
//		[ContextMenu("Debug Invoke")]
//		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}