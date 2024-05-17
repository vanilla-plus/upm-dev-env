using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.GenericComponent
{
    
	[SerializeField]
	public class AnimatorSource : IComponentSource<Animator, AnimatorSource>
	{

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize() { }

		[SerializeField]
		private Animator _value;
		public Animator Value
		{
			get => _value;
			set => _value = value;
		}

		[SerializeField]
		private Action<Animator> _onSet;
		public Action<Animator> OnSet
		{
			get => _onSet;
			set => _onSet = value;
		}

		[SerializeField]
		private Action<Animator, Animator> _onSetWithHistory;
		public Action<Animator, Animator> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

	}
}