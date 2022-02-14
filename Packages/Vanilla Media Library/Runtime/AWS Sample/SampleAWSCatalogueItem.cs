using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MediaLibrary.Samples.AWS
{
    public class SampleAWSCatalogueItem : AWSCatalogueItem
    {

        public override async UniTask Initialize()
        {
            await base.Initialize();

            Debug.Log($"Sample AWS catalogue item [{RawData["name"]}] initialized!");
        }

    }
}
