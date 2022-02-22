using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Catalogue.Samples
{

    [Serializable]
    public class SampleCatalogueItem : CatalogueItem
    {

        public int duration = 500;

        public float[] borderColor = {
                                         1.0f,
                                         1.0f,
                                         1.0f,
                                         1.0f
                                     };
        public Color BorderColor =>
            new(r: borderColor[0],
                g: borderColor[1],
                b: borderColor[2],
                a: 1.0f);

        public float[] latLong =
        {
            10.0f,
            20.0f
        };

        public Vector2 LatLong =>
            new(x: latLong[0],
                y: latLong[1]);

        public string rawDataKeyTest = "duration";

        public override async UniTask Initialize()
        {
//            await UniTask.Delay(500);

            Debug.Log($"{_name} Initialized! Here, have some adhoc data matching the key [{rawDataKeyTest}] => [{RawData[rawDataKeyTest]}]");
        }


    }

}