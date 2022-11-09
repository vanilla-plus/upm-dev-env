using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Geocodes
{

    [Serializable]
    public abstract class GeocodeMap<C>
        where C : IGeocode<C>
    {

        [SerializeField]
        private C[] _neighbours = new C[9];
        public C[] Neighbours => _neighbours;

        [NonSerialized]
        private C _center;
        public  C CenterCode => _center;

        public bool AutoUpdateNeighbours = false;

        public virtual void OnValidate()
        {
            _center ??= _neighbours[4];

            _center.OnValidate();

            if (AutoUpdateNeighbours) _center.UpdateNeighbours(northWest: _neighbours[0],
                                                               north: _neighbours[1],
                                                               northEast: _neighbours[2],
                                                               west: _neighbours[3],
                                                               east: _neighbours[5],
                                                               southWest: _neighbours[6],
                                                               south: _neighbours[7],
                                                               southEast: _neighbours[8]);
        }

    }

}