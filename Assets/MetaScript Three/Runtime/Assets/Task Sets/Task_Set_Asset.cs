#if DEBUG_VANILLA && METASCRIPT
#define debug
#endif

using UnityEngine;

using Vanilla.TypeMenu;

using static UnityEngine.Debug;

namespace Vanilla.MetaScript.Three
{

	[CreateAssetMenu(fileName = "New Task Set",
	                 menuName = "Vanilla/MetaScript/3/Task Set")]
	public class Task_Set_Asset : ScriptableObject, IValidatable
	{

		[SerializeReference] [TypeMenu]
		public Task_Set set;

		[ContextMenu("Validate")]
		public void Validate()
		{
			#if UNITY_EDITOR
			#if debug
			Log($"Validate {GetType().Name}");
			#endif

			set?.Validate();
			#endif
		}

	}

}