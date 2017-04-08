using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour {

    public GameObject RockModel;
    private Vector2d pos_ucs;

    private Coordinate coord = null;

    public Target()
    {

    }

    public virtual void init(double lat, double lan)
    {
        coord = new Coordinate(lat,lan);
    }

    public virtual void init(Vector2d vGPSPos)
    {
        coord = new Coordinate(vGPSPos);
    }
    
    public virtual void updateCameraPos(Coordinate pos_camera)
    {
        Vector3d vDiff = (coord.pos_ucs - pos_camera.pos_ucs) / 1.0f;
        transform.position = new Vector3((float)vDiff.x, (float)vDiff.y, (float)vDiff.z);
    }
    
    void Start()
    {

    }
    void Update () {
		
	}
    
}
