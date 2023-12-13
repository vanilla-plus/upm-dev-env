using System;

using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers
{

    [Serializable]
    public class Vec1DriverInstance : DriverInstance<float>
    {

        [SerializeField]
        public Vec1DriverSocket[] _sockets = Array.Empty<Vec1DriverSocket>();
        public override DriverSocket<float>[] Sockets => _sockets;

    }

    [Serializable]
    public class Vec1DriverSocket : DriverSocket<float>
    {

        [SerializeField]
        private Vec1Asset _asset;
        public override DataAsset<float> Asset => _asset;

            [SerializeReference]
        [TypeMenu("blue")]
        private Vec1Driver[] _drivers;
        public override Module<float>[] Modules => _drivers;

    }

    
}