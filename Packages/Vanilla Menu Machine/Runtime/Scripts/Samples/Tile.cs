using UnityEngine;
using UnityEngine.EventSystems;

namespace Vanilla.MediaLibrary
{

    public abstract class Tile<I, T> : MonoBehaviour,
                           IPointerEnterHandler,
                           IPointerExitHandler,
                           IPointerDownHandler,
                           IPointerUpHandler
        where I : Tile<I,T>
        where T : Transform
    {

        public T Transform;
        
        public SmartState.SmartState hover;
        public SmartState.SmartState down;
        public SmartState.SmartState selected;
        
        public static I Current;

        public abstract bool MonoSelectable();
        
        protected virtual void Awake()
        {
            Transform = transform as T;
            
            hover.Init();
            down.Init();
            selected.Init();
        }


        private void OnValidate()
        {
            hover.OnValidate();
            down.OnValidate();
            selected.OnValidate();
        }


        public void OnPointerEnter(PointerEventData eventData) => hover.Active.Value = true;


        public void OnPointerExit(PointerEventData eventData)
        {
            hover.Active.Value = false;
            down.Active.Value  = false;
        }


        public void OnPointerDown(PointerEventData eventData) => down.Active.Value = true;


        public void OnPointerUp(PointerEventData eventData)
        {
            if (down.Active.Value)
            {
                selected.Active.Value = true;

                if (MonoSelectable())
                {
                    if (Current)
                    {
                        Debug.Log("Outgoing " + Current.name);
                        
                        Current.selected.Active.Value = false;
                    }
                    
                    selected.Active.Value = true;

                    Current = this as I;
                }
                else
                {
                    selected.Active.Flip();
                }
            }

            down.Active.Value = false;
        }


    }

}