using System;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_String", menuName = "Vanilla/MetaScript/MetaActions/String")]
	public class MetaAction_String : MetaAction_Base
	{

		[TypeMenu("red")]
		[SerializeReference]
		public StringSource Source;
        
		[NonSerialized] public Action<string> OnInvoke;
		
		[ContextMenu("Debug Invoke")]
		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}