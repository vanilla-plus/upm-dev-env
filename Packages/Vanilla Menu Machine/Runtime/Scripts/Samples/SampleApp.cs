using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Catalogue.Samples;

namespace Vanilla.MediaLibrary.Samples
{

	public static class SampleApp
	{

		public static SampleCatalogue catalogue = new();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static async UniTask Init() => await CatalogueBuilder.FetchViaRemoteConfig<SampleCatalogue, SampleCatalogueItem>(catalogue: catalogue,
		                                                                                                                        fallback: string.Empty);

	}

}