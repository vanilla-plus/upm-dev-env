using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public abstract class BaseCatalogueItem : ICatalogueItem
	{

		protected JToken _rawData;
		public JToken RawData
		{
			get => _rawData;
			set => _rawData = value;
		}

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

		public abstract UniTask Initialize();

	}

}