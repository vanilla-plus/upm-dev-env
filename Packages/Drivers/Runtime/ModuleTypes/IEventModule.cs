using UnityEngine.Events;

namespace Vanilla.Drivers.Snrubs
{

	public interface IEventSnrub<T>
	{

		UnityEvent<T> OnValueChange
		{
			get;
		}

	}
	
}