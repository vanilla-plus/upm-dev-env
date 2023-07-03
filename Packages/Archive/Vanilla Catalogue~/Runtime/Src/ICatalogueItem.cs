using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Vanilla.Catalogue
{

	public interface ICatalogueItem
	{

		JToken Data
		{
			get;
		}
		
		UniTask Initialize(JToken data);

	}

}