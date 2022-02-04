using System;

using UnityEngine;

namespace Vanilla.MenuMachine
{
    
    [Serializable]
    [RequireComponent(typeof(CanvasGroup))]
    public class SampleMenuItem : MonoBehaviour
    {

        [SerializeField]
        public MenuItemState2D state;

        // Hold up time! :')
        // This whole set up is great, but it is kind of assuming that the reason we're using it isn't necessarily a Hover.
        // If we automatically turn off the GameObject when its normal is 0, that's an unrecoverable state for a Hover-able.
        // Woops. Make turning off optional?

        private void OnValidate()
        {
            state.OnValidate();
        }


        void Awake()
        {
            state = new MenuItemState2D(@group: GetComponent<CanvasGroup>(),
                                        activator: null);
        }

    }
}
