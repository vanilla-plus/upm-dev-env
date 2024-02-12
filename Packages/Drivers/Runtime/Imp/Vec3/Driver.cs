using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Vec3
{
    
    [Serializable]
    public class Driver : Driver<Vector3>
    {

        [SerializeField]
        private Vec3Asset _asset;
        public override DataAsset<Vector3> Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module<Vector3>[] Modules => _modules;

    }
}
