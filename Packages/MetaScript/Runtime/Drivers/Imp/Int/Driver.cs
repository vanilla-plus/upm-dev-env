using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Int
{
    
	[Serializable]
	public class Driver : Driver<int,IntSource, IntAsset, Module, Driver>
	{

		[SerializeField]
		private IntAsset _asset;
		public override IntAsset Asset => _asset;

		[SerializeReference]
		[TypeMenu("red")]
		private Module[] _modules = Array.Empty<Module>();
		public override Module[] Modules => _modules;

	}
}