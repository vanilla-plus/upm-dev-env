using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Float
{
    
    [Serializable]
    public class Driver : Driver<float, FloatSource, FloatAsset, Module, Driver>
    {

        [SerializeField]
        private FloatAsset _asset;
        public override FloatAsset Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module[] Modules => _modules;

    }
}
