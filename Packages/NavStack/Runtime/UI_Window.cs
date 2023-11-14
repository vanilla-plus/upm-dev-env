using System;

using UnityEngine;

using Vanilla.DeltaValues;
using Vanilla.Init;

namespace Vanilla.NavStack
{

	[Serializable]
	[RequireComponent(typeof(CanvasGroup))]
	public class UI_Window : MonoBehaviour, IInitiable
	{

		[SerializeField]
		public DeltaState State;

		[SerializeField]
		private CanvasGroup _group;


		protected virtual void OnValidate()
		{
			#if UNITY_EDITOR
			if (_group == null) _group = GetComponent<CanvasGroup>();

			State ??= new DeltaState(name: $"[{GetType().Name}].State",
			                         defaultActiveState: false,
			                         fillSeconds: 0.5f);
			
			PostInit();
			#endif
		}


		public virtual void Init()
		{
			State.Init();
			
			State.Progress.OnValueChanged       += HandleActiveProgress;
			State.Progress.AtMin.OnValueChanged += HandleFullyInactive;
			State.Progress.AtMax.OnValueChanged += HandleFullyActive;
		}


		public virtual void PostInit()
		{
			_group.gameObject.SetActive(State.Active);
			_group.alpha        = State.Progress.Value;
			_group.interactable = State.Progress.AtMax;
		}

		protected virtual void HandleFullyInactive(bool outgoing,
		                                           bool incoming) => _group.gameObject.SetActive(outgoing);


		protected virtual void HandleActiveProgress(float outgoing,
		                                            float incoming) => _group.alpha = incoming;


		protected virtual void HandleFullyActive(bool outgoing,
		                                         bool incoming) => _group.interactable = incoming;


		protected virtual void OnDestroy()
		{
			State.Deinit();

			State.Progress.OnValueChanged       -= HandleActiveProgress;
			State.Progress.AtMin.OnValueChanged -= HandleFullyInactive;
			State.Progress.AtMax.OnValueChanged -= HandleFullyActive;
		}

	}

}