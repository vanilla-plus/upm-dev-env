using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    
    [Serializable]
    public class Driver : Driver<Vector2, Vec2Source, Vec2Asset, Module, Driver>
    {

        [SerializeField]
        private Vec2Asset _asset;
        public override Vec2Asset Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module[] Modules => _modules;

    }
}
