using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.Drivers.Modules
{

	public interface IVFXGraphModule
	{
		
		string PropertyName
		{
			get;
			set;
		}

		int PropertyID
		{
			get;
			set;
		}

		VisualEffect[] Graphs
		{
			get;
			set;
		}

	}

}