using System;

using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Color
{
    [Serializable]
    public class Unity_Event : Module
    {

        public UnityEvent<UnityEngine.Color> OnSet = new();

        public override void OnValidate(Driver<UnityEngine.Color> driver) => OnSet.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<UnityEngine.Color> driver) => TryConnectSet(driver);

        public override void DeInit(Driver<UnityEngine.Color> driver) => TryDisconnectSet(driver);

        protected override void HandleSet(UnityEngine.Color value) => OnSet.Invoke(value);

    }
}
