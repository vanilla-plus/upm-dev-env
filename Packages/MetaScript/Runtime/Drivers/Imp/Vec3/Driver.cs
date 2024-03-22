using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Vec3
{
    
    [Serializable]
    public class Driver : Driver<Vector3, Vec3Source, Vec3Asset, Module, Driver>
    {

        [SerializeField]
        private Vec3Asset _asset;
        public override Vec3Asset Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module[] Modules => _modules;

    }
}
