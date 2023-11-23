using UnityEngine;

namespace Vanilla.Drivers.Modules
{

	public interface IMaterialModule
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

		Material[] Materials
		{
			get;
		}

	}

}