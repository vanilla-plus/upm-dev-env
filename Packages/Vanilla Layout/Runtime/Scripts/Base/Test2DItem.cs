using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Layout
{
    public class Test2DItem : ILayoutItem<RectTransform>
    {

        private bool _isDirty;
        public bool IsDirty => _isDirty;


        public RectTransform Transform() => null;

    }
}
