using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Geocodes;

[Serializable]
public class GeocodeTest : MonoBehaviour
{

    [SerializeField]
    public Coordinate coordinate = new Coordinate();

    [SerializeField]
    public GeoHash geohash = new GeoHash();

    [SerializeField]
    public GeoHashMap geoHashMap = new GeoHashMap();

    private void OnValidate()
    {
        coordinate.OnValidate();
        
        geohash.OnValidate();
        geoHashMap.OnValidate();
    }

}