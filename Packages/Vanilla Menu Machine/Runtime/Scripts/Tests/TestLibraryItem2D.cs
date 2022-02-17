using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Catalogue.Samples;

namespace Vanilla.MediaLibrary
{

	[Serializable]
	public class TestLibraryItem2D : LibraryItem2D<SampleCatalogueItem>
	{

		public override UniTask Populate(SampleCatalogueItem item)
		{
			Debug.Log($"[{item.Name}] lives!!");

			return default;
		}

	}

}