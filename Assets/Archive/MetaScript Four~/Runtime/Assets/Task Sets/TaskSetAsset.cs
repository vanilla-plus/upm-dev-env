#if DEBUG_VANILLA && METASCRIPT
#define debug
#endif

using System;

using UnityEditor;

using UnityEngine;

using Vanilla.TypeMenu;

using static UnityEngine.Debug;

namespace Vanilla.MetaScript.Four
{

	[CreateAssetMenu(fileName = "[TaskSet] ",
	                 menuName = "Vanilla/MetaScript/4/TaskSet Asset")]
	[Serializable]
	public class TaskSetAsset : ScriptableObject, IValidatable
	{

		[SerializeReference] 
		[TypeMenu]
		public TaskSet_Base set;

		[ContextMenu("Validate")]
		public void Validate()
		{
			#if UNITY_EDITOR
			#if debug
			Log($"Validate {GetType().Name}");
			#endif

			set?.Validate();
			
			AssetDatabase.SaveAssets();
			#endif
		}

	}

}