using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Vanilla.MediaLibrary
{

    public interface ICatalogue<out I>
        where I : ICatalogueItem
    {

        int Version
        {
            get;
        }

        JObject RawData
        {
            get;
        }
        
        I[] Items
        {
            get;
        }

        int K();

        UniTask Initialize();

        UniTask PreFetch();

        UniTask PostFetch();
        
    }

}