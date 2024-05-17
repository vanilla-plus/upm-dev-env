using System;
using System.IO;

namespace Vanilla.MetaScript.DataSources.Strings
{
	[Serializable]
    public class StringSource_Concat_Path : StringSource_Concat
    {
	    protected override string GetFreshValue => Elements.Length switch
	                                           {
		                                           0 => string.Empty,
		                                           1 => Elements[0].Value,
		                                           2 => Path.Combine(Elements[0].Value, Elements[1].Value),
		                                           3 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value),
		                                           4 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value, Elements[3].Value), 
		                                           5 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value, Elements[3].Value, Elements[4].Value),
		                                           6 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value, Elements[3].Value, Elements[4].Value, Elements[5].Value), 
		                                           7 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value, Elements[3].Value, Elements[4].Value, Elements[5].Value, Elements[6].Value),
		                                           8 => Path.Combine(Elements[0].Value, Elements[1].Value, Elements[2].Value, Elements[3].Value, Elements[4].Value, Elements[5].Value, Elements[6].Value, Elements[7].Value),
		                                           _ => "Only 8 concatenation slots available"
	                                           };
    }
}