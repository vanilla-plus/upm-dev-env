using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataSources
{

	[Serializable]
	public class PositionToSource : MonoBehaviour
	{

		[SerializeReference]
		[TypeMenu("red")]
		public Vec3Source Source;

		void LateUpdate() => Source.Value = transform.position;

	}

}