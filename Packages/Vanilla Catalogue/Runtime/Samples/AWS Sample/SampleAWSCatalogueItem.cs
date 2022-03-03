using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.Catalogue.Samples.AWS
{
    public class SampleAWSCatalogueItem : AWSCatalogueItem
    {

        public override async UniTask Initialize(JToken data)
        {
            await base.Initialize(data: data);

            Debug.Log($"Sample AWS catalogue item [{Data["name"]}] initialized!");
        }

    }
}
