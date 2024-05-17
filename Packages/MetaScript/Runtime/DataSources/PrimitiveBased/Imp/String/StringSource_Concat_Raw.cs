using System;
using System.Linq;

namespace Vanilla.MetaScript.DataSources.Strings
{
	[Serializable]
	public class StringSource_Concat_Raw : StringSource_Concat
	{

		protected override string GetFreshValue => string.Concat(Elements.SelectMany(e => e.Value));

	}
}