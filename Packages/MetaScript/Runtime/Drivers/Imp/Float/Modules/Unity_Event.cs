using System;

using UnityEngine.Events;

namespace Vanilla.MetaScript.Drivers.Float
{
    [Serializable]
    public class Unity_Event : Module
    {

        public UnityEvent<float> OnSet = new ();

        public override void OnValidate(Driver<float> driver) => OnSet.Invoke(driver.Asset.Source.Value);

        public override void Init(Driver<float> driver)   => TryConnectSet(driver);
        public override void DeInit(Driver<float> driver) => TryDisconnectSet(driver);

        protected override void HandleSet(float value) => OnSet.Invoke(value);

    }
}
