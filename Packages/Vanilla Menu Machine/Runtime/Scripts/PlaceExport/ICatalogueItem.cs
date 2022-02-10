using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Vanilla.MenuMachine
{

	public interface ICatalogueItem
	{

		JToken RawData
		{
			get;
			set;
		}
		
		UniTask Initialize();

		string Name
		{
			get;
		}

		bool Available
		{
			get;
		}

	}

}