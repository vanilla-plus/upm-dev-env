using System;

using Cysharp.Threading.Tasks;

using static UnityEngine.Debug;

namespace Vanilla.Catalogue.Samples
{

    [Serializable]
    public class SampleCatalogue : Catalogue<SampleCatalogueItem>
    {

        public override int K() => 100;


        public override async UniTask Initialize()
        {
            Log(message: "Sample catalogue handling initialization...");

//            await UniTask.Delay(millisecondsDelay: 500);

            Log(message: "Done!");
        }


        public override async UniTask PreFetch()
        {
            Log(message: "Sample catalogue handling pre-fetch...");

//            await UniTask.Delay(millisecondsDelay: 500);

            Log(message: "Done!");
        }


        public override async UniTask PostFetch()
        {
            Log(message: "Sample catalogue handling post-fetch...");

//            await UniTask.Delay(millisecondsDelay: 500);

            Log(message: "Done!");

        }

    }

}