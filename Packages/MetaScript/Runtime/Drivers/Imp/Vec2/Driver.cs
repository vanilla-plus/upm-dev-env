using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    
    [Serializable]
    public class Driver : Driver<Vector2>
    {

        [SerializeField]
        private Vec2Asset _asset;
        public override DataAsset<Vector2> Asset => _asset;

        [SerializeReference]
        [TypeMenu("red")]
        private Module[] _modules = Array.Empty<Module>();
        public override Module<Vector2>[] Modules => _modules;

    }
}
