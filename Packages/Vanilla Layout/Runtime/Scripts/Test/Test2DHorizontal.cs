using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Layout
{

    public class Test2DHorizontal : TestLayout2D<Layout2DHorizontal>
    {

	    protected async UniTask Start()
	    {
		    foreach (var i in layout.Items)
		    {
			    i.Selected.State = true;

			    await UniTask.Delay(500);
		    }
	    }

    }

}