using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Vanilla.MediaLibrary
{

	public interface ICatalogueItem
	{

		JToken RawData
		{
			get;
			set;
		}
		
		string Name
		{
			get;
		}

		bool Available
		{
			get;
		}
		
		UniTask Initialize();

	}

}