#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vanilla.TypeMenu
{
	public interface IPortable
	{
		
		#if UNITY_EDITOR
		void Port(SerializedProperty previousInstance);
		#endif

	}
}