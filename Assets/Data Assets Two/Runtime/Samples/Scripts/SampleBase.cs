using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.DataAssets.Samples
{

    public abstract class SampleBase : MonoBehaviour
    {

        public BoolAsset boolBinding;
        public FloatAsset floatBinding;
        public IntAsset   intBinding;
        public Vector3Asset vector3Binding;
        public StringAsset stringBinding;
        public EventAsset eventBinding;
        public GameObjectAsset gameObjectBinding;
        
        protected virtual void OnEnable()
        {
            boolBinding.onBroadcast       += BoolChanged;
            floatBinding.onBroadcast      += FloatChanged;
            intBinding.onBroadcast        += IntChanged;
            vector3Binding.onBroadcast    += Vector3Changed;
            stringBinding.onBroadcast     += StringChanged;
            eventBinding.onBroadcast      += EventInvoked;
            gameObjectBinding.onBroadcast += GameObjectAssigned;
        }
        
        protected virtual void OnDisable()
        {
            boolBinding.onBroadcast       -= BoolChanged;
            floatBinding.onBroadcast      -= FloatChanged;
            intBinding.onBroadcast        -= IntChanged;
            vector3Binding.onBroadcast    -= Vector3Changed;
            stringBinding.onBroadcast     -= StringChanged;
            eventBinding.onBroadcast      -= EventInvoked;
            gameObjectBinding.onBroadcast -= GameObjectAssigned;
        }

        protected virtual void BoolChanged(bool value) {}

        protected virtual void FloatChanged(float value) {}

        protected virtual void IntChanged(int value) {}

        protected virtual void Vector3Changed(Vector3 value) {}

        protected virtual void StringChanged(string value) {}

        protected virtual void EventInvoked() { }

        protected virtual void GameObjectAssigned(GameObject incoming) { }
    }

}