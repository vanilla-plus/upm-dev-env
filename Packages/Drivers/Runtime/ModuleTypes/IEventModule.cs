using UnityEngine.Events;

namespace Vanilla.Drivers.Modules
{

	public interface IEventModule<T>
	{

		UnityEvent<T> OnValueChange
		{
			get;
		}

	}
	
}