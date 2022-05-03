using Vanilla.JNode.Samples;

namespace Vanilla.JNode
{

	public class ContinentEditor : JNodeEditor<Continent>
	{

		protected override string GetName() => data.Name;

	}
	
}