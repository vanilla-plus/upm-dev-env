using System;

using UnityEngine;

using Vanilla.DeltaValues;
using Vanilla.TypeMenu;

namespace Vanilla.MaterialDriver
{

    [Serializable]
    public class PropertyDriverSet : ISerializationCallbackReceiver
    {

        [Tooltip("Please ensure that Name is unique across your entire application. It may be good to utilize a naming convention that appends by the containing scene name as a prefix, for example.")]
        [SerializeField]
        public string Name;

        [Tooltip("This DeltaFloat drives all the materials found in Properties simultaneously. For best results, set it to the desired default value for a given scene in the Editor. All referenced materials will restore to their default values after exiting Play mode.")]
        [SerializeField]
        public DeltaVec1 Normal = new DeltaVec1();

        [SerializeReference]
        [TypeMenu]
        public PropertyDriver[] Properties = Array.Empty<PropertyDriver>();

        public void OnBeforeSerialize() { }


        public void OnAfterDeserialize()
        {
            Normal.Name          = Name;
            
            Normal.Min           = 0.0f;
            Normal.Max           = 1.0f;
            Normal.Min           = 0.0f;

            Normal.MinMaxEpsilon = 0.001f; // This is 0.01f in DeltaFloat
            Normal.ChangeEpsilon = Mathf.Epsilon;

            foreach (var p in Properties)
            {
//                p?.Lerp(Normal.Value); // Have to comment this out for builds... so annoying!
            }
        }

        public void OnValidate()
        {
            #if UNITY_EDITOR
            foreach (var p in Properties)
            {
                p?.OnValidate(Normal.Value);
            }
            #endif
        }

        public void Init()
        {
            foreach (var p in Properties)
            {
                p.Init(Normal.Value);
                
                Normal.OnValueChanged += p.Lerp;
            }
        }


        public void DeInit()
        {
            foreach (var p in Properties)
            {
                p.DeInit();
                
                Normal.OnValueChanged -= p.Lerp;
            }
        }




    }

}