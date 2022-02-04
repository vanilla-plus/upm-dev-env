using UnityEngine;

namespace Vanilla.MenuMachine
{

	public class MenuBase<M, I> : MonoBehaviour
		where M : MenuBase<M, I>
		where I : MenuItemBase<M, I>
	{

		

	}

}