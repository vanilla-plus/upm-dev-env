using System;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public abstract class MetaAction_Base : ScriptableObject
	{

		#if UNITY_EDITOR
		[TextArea(8, 20)] public string Description = "Describe this action below:\n\n• What does it do?\n• What scene is it used in?\n• When should it be called?";
		#endif

		public abstract void Invoke();

	}
}