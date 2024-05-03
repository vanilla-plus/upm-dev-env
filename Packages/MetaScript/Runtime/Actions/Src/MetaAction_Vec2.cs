using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Vec2", menuName = "Vanilla/MetaScript/MetaActions/Vec2")]
	public class MetaAction_Vec2 : MetaAction_Base
	{

		[TypeMenu("red")]
		[SerializeReference]
		public Vec2Source Source;
        
		[NonSerialized] public Action<Vector2> OnInvoke;
		
		[ContextMenu("Debug Invoke")]
		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}