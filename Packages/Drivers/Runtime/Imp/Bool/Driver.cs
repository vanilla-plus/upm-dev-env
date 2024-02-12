using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Bool
{
    
	[Serializable]
	public class Driver : Driver<bool>
	{

		[SerializeField]
		private BoolAsset _asset;
		public override DataAsset<bool> Asset => _asset;

		[SerializeReference]
		[TypeMenu("red")]
		private Module[] _modules = Array.Empty<Module>();
		public override Module<bool>[] Modules => _modules;

	}
}