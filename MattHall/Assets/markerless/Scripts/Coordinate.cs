using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate {

    public double latitude = 0;
    public double longitude = 0;

    public Vector3d pos_ucs;

    public Coordinate()
    {

    }

    public Coordinate(Vector2d vGPS)
    {
        latitude = vGPS.x;
        longitude = vGPS.y;
        pos_ucs = GPSEncoder.GPSToUCS(latitude, longitude);/// 10000.0f;
    }

    public Coordinate(double lat, double lon)
    {
        latitude = lat;
        longitude = lon;
        pos_ucs = GPSEncoder.GPSToUCS(latitude, longitude);// 10000.0f;
    }

    public Vector3d updateGPS(double lat,double lon)
    {
        latitude = lat;
        longitude = lon;
        pos_ucs = GPSEncoder.GPSToUCS(lat, lon);// 10000.0f;
        return pos_ucs;
    }

    public Vector3d updateGPS(Vector2d vGPS)
    {
        latitude = vGPS.x;
        longitude = vGPS.y;
        pos_ucs = GPSEncoder.GPSToUCS(vGPS.x, vGPS.y);// 10000.0f;
        return pos_ucs;
    }

}
