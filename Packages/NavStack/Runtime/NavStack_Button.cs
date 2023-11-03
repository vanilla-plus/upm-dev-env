using System;

using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.NavStack
{
    
	[RequireComponent(typeof(Button))]
	[Serializable]
	public abstract class NavStack_Button : NavStack_Element
	{
		[SerializeField]
		public Button _button;
		
		private void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();
            
			if (!_button) _button = GetComponent<Button>();
			#endif
		}

		protected virtual void Awake()     => _button.onClick.AddListener(Click);
		protected virtual void OnDestroy() => _button.onClick.RemoveListener(Click);

		public abstract void Click();

	}
}