using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataBinders
{

    [Serializable]
    public class TransformAssetBinder : ReferenceBinder<Transform, ITransformSource, TransformAsset>
    {

        protected override Transform Assign => transform;

    }

}