using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Vec4
{
    
    [Serializable]
    public class Driver : Driver<Vector4,Vec4Source, Vec4Asset, Module, Driver>
        
    {

        [SerializeField]
        private Vec4Asset _asset;
        public override Vec4Asset Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module[] Modules => _modules;

    }
}
