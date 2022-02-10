using System;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MenuMachine.Samples
{

	public enum FetchMethod
	{

		RemoteConfig,
		LocalFile,
		WebRequest

	}

	[Serializable]
	public class TestApp : MonoBehaviour
	{

		public static FetchMethod fetchMethod = FetchMethod.WebRequest;

		public static TestCatalogue Catalogue = new();

		public TestCatalogue local;


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static async UniTask Init()
		{
			switch (fetchMethod)
			{
				case FetchMethod.RemoteConfig:
					await MenuMachine.FetchViaRemoteConfig<TestCatalogue, TestCatalogueItem>(catalogue: Catalogue);

					break;

				case FetchMethod.LocalFile:
					await MenuMachine.FetchViaLocalFile<TestCatalogue, TestCatalogueItem>(catalogue: Catalogue,
					                                                                      path: Application.dataPath + "/manifest.json");

					break;

				case FetchMethod.WebRequest:
					await MenuMachine.FetchViaWebRequest<TestCatalogue, TestCatalogueItem>(catalogue: Catalogue,
					                                                                       url: "https://vanilla-plus.neocities.org/phoriajunk/manifest.json");

					break;

				default: throw new ArgumentOutOfRangeException();
			}
		}


		void Awake() => MenuMachine.OnCatalogueFetchSuccess += OnCatalogueFetchSuccess;

		void OnCatalogueFetchSuccess() => local = Catalogue;

	}

}