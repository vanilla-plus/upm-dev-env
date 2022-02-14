using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MediaLibrary.Samples.AWS
{

    public class SampleAWSApp : MonoBehaviour
    {

        private SampleAWSCatalogue _catalogue;
        public  SampleAWSCatalogue Catalogue => _catalogue;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public async UniTask Initialize()
        {
            await MediaLibrary.FetchViaRemoteConfig<SampleAWSCatalogue, SampleAWSCatalogueItem>(_catalogue,
                                                                                                "");

            MediaLibrary.OnCatalogueFetchBegun   += () => Debug.LogError("Fetch started :)");
            MediaLibrary.OnCatalogueFetchFailed  += () => Debug.LogError("Fetch failed :(");
            MediaLibrary.OnCatalogueFetchSuccess += () => Debug.LogError("Fetch successful :A");
            MediaLibrary.OnCatalogueUpToDate     += () => Debug.LogError("Nothing new to fetch :/");
        }

    }

}