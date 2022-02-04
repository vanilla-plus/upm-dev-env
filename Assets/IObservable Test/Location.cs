using System;

using UnityEngine;

[Serializable]
public struct Location
{

	[SerializeField]
	public double lat;

	[SerializeField]
	public double lon;


	public Location(double latitude,
	                double longitude)
	{
		lat = latitude;
		lon = longitude;
	}


	public double Latitude  => lat;
	public double Longitude => lon;

}