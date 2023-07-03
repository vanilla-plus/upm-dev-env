using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.MetaScript.Three
{

	public class AccessorTaskTest : MonoBehaviour
	{

		public int rawGet;
		public int rawSet;

		public IntSocket socket;


		[ContextMenu("Get")]
		public void Get() => rawGet = socket.Get();


		[ContextMenu("Set")]
		public void Set() => socket.Set(rawSet);

	}

}