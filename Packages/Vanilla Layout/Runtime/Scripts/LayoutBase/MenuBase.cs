using UnityEngine;

namespace Vanilla.MediaLibrary
{

	public class MenuBase<M, I> : MonoBehaviour
		where M : MenuBase<M, I>
		where I : MenuItemBase<M, I>
	{

		

	}

}