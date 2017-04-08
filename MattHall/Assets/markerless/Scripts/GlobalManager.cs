using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

    public GameObject cameraObject;

    public GameObject rock1Prefab;
    public GameObject rock2Prefab;

    private List<Target> listTarget = new List<Target>();

    void Awake()
    {
        Target target1 = (Instantiate(rock1Prefab) as GameObject).GetComponent("RockTarget1") as RockTarget1;
        listTarget.Add(target1);
        target1.init(Constants.pos_target_1);

        Target target2 = (Instantiate(rock2Prefab) as GameObject).GetComponent("RockTarget2") as RockTarget2;
        listTarget.Add(target2);
        target2.init(Constants.pos_target_2);

    }

    void Start ()
    {
        

    }
        
    void Update () {

        Origin origin = cameraObject.GetComponent<Origin>();

        foreach (Target target in listTarget)
        {
            target.updateCameraPos(origin.camera_pos);
        }
	}
}
