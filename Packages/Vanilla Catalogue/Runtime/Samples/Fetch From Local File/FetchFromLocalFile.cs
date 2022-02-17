using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Catalogue.Samples
{

    [Serializable]
    public class FetchFromLocalFile : MonoBehaviour
    {

        public static string fallback = "{\"_version\":0,\"_items\":[{\"_name\":\"Antarctica\",\"_available\":true,\"duration\":500,\"borderColor\":[1.0,1.0,1.0,1.0],\"latLong\":[-76.27476501464844,22.77211570739746],\"rawDataKeyTest\":\"duration\"},{\"_name\":\"Borneo\",\"_available\":true,\"duration\":500,\"borderColor\":[0.5,1.0,0.5,1.0],\"latLong\":[-1.6898059844970704,113.38179016113281],\"rawDataKeyTest\":\"duration\"},{\"_name\":\"Costa Rica\",\"_available\":true,\"duration\":500,\"borderColor\":[0.5,0.5,1.0,1.0],\"latLong\":[10.453800201416016,-84.75909423828125],\"rawDataKeyTest\":\"duration\"}]}";

        public static SampleCatalogue Catalogue = new();

        public SampleCatalogue catalogue;


        //public async UniTask Init() => Fetch();
	    
        [ContextMenu("Fetch")]
        public async void Fetch() => await CatalogueBuilder.FetchViaLocalFile<SampleCatalogue, SampleCatalogueItem>(catalogue: Catalogue,
                                                                                                                path: Application.dataPath + "/Samples/Vanilla Media Library/1.0.0/Sample Catalogue/manifest.json");

        void Awake() => CatalogueBuilder.OnCatalogueFetchSuccess += () => catalogue = Catalogue;

    }

}