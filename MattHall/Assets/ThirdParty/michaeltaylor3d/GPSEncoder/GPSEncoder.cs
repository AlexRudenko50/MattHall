//Copyright 2013 MichaelTaylor3D
//www.michaeltaylor3d.com

using System.Collections.Generic;
using UnityEngine;

public sealed class GPSEncoder {

	/////////////////////////////////////////////////
	//////-------------Public API--------------//////
	/////////////////////////////////////////////////
	
	/// <summary>
	/// Convert UCS (X,Y,Z) coordinates to GPS (Lat, Lon) coordinates
	/// </summary>
	/// <returns>
	/// Returns Vector2d containing Latitude and Longitude
	/// </returns>
	/// <param name='position'>
	/// (X,Y,Z) Position Parameter
	/// </param>
	public static Vector2d USCToGPS(Vector3d position)
	{
		return GetInstance().ConvertUCStoGPS(position);
	}
	
	/// <summary>
	/// Convert GPS (Lat, Lon) coordinates to UCS (X,Y,Z) coordinates
	/// </summary>
	/// <returns>
	/// Returns a Vector3d containing (X, Y, Z)
	/// </returns>
	/// <param name='gps'>
	/// (Lat, Lon) as Vector2d
	/// </param>
	public static Vector3d GPSToUCS(Vector2d gps)
	{
		return GetInstance().ConvertGPStoUCS(gps);
	}
	
	/// <summary>
	/// Convert GPS (Lat, Lon) coordinates to UCS (X,Y,Z) coordinates
	/// </summary>
	/// <returns>
	/// Returns a Vector3d containing (X, Y, Z)
	/// </returns>
	public static Vector3d GPSToUCS(double latitude, double longitude)
	{
		return GetInstance().ConvertGPStoUCS(new Vector2d(latitude,longitude));
	}

    public static Vector3d FindCenterPoint( List<Vector3d> vectors){
        var center = new Vector3d(0, 0, 0);
        var count = 0;
        foreach(var point in vectors)
        {
            center += point;
            count++;
        }

        return center / count;
    }
/// <summary>
/// Change the relative GPS offset (Lat, Lon), Default (0,0), 
/// used to bring a local area to (0,0,0) in UCS coordinate system
/// </summary>
/// <param name='localOrigin'>
/// Referance point.
/// </param>
public static void SetLocalOrigin(Vector2d localOrigin)
	{
		GetInstance()._localOrigin = localOrigin;
	}
		
	/////////////////////////////////////////////////
	//////---------Instance Members------------//////
	/////////////////////////////////////////////////
	
	#region Singleton
	private static GPSEncoder _singleton;
	
	private GPSEncoder()
	{
		
	}
	
	private static GPSEncoder GetInstance()
	{
		if(_singleton == null)
		{
			_singleton = new GPSEncoder();
		}
		return _singleton;
	}
	#endregion
	
	#region Instance Variables
	private Vector2d _localOrigin = Vector2d.zero;
	private double _LatOrigin { get{ return _localOrigin.x; }}	
	private double _LonOrigin { get{ return _localOrigin.y; }}

	private double metersPerLat;
	private double metersPerLon;
	#endregion
	
	#region Instance Functions
	private void FindMetersPerLat(double lat) // Compute lengths of degrees
	{
	    // Set up "Constants"
	    double m1 = 111132.92f;    // latitude calculation term 1
	    double m2 = -559.82f;        // latitude calculation term 2
	    double m3 = 1.175f;      // latitude calculation term 3
	    double m4 = -0.0023f;        // latitude calculation term 4
	    double p1 = 111412.84f;    // longitude calculation term 1
	    double p2 = -93.5f;      // longitude calculation term 2
	    double p3 = 0.118f;      // longitude calculation term 3
	    
	    lat = lat * Mathd.Deg2Rad;
	
	    // Calculate the length of a degree of latitude and longitude in meters
	    metersPerLat = m1 + (m2 * Mathd.Cos(2 * (double)lat)) + (m3 * Mathd.Cos(4 * (double)lat)) + (m4 * Mathd.Cos(6 * (double)lat));
	    metersPerLon = (p1 * Mathd.Cos((double)lat)) + (p2 * Mathd.Cos(3 * (double)lat)) + (p3 * Mathd.Cos(5 * (double)lat));	   
	}

	private Vector3d ConvertGPStoUCS(Vector2d gps)  
	{
		FindMetersPerLat(_LatOrigin);
		double zPosition  = metersPerLat * (gps.x - _LatOrigin); //Calc current lat
		double xPosition  = metersPerLon * (gps.y - _LonOrigin); //Calc current lat
		return new Vector3d((double)xPosition, 0, (double)zPosition);
	}
	
	private Vector2d ConvertUCStoGPS(Vector3d position)
	{
		FindMetersPerLat(_LatOrigin);
		Vector2d geoLocation = new Vector2d(0,0);
		geoLocation.x = (_LatOrigin + (position.z)/metersPerLat); //Calc current lat
		geoLocation.y = (_LonOrigin + (position.x)/metersPerLon); //Calc current lon
		return geoLocation;
	}
	#endregion
}
