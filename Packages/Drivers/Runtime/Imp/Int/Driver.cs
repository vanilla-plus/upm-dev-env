using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Int
{
    
	[Serializable]
	public class Driver : Driver<int>
	{

		[SerializeField]
		private IntAsset _asset;
		public override DataAsset<int> Asset => _asset;

		[SerializeReference]
		[TypeMenu("red")]
		private Module[] _modules = Array.Empty<Module>();
		public override Module<int>[] Modules => _modules;

	}
}