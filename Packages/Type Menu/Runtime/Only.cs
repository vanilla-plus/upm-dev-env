using System;

using UnityEngine;

namespace Vanilla.TypeMenu
{

	[AttributeUsage(validOn: AttributeTargets.Field)]
	public class Only : PropertyAttribute
	{

		public readonly Type[] Types;

		public Only(params Type[] types) => Types = types;

	}

}