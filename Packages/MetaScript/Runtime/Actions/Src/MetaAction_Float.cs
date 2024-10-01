using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	[CreateAssetMenu(order = 0, fileName = "New MetaAction_Float", menuName = "Vanilla/MetaScript/MetaActions/Float")]
	public class MetaAction_Float : MetaAction_Base<float>
	{

//		[TypeMenu("red")]
//		[SerializeReference]
//		public FloatSource Source;
        
//		[NonSerialized] public Action<float> OnInvoke;
		
//		[ContextMenu("Debug Invoke")]
//		public override void Invoke() => OnInvoke?.Invoke(Source.Value);
		
	}
}