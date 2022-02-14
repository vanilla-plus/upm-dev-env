using Cysharp.Threading.Tasks;

using static UnityEngine.Debug;

namespace Vanilla.MediaLibrary.Samples.AWS
{

    public class SampleAWSCatalogue : AWSCatalogue<SampleAWSCatalogueItem>
    {

        public override int K() => 925810763;

        public override async UniTask PreFetch()
        {
            Log(message: "Sample AWS catalogue handling pre-fetch...");

            await UniTask.Delay(millisecondsDelay: 500);

            Log(message: "Done!");
        }


        public override async UniTask PostFetch()
        {
            Log(message: "Sample AWS catalogue handling post-fetch...");

            await UniTask.Delay(millisecondsDelay: 500);

            Log(message: "Done!");

        }

    }

}