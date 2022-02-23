using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Catalogue.Samples.AWS
{

    public class SampleAWSApp : MonoBehaviour
    {

        private SampleAWSCatalogue _catalogue;
        public  SampleAWSCatalogue Catalogue => _catalogue;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public async UniTask Initialize()
        {
            await CatalogueBuilder.FetchViaRemoteConfig<SampleAWSCatalogue, SampleAWSCatalogueItem>(catalogue: _catalogue,
                                                                                                    rootKey: "manifest",
                                                                                                    itemArrayKey: "_items",
                                                                                                    fallback: "");

            CatalogueBuilder.OnCatalogueFetchBegun   += () => Debug.LogError("Fetch started :)");
            CatalogueBuilder.OnCatalogueFetchFailed  += () => Debug.LogError("Fetch failed :(");
            CatalogueBuilder.OnCatalogueFetchSuccess += () => Debug.LogError("Fetch successful :A");
            CatalogueBuilder.OnCatalogueUpToDate     += () => Debug.LogError("Nothing new to fetch :/");
        }

    }

}