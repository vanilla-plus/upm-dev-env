using UnityEngine;

namespace Vanilla.Drivers.Snrubs
{

	public interface IMaterialSnrub
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