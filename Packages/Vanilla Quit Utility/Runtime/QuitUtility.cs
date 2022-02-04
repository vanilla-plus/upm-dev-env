using UnityEngine;

namespace Vanilla.QuitUtility
{

	public static class Quit
	{

		public static bool InProgress;

		[RuntimeInitializeOnLoadMethod]
		static void ListenForQuit() => Application.quitting += () => InProgress = true;

	}

}