using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Vec3", menuName = "Vanilla/MetaScript/MetaActions/Vec3")]
	public class MetaAction_Vec3 : MetaAction_Base<Vector3>
	{

//		[TypeMenu("red")]
//		[SerializeReference]
//		public Vec3Source Source;
        
//		[NonSerialized] public Action<Vector3> OnInvoke;
		
//		[ContextMenu("Debug Invoke")]
//		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}