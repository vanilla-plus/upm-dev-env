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
        
        public State hover;
        public State down;
        public State selected;
        
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


        public void OnPointerEnter(PointerEventData eventData) => hover.Toggle.State = true;


        public void OnPointerExit(PointerEventData eventData)
        {
            hover.Toggle.State = false;
            down.Toggle.State  = false;
        }


        public void OnPointerDown(PointerEventData eventData) => down.Toggle.State = true;


        public void OnPointerUp(PointerEventData eventData)
        {
            if (down.Toggle.State)
            {
                selected.Toggle.State = true;

                if (MonoSelectable())
                {
                    if (Current)
                    {
                        Debug.Log("Outgoing " + Current.name);
                        
                        Current.selected.Toggle.State = false;
                    }
                    
                    selected.Toggle.State = true;

                    Current = this as I;
                }
                else
                {
                    selected.Toggle.Flip();
                }
            }

            down.Toggle.State = false;
        }


    }

}