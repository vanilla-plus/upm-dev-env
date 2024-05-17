using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class TransformToAsset : MonoBehaviour
	{

		[SerializeField]
		public TransformAsset Asset;

		void OnEnable()
		{
			if (Asset        != null &&
			    Asset.Source != null)
			{
				Asset.Source.Value = transform;
			}
		}


		void OnDisable()
		{
			if (ReferenceEquals(objA: Asset.Source.Value,
			                    objB: transform)) Asset.Source.Value = null;
		}

	}

}