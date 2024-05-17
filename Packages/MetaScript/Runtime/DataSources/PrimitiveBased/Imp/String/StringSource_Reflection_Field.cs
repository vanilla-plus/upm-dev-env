using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Strings
{

	[Serializable]
	public class StringSource_Reflection_Field : StringSource_Reflection
	{

		protected override string TryAccess(Type t)
		{
			var p = t.GetField(name: TargetAccessorName,
			                   bindingAttr: Flags);

			if (p == null)
			{
				Debug.LogError($"Failed to find the specified field: {TargetAccessorName}");

				return Utility.Unknown;
			}

			var v = p.GetValue(null);

			if (v == null)
			{
				Debug.LogError($"The value of the field '{TargetAccessorName}' is null.");

				return Utility.Unknown;
			}

			return (string) v;
		}

	}

}