using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.SchrodingerAssets.Samples
{

    public class SampleA : MonoBehaviour
    {

        public SchrodingerAsset assetA;
        public SchrodingerAsset assetB;

        public SampleA somethingA;
        public float   somethingB;


        [ContextMenu("Get A")]
        public void GetA() => somethingA = assetA.payloads[0].Get<SampleA>();


        [ContextMenu("Set A")]
        public void SetA() => assetA.payloads[0].Set(input: this);


        [ContextMenu("Get B")]
        public void GetB() => somethingB = assetB.payloads[0].Get<float>();


        [ContextMenu("Set B")]
        public void SetB() => assetB.payloads[0].Set(input: somethingB);

    }

}