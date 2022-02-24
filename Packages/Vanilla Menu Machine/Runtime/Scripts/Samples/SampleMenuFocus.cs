using UnityEngine;

namespace Vanilla.MediaLibrary.Samples
{

    public class SampleMenuFocus : MonoBehaviour
    {

        public SampleLibrary library;
        public Toggle        arrangementInProgress;

        public RectTransform _rect;

        public Toggle focusInProgress = new(startingState: false);

        [Header(header: "Settings")]
        public float smoothTime = 0.1666f;
        public float smoothDistanceEpsilon = 0.001f;

        [Header(header: "State")]
        public float smoothPosition;
        public float smoothPositionTarget;
        public float smoothVelocity;

        public float smoothDistance;

        public RectTransform targetRect;


        void Awake()
        {
            _rect = (RectTransform) transform;

            arrangementInProgress = library.Arrangement.ArrangementInProgress;

            SampleLibraryItem.OnSelectedChange += SelectedItemChanged;

            arrangementInProgress.onTrue += () =>
                                            {
                                                focusInProgress.State = true;
                                                enabled               = true;
                                            };
        }


        void LateUpdate()
        {
            if (!targetRect)
            {
                enabled = false;

                return;
            }

            smoothPositionTarget = targetRect.anchoredPosition.x;

            smoothPosition = Mathf.SmoothDamp(current: smoothPosition,
                                              target: smoothPositionTarget,
                                              currentVelocity: ref smoothVelocity,
                                              smoothTime: smoothTime);

            _rect.anchoredPosition = new Vector2(x: -smoothPosition,
                                                 y: 0.0f);

            smoothDistance = Mathf.Abs(f: smoothPositionTarget - smoothPosition);

            if (arrangementInProgress.State
             || smoothDistance > smoothDistanceEpsilon) return;

            focusInProgress.State = false;
            enabled               = false;
        }


        private void SelectedItemChanged(SampleLibraryItem outgoing,
                                         SampleLibraryItem incoming)
        {
            if (!incoming)
            {
                targetRect = null;

                return;
            }

            targetRect = incoming.Transform;
        }

    }

}