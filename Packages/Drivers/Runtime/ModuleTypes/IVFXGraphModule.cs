using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.Drivers.Snrubs
{

	public interface IVFXGraphSnrub
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