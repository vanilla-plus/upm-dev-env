using System;

using UnityEngine;

namespace Vanilla.Geocodes
{

    [Serializable]
    public class GeocodeMap<C> : ISerializationCallbackReceiver
        where C : IGeocode<C>, new()
    {

//        [SerializeField]
//        private C[] _neighbours = new C[9];
//        public C[] Neighbours => _neighbours;

//        [NonSerialized]
//        private C _center;
//        public  C CenterCode => _center;

        [SerializeField]
        public C NorthWest,
                 North,
                 NorthEast,
                 West,
                 Center,
                 East,
                 SouthWest,
                 South,
                 SouthEast = new();

        public bool AutoUpdateNeighbours = false;

        public void OnBeforeSerialize() { }

        public virtual void OnAfterDeserialize()
        {
            #if UNITY_EDITOR
//            _center ??= _neighbours[4];

//            _center.OnValidate();
//
//            if (AutoUpdateNeighbours) _center.UpdateNeighbours(northWest: _neighbours[0],
//                                                               north: _neighbours[1],
//                                                               northEast: _neighbours[2],
//                                                               west: _neighbours[3],
//                                                               east: _neighbours[5],
//                                                               southWest: _neighbours[6],
//                                                               south: _neighbours[7],
//                                                               southEast: _neighbours[8]);

            if (AutoUpdateNeighbours)
                Center.UpdateNeighbours(NorthWest,
                                        North,
                                        NorthEast,
                                        West,
                                        East,
                                        SouthWest,
                                        South,
                                        SouthEast);

            #endif
        }

    }

}