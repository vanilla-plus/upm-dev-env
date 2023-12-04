using System;

using UnityEngine;

namespace Vanilla.TypeMenu
{

	[AttributeUsage(validOn: AttributeTargets.Field)]
	public class TypeMenuAttribute : PropertyAttribute
	{

		public readonly Color color;

		public TypeMenuAttribute(string color) => this.color = color switch
		                                                       {
			                                                       "red"     => new Color(0.75f,
			                                                                              0.38f,
			                                                                              0.39f),
			                                                       "green"   => new Color(0.38f,
			                                                                              0.75f,
			                                                                              0.41f),
			                                                       "blue"    => new Color(0.5f,
			                                                                              0.85f,
			                                                                              1f),
			                                                       "cyan"    => new Color(0.38f,
			                                                                              0.75f,
			                                                                              0.71f),
			                                                       "magenta" => new Color(1f,
			                                                                              0.5f,
			                                                                              1f),
			                                                       "yellow"  => new Color(0.75f,
			                                                                              0.75f,
			                                                                              0.38f),
			                                                       _         => new Color(0.74f,
			                                                                              0.74f,
			                                                                              0.74f),
		                                                       };

	}

}