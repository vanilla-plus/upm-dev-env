using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.NavStack
{

    [Serializable]
    public class NavStack_Button_Back : NavStack_Button
    {

        [SerializeField]
        public bool HideWhenStackEmpty = true;

        [SerializeField]
        public bool NonInteractiveWhenStackEmpty = true;

        protected override void Awake()
        {
            base.Awake();
            
            _stack.StackIsEmpty.OnValueChanged += HandleStackIsEmpty;
        }


        protected virtual void OnEnable()
        {
            if (HideWhenStackEmpty) gameObject.SetActive(false);
            if (NonInteractiveWhenStackEmpty) _button.interactable = false;
        }


        protected override void OnDestroy()
        {
            _stack.StackIsEmpty.OnValueChanged -= HandleStackIsEmpty;

            base.OnDestroy();
        }


        private void HandleStackIsEmpty(bool outgoing,
                                        bool incoming)
        {
            if (HideWhenStackEmpty) gameObject.SetActive(outgoing);
            if (NonInteractiveWhenStackEmpty) _button.interactable = outgoing;
        }


        public override async void Click()
        {
            if (_stack.asyncAwait)
            {
                _button.interactable = false;
                
                await _stack.Nav_Back_Async();

                _button.interactable = !NonInteractiveWhenStackEmpty || NonInteractiveWhenStackEmpty && _stack.History.Count > 0;
                
                if (!NonInteractiveWhenStackEmpty)
                {
                    _button.interactable = true;
                }
                else
                {
                    _button.interactable = _stack.History.Count > 0;
                }
            }
            else
            {
                _stack.Nav_Back_Async().Forget();
            }
        }

    }

}