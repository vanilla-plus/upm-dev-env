using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Float
{
    
    [Serializable]
    public class Driver : Driver<float>
    {

        [SerializeField]
        private FloatAsset _asset;
        public override DataAsset<float> Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module<float>[] Modules => _modules;

    }
}
