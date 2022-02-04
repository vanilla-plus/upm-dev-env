#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vanilla.TypeMenu
{
	public interface IPortable
	{
		
		void Port(SerializedProperty previousInstance);

	}
}
