using System;

using UnityEngine;
using UnityEngine.VFX;

namespace Vanilla.MetaScript.Drivers.Vec3
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

        public override void Init(Driver<Vector3> driver)
        {
            PropertyID = Shader.PropertyToID(PropertyName);

            TryConnectSet(driver);
        }


        public override void DeInit(Driver<Vector3> driver) => TryDisconnectSet(driver);


        protected override void HandleSet(Vector3 value)
        {
            foreach (var g in Graphs)
            {
                if (g != null)
                    g.SetVector3(nameID: PropertyID,
                                 v: value);
            }
        }

    }

}
