using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary.Samples
{

	[Serializable]
	public class SampleCatalogueItem : CatalogueItem
	{

		[SerializeField]
		protected string _name;
		public string Name
		{
			get => _name;
			private set => _name = value;
		}

		[SerializeField]
		protected bool _available = true;
		public bool Available
		{
			get => _available;
			private set => _available = value;
		}
		
		public int duration = 500;

		public float[] borderColor = {
			                             1.0f,
			                             1.0f,
			                             1.0f,
			                             1.0f
		                             };
		public Color BorderColor =>
			new(r: borderColor[0],
			    g: borderColor[1],
			    b: borderColor[2],
			    a: 1.0f);

		public float[] latLong =
		{
			10.0f,
			20.0f
		};

		public Vector2 LatLong =>
			new(x: latLong[0],
			    y: latLong[1]);

		public string rawDataKeyTest = "duration";

		[SerializeField]
		public int _hash = -1;
		
		public override async UniTask Initialize(JToken data)
		{
			base.Initialize(data: data);
			
			_hash = GetHashCode();
			
//            await UniTask.Delay(500);

			Debug.Log(message: $"{_name} Initialized! Here, have some adhoc data matching the key [{rawDataKeyTest}] => [{Data[key: rawDataKeyTest]}]");
		}


	}

}