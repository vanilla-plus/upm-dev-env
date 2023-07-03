using System;

using UnityEngine;

using Vanilla.Geocodes;

[Serializable]
public class GeocodeTest : MonoBehaviour
{

	[SerializeField]
	public Coordinate coordinate = new Coordinate();

	[SerializeField]
	public string CorrectAnswer = "CWC8+Q9W";

	[SerializeField]
	public GeoHash geohash = new GeoHash();

	[SerializeField]
	public Geoji Geoji;

	[SerializeField]
	public OpenLocationCode OpenLocationCode = new OpenLocationCode(0.0,
	                                        0.0);
	

//    [SerializeField]
//    public GeoHashMap geoHashMap = new GeoHashMap();

	[SerializeField]
	public GeocodeMap<GeoHash> geoHashMap;

	//-37.780120, 144.892915

	[ContextMenu("Geoji - Hash To Lat Long")]
	public void GeojiHashToLatLong() => Geoji.Hash = Geoji.Hash;

	[ContextMenu("Geoji - Lat Long To Hash")]
	public void GeojiLatLongToHash() => Geoji.Validate();
}