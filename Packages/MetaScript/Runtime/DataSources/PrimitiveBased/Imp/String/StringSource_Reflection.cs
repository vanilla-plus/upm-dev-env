using System;
using System.Reflection;

using UnityEngine;

using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.MetaScript
{
	[Serializable]
	public abstract class StringSource_Reflection : StringSource_Bakable
	{
		[SerializeField] public string TargetAssemblyName = "UnityEngine";
		[SerializeField] public string TargetClassName    = "UnityEngine.Application";
		[SerializeField] public string TargetAccessorName    = "streamingAssetsPath";

		[SerializeField] public BindingFlags Flags = BindingFlags.Static | BindingFlags.Public;

		protected override string GetFreshValue
		{
			get
			{
				var assemblyQualifiedName = $"{TargetClassName}, {TargetAssemblyName}";

				var t = Type.GetType(assemblyQualifiedName);

				if (t == null)
				{
					Debug.LogError($"Failed to find the specified type: {assemblyQualifiedName}");
					
					return Utility.Unknown;
				}

				return TryAccess(t);
			}
		}


		protected abstract string TryAccess(Type t);

		public override string ToString() => $"{TargetClassName}.{TargetAccessorName} ({TargetAssemblyName})";

	}
}