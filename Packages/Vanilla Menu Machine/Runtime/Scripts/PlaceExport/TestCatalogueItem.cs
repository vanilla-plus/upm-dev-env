using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.MenuMachine.Samples
{

	[Serializable]
	public class TestCatalogueItem : ICatalogueItem
	{

		private JToken _rawData;
		public JToken RawData
		{
			get => _rawData;
			set => _rawData = value;
		}

		[SerializeField]
		private string _name;
		public string Name => _name;

		[SerializeField]
		private bool _available;
		public bool Available => _available;
		
		public int     duration;
		
		public float[] borderColor;
		public Color BorderColor =>
			new(r: borderColor[0],
			    g: borderColor[1],
			    b: borderColor[2],
			    a: 1.0f);

		public float[] latLong;
		public Vector2 LatLong =>
			new(x: latLong[0],
			    y: latLong[1]);

		public string rawDataKeyTest = "duration";

		public async UniTask Initialize()
		{
			await UniTask.Delay(500);

			Debug.Log($"{_name} Initialized!");

			Debug.Log($"Here, have some adhoc data matching the key [{rawDataKeyTest}] => [{RawData[rawDataKeyTest]}]");
		}



	}

}