using UnityEngine;

using Vanilla.Easing;

namespace Vanilla.MediaLibrary
{

    public class Tile2D : Tile<Tile2D, RectTransform>
    {

//        public TileFocus focusPanel;

        private Vector2 originalSize;

        public float hoverScalar  = 1.0f;
        public float selectScalar = 1.0f;

        public override bool MonoSelectable() => true;


        protected override void Awake()
        {
            base.Awake();

            originalSize = Transform.sizeDelta;

//            focusPanel = GetComponentInParent<TileFocus>();

            hover.Normal.OnChange    += _ => Resize();
            selected.Normal.OnChange += _ => Resize();

//            selected.Toggle.onTrue   += () => focusPanel.ChangeTarget(newTarget: _rect);
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

            Transform.sizeDelta = finalSize;

            Canvas.ForceUpdateCanvases();
        }

    }

}