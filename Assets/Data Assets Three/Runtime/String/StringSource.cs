using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

    // ----------------------------------------------------------------------------------------------------------------------------------- Source //

    [Serializable]
    public abstract class StringSource : RefSource<string> { }

    // -------------------------------------------------------------------------------------------------------------------------------------- Raw //

    [Serializable]
    public class StringSource_Raw : StringSource
    {

        [SerializeField]
        private String _value;
        public String Value
        {
            get => _value;
            set
            {
                if (ReferenceEquals(objA: _value,
                                    objB: value)) return;

                var outgoing = _value;

                _value = value;

                SetReceived(outgoing: outgoing,
                            incoming: _value);
            }
        }

        public override String Get() => Value;

        public override void Set(String newValue) => Value = newValue;


        public override void Validate()
        {
            #if UNITY_EDITOR
            SetReceived(outgoing: _value,
                        incoming: _value);

            BroadcastReceived(incoming: _value);
            #endif
        }

    }

    // ------------------------------------------------------------------------------------------------------------------- Direct Asset Reference //

    [Serializable]
    public class StringSource_DirectAssetReference : StringSource
    {

        public StringAsset asset;

        public override String Get() => asset.source.Get();

        public override void Set(String newValue) => asset.source.Set(newValue: newValue);


        public override async UniTask Initialize()
        {
            if (Initialized) return;

            asset.source.OnSet.AddListener(call: SetReceived);
            asset.source.OnBroadcast.AddListener(call: BroadcastReceived);

            await base.Initialize();
        }


        public override async UniTask DeInitialize()
        {
            if (!Initialized) return;

            asset.source.OnSet.RemoveListener(call: SetReceived);
            asset.source.OnBroadcast.RemoveListener(call: BroadcastReceived);

            await base.DeInitialize();

        }

    }


}