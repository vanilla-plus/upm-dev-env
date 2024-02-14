using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.NavStack
{

	[Serializable]
	public class NavStack_Button_Open : NavStack_Button
	{

		[SerializeField]
		public NavStack_Window _target;

		[SerializeField]
		public bool HideWhileTargetOpen = true;

		[SerializeField]
		public bool NonInteractiveWhileTargetOpen = true;
		
		protected override void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();

			if (!_target)
			{
				Debug.LogWarning("NavStack_Button_Open with no target window (That's bad).");
			}
			else
			{
				gameObject.name = $"Open [{_target.name}] Window";
			}
			#endif
		}


		protected override void Awake()
		{
			base.Awake();

			_target._window.State.Progress.AtMin.OnSet += HandleTargetActiveAtAll;
		}

		protected virtual void OnEnable()
		{
			if (HideWhileTargetOpen) gameObject.SetActive(!_target._window.State.Active);
			if (NonInteractiveWhileTargetOpen) _button.interactable = !_target._window.State.Active;
		}

		protected override void OnDestroy()
		{
			_target._window.State.Progress.AtMin.OnSet += HandleTargetActiveAtAll;

			base.OnDestroy();
		}


		private void HandleTargetActiveAtAll(bool incoming)
		{
			if (HideWhileTargetOpen) gameObject.SetActive(incoming);
			if (NonInteractiveWhileTargetOpen) _button.interactable = incoming;
		}


		public override void Click() => _stack.Nav_Open(_target).Forget();

	}

}