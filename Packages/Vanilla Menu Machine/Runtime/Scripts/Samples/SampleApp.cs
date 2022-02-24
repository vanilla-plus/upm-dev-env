using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary.Samples
{

	public static class SampleApp
	{

		public static SampleCatalogue catalogue = new();


		[RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.AfterSceneLoad)]
		public static async UniTask Init() => await CatalogueBuilder.FetchViaRemoteConfig<SampleCatalogue, SampleCatalogueItem>(catalogue: catalogue,
		                                                                                                                        rootKey: "manifest",
		                                                                                                                        itemArrayKey: "_items",
		                                                                                                                        fallback: string.Empty);

	}

}