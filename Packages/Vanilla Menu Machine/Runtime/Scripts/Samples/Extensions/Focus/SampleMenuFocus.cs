using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MediaLibrary.Samples
{

    public class SampleMenuFocus : MonoBehaviour
    {

        public SampleLibrary library;
        public Toggle        arrangementInProgress;

        public RectTransform _rect;

        public Toggle focusInProgress = new(startingState: false);

        public Toggle lockedOn = new(startingState: false);

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
        }


        private void OnEnable()
        {
            SampleLibraryItem.OnSelectedChange += SelectedItemChanged;
            arrangementInProgress.onTrue       += HandleArrangeInProgress;
            focusInProgress.onTrue             += HandleFocus;

            // Let's fetch whatever is currently selected and focus on that.

            SelectedItemChanged(outgoing: null,
                                incoming: SampleLibraryItem.Selected);
        }


        private void OnDisable()
        {
            SampleLibraryItem.OnSelectedChange -= SelectedItemChanged;
            arrangementInProgress.onTrue       -= HandleArrangeInProgress;
            focusInProgress.onTrue             -= HandleFocus;
        }


        private void HandleArrangeInProgress()
        {
            if (targetRect                 != null
             || SampleLibraryItem.Selected != null)
            {
                focusInProgress.State = true;
            }
        }

        private void HandleFocus()
        {
            FocusAsync();
        }

        private async UniTask FocusAsync()
        {
            do
            {
                await UniTask.Yield(timing: PlayerLoopTiming.LastUpdate);

//                if (targetRect == null)
//                {
//                    focusInProgress.State = false;
//
//                    return;
//                }
                
                smoothPositionTarget = targetRect.anchoredPosition.x;

                // How far away are we from the target position?

                smoothDistance = Mathf.Abs(f: smoothPositionTarget - smoothPosition);

                // If we're close enough, let's 'lock on' to the target position.

                if (smoothDistance < smoothDistanceEpsilon)
                {
                    lockedOn.State = true;
                }

                // We only want to smoothly transition over to the target position if we're far away; not after we've arrived.

                if (lockedOn)
                {
                    smoothPosition = smoothPositionTarget;
                }
                else
                {
                    smoothPosition = Mathf.SmoothDamp(current: smoothPosition,
                                                      target: smoothPositionTarget,
                                                      currentVelocity: ref smoothVelocity,
                                                      smoothTime: smoothTime);
                }

                _rect.anchoredPosition = new Vector2(x: -smoothPosition,
                                                     y: 0.0f);
            }
            while (arrangementInProgress.State
                || !lockedOn.State);

            focusInProgress.State = false;
        }

        private void SelectedItemChanged(SampleLibraryItem outgoing,
                                         SampleLibraryItem incoming)
        {
            if (incoming == null)
            {
                targetRect = null;

                return;
            }

            lockedOn.State = false;

            targetRect = incoming.Transform;

            focusInProgress.State = true;
        }

    }

}