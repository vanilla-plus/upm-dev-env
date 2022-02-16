using UnityEngine;

namespace Vanilla.Layout
{

	public class MenuBase<M, I> : MonoBehaviour
		where M : MenuBase<M, I>
		where I : MenuItemBase<M, I>
	{

		

	}

}