using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Layout
{

    public class Test2DHorizontal : TestLayout<Layout2DHorizontal, LayoutItem2D, RectTransform, Vector2>
    {

        [ContextMenu("Arrange Async")]
        public void AttemptArrange() => layout.AttemptArrange();


        [ContextMenu("Reset hasChanged")]
        public void ResetHasChanged()
        {
            foreach (var t in layout.Transforms)
            {
                t.hasChanged = false;
            }
        }


        [ContextMenu("Animate")]
        public void AnimateTest()
        {
            layout.AnimateTest();
            
            layout.AttemptArrange();
        }

    }

}