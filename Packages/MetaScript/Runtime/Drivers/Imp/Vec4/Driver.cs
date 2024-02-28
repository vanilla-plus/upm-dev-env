using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    
    [Serializable]
    public class Driver : Driver<Vector4>
    {

        [SerializeField]
        private Vec4Asset _asset;
        public override DataAsset<Vector4> Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module<Vector4>[] Modules => _modules;

    }
}
