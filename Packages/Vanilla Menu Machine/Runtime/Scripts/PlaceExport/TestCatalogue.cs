using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MenuMachine.Samples
{

	[Serializable]
	public class TestCatalogue : ICatalogue<TestCatalogueItem>
	{

		[SerializeField]
		private bool _initialized;
		public bool Initialized
		{
			get => _initialized;
			set => _initialized = value;
		}

		public int version;

		[SerializeField]
		private TestCatalogueItem[] _items;
		public TestCatalogueItem[] Items
		{
			get => _items;
			set => _items = value;
		}

		public string DefaultRemoteConfig => "{\r\n\"version\":12,\r\n\"_items\":[\r\n{\r\n\"name\":\"Forests\",\r\n\"available\":true,\r\n\"duration\":15873752,\r\n\"borderColor\":[\r\n1.0,\r\n0.0,\r\n0.0\r\n],\r\n\"latLong\":[\r\n5.22,\r\n1.298\r\n]\r\n},\r\n{\r\n\"name\":\"Oceans\",\r\n\"available\":false,\r\n\"duration\":4378572,\r\n\"borderColor\":[\r\n0.0,\r\n1.0,\r\n0.0\r\n],\r\n\"latLong\":[\r\n2.1247,\r\n3.4821\r\n]\r\n},\r\n{\r\n\"name\":\"Antarctica\",\r\n\"available\":true,\r\n\"duration\":782749,\r\n\"borderColor\":[\r\n1.0,\r\n1.0,\r\n1.0\r\n],\r\n\"latLong\":[\r\n0,\r\n0\r\n]\r\n}\r\n]\r\n}";

		public async UniTask Initialize()
		{
			await UniTask.Delay(500);

			Debug.Log("Catalogue initialized!");
		}


		public async UniTask Update()
		{
			await UniTask.Delay(500);

			Debug.Log("Catalogue updated!");
		}

	}

}