using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript
{
	
	[Serializable]
    public class TestLerp : Lerp
    {

        protected override bool CanAutoName() => false;


        protected override string CreateAutoName() => null;

        protected override void Init() { }


        protected override void Frame(float normal,
                                      float easedNormal) { }


        protected override void CleanUp() { }

    }
}
