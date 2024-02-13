using System;

using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.Drivers.Color
{
    [Serializable]
    public class Set_VFX_Graph : Module
    {

        [SerializeField]
        private string propertyName = string.Empty;
        public string PropertyName
        {
            get => propertyName;
            set => propertyName = value;
        }

        [SerializeField]
        private int propertyID = -1;
        public int PropertyID
        {
            get => propertyID;
            set => propertyID = value;
        }

        [SerializeField]
        private VisualEffect[] graphs;
        public VisualEffect[] Graphs
        {
            get => graphs;
            set => graphs = value;
        }


        public override void OnValidate(Driver<UnityEngine.Color> driver)
        {
            #if UNITY_EDITOR
            PropertyID = Shader.PropertyToID(PropertyName);

            // Is it safe to set VFXGraph values outside of Play Mode? Let's find out.

            HandleValueChange(driver.Asset.Source.Value);
            #endif
        }
		
        public override void Init(Driver<UnityEngine.Color> driver)
        {
            PropertyID = Shader.PropertyToID(PropertyName);
            
            base.Init(driver: driver);
        }


        public override void HandleValueChange(UnityEngine.Color value)
        {
            foreach (var g in Graphs)
            {
                if (g != null)
                    g.SetVector4(nameID: PropertyID,
                                 v: value);
            }
        }
		
    }
}
