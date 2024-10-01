using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Color", menuName = "Vanilla/MetaScript/MetaActions/Color")]
	public class MetaAction_Color : MetaAction_Base<Color>
	{

//		[TypeMenu("red")]
//		[SerializeReference]
//		public ColorSource Source;
        
//		[NonSerialized] public Action<Color> OnInvoke;
		
//		[ContextMenu("Debug Invoke")]
//		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}