using System;

using UnityEngine;
using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Vec2
{
    [Serializable]
    public class Unity_Event : Module
    {

        public UnityEvent<Vector2> OnSet = new();

        public override void OnValidate(Driver<Vector2> driver) => OnSet.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<Vector2> driver) => TryConnectSet(driver);
        public override void DeInit(Driver<Vector2> driver) => TryDisconnectSet(driver);

        protected override void HandleSet(Vector2 value) => OnSet.Invoke(value);

    }
}
