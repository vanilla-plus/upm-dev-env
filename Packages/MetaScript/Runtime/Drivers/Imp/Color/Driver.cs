using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Color
{
    
    [Serializable]
    public class Driver : Driver<UnityEngine.Color,ColorSource, ColorAsset, Module, Driver>
    {

        [SerializeField]
        private ColorAsset _asset;
        public override ColorAsset Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module[] Modules => _modules;

    }
}
