using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

    // ----------------------------------------------------------------------------------------------------------------------------------- Source //

    [Serializable]
    public abstract class MonoBehaviourSource : RefSource<MonoBehaviour> { }

    // -------------------------------------------------------------------------------------------------------------------------------------- Raw //

    [Serializable]
    public class MonoBehaviourSource_Raw : MonoBehaviourSource
    {

        [SerializeField]
        private MonoBehaviour _value;
        public MonoBehaviour Value
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

        public override MonoBehaviour Get() => Value;

        public override void Set(MonoBehaviour newValue) => Value = newValue;


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
    public class MonoBehaviourSource_DirectAssetReference : MonoBehaviourSource
    {

        public MonoBehaviourAsset asset;

        public override MonoBehaviour Get() => asset.source.Get();

        public override void Set(MonoBehaviour newValue) => asset.source.Set(newValue: newValue);


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