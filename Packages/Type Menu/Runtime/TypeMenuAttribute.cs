using System;

using UnityEngine;

namespace Vanilla.TypeMenu
{

	[AttributeUsage(validOn: AttributeTargets.Field)]
	public class TypeMenuAttribute : PropertyAttribute { }

}