using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Vanilla.Catalogue
{

	[Serializable]
	public abstract class CatalogueItem : ICatalogueItem
	{

		[NonSerialized]
		protected JToken _data;
		public    JToken Data => _data;

		public virtual UniTask Initialize(JToken data)
		{
			_data = data;

			return default;
		}

	}

}