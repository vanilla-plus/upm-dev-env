using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Strings
{

	[Serializable]
	public class StringSource_Reflection_Method : StringSource_Reflection
	{

		protected override string TryAccess(Type t)
		{
			var m = t.GetMethod(name: TargetAccessorName,
			                    bindingAttr: Flags);

			if (m == null)
			{
				Debug.LogError($"Failed to find the specified method: {TargetAccessorName}");

				return Utility.Unknown;
			}


			var v = m.Invoke(null,
			                 null);

			if (v == null)
			{
				Debug.LogError($"The return value of the method '{TargetAccessorName}' is null.");

				return Utility.Unknown;
			}


			return (string) v;

		}

	}

}