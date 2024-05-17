using UnityEngine;

using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.MetaScript.DataSources.Bindings
{

	[SerializeField]
	public class StringSource_To_Animator_Trigger : MonoBehaviour
	{

		[SerializeField] public Animator Animator;

		[SerializeReference] public StringSource Source;

		void OnEnable() => Source.OnSet += HandleSet;

		void OnDisable() => Source.OnSet -= HandleSet;

		private void HandleSet(string trigger) => Animator.SetTrigger(trigger);

	}

}