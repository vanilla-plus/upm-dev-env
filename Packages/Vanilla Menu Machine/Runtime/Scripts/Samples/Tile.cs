using UnityEngine;
using UnityEngine.EventSystems;

using Vanilla.Easing;

namespace Vanilla.MediaLibrary
{

    public class Tile : MonoBehaviour,
                            IPointerEnterHandler,
                            IPointerExitHandler,
                            IPointerDownHandler,
                            IPointerUpHandler
    {

        private RectTransform _rect;

        public TileFocus focusPanel;

        public State hover;
        public State down;
        public State selected;

        private Vector2 originalSize;

        public float hoverScalar  = 1.0f;
        public float selectScalar = 1.0f;


        void Awake()
        {
            _rect        = (RectTransform) transform;
            originalSize = _rect.sizeDelta;
            
            hover.Init();
            down.Init();
            selected.Init();

            focusPanel = GetComponentInParent<TileFocus>();

            selected.Toggle.onTrue += () => focusPanel.ChangeTarget(newTarget: _rect);

            hover.Normal.OnChange    += _ => Resize();
            selected.Normal.OnChange += _ => Resize();
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
//            if (down.Toggle.State) selected.Toggle.Flip();
            if (down.Toggle.State) focusPanel.ChangeTarget(newTarget: _rect);

            down.Toggle.State = false;
        }


        public void Resize()
        {
            // Hovering over elements inside the tile count as de-hover.
            // We should only factor in the hover normal if the tile isn't selected.
            var hoverPadding = (selected.Toggle.State ?
                                    hoverScalar :
                                    hover.Normal.Value.InOutPower(2.0f) * hoverScalar)
                             * originalSize;
            
            var selectPadding = selected.Normal.Value.InOutPower(2.0f) * selectScalar * originalSize;

            var finalSize = originalSize + hoverPadding + selectPadding;

            finalSize.y = originalSize.y;
            
            _rect.sizeDelta = finalSize;
            
            Canvas.ForceUpdateCanvases();
        }

    }

}