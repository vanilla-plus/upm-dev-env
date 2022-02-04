using System;

using UnityEngine;

namespace Vanilla.MenuMachine
{
    
//    public interface IMenuItemState
//    {

//        Toggle Toggle
//        {
//            get;
//        }

//        Normal Normal
//        {
//            get;
//        }
//
//    }

    [Serializable]
    public class MenuItemState2D
    {

        [SerializeField]
        private Toggle _toggle = new();
        public  Toggle Toggle => _toggle;

        [SerializeField]
        private Normal _normal;
        public  Normal Normal => _normal;

        private CanvasGroup canvasGroup;

        public MenuItemState2D(CanvasGroup group,
                               Toggle activator)
        {
	        canvasGroup = group;
	        
	        _toggle = activator ?? new Toggle();

	        _normal = new Normal(activator: _toggle);
	        
//            _toggle.onChange += a => _normal.toggle.active = a;

            // UI elements that are fully transparent still induce a draw call.
            // We can remove this problem by deactivating them if they're fully transparent. 

            _normal.onNoLongerEmpty += () => canvasGroup.gameObject.SetActive(true);
            _normal.onBecameEmpty += () => canvasGroup.gameObject.SetActive(false);

            canvasGroup.gameObject.SetActive(!_normal.IsEmpty);
            
//            _normal.empty.onChange += isEmpty => canvasGroup.gameObject.SetActive(value: !isEmpty);
            
            _normal.onFillFrame += n => canvasGroup.alpha = n;
            _normal.onDrainFrame += n => canvasGroup.alpha = n;

//	        _normal.onChange += n => canvasGroup.alpha = n;
        }


        internal void OnValidate()
        {
	        _toggle.active = !_toggle._active;
        }
        
//        public void Activate() => _normal.toggle.active = true;
//
//        public void Deactivate() => _normal.toggle.active = false;

    }
}
