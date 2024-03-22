using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Bool
{
    
	[Serializable]
	public class Driver : Driver<bool, BoolSource, BoolAsset, Module, Driver>
	{

		[SerializeField]
		private BoolAsset _asset;
		public override BoolAsset Asset => _asset;

		[SerializeReference]
		[TypeMenu("red")]
		private Module[] _modules = Array.Empty<Module>();
		public override Module[] Modules => _modules;

	}
}