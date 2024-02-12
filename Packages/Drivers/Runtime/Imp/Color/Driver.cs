using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Color
{
    
    [Serializable]
    public class Driver : Driver<UnityEngine.Color>
    {

        [SerializeField]
        private ColorAsset _asset;
        public override DataAsset<UnityEngine.Color> Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module<UnityEngine.Color>[] Modules => _modules;

    }
}
