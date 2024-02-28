using System;

using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.MetaScript.Drivers.Vec2
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

        public override void Init(Driver<Vector2> driver)
        {
            PropertyID = Shader.PropertyToID(PropertyName);
            
            TryConnectSet(driver);
        }


        public override void DeInit(Driver<Vector2> driver) => TryDisconnectSet(driver);


        protected override void HandleSet(Vector2 value)
        {
            foreach (var g in Graphs)
            {
                if (g != null)
                    g.SetVector2(nameID: PropertyID,
                                 v: value);
            }
        }
		
    }
}
