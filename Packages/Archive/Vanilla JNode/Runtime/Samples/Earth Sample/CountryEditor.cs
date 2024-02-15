using Vanilla.JNode.Samples;

namespace Vanilla.JNode
{

    public class CountryEditor : JNodeEditor<Country>
    {

        
        protected override string GetName() => data.Name;

    }
}
