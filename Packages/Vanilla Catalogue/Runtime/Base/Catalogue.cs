using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.Catalogue
{

	[Serializable]
	public abstract class Catalogue<I> : ICatalogue<I> 
		where I : ICatalogueItem
	{

		[Range(min: -1, 
		       max: 100)]
		[SerializeField]
		private int _version = -1;
		public int Version => _version;
		
		private JObject _rawData;
		public  JObject RawData => _rawData;

		[SerializeField]
		private I[] _items;
		public I[] Items => _items;
		
		public abstract int K();

		public abstract UniTask Initialize();
		
		public abstract UniTask PreFetch();

		public abstract UniTask PostFetch();

	}

}