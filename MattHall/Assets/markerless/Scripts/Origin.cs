using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Origin : MonoBehaviour
{
    public Text txtLocation;

    public Coordinate camera_pos = new Coordinate(Constants.pos_camera);
    private IEnumerator coroutine;

    public void Awake()
    {
        Input.location.Start();
    }

    void Update()
    {

    }

    IEnumerator Start()
    {
        coroutine = updateGPS();

        if (!Input.location.isEnabledByUser)
        {
            txtLocation.text = "You must enable GPS on your device.";
            yield break;
        }
            

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            txtLocation.text = "Device initializing...";
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            txtLocation.text = "Device initializing timed out";
            yield break;
        }


        if (Input.location.status == LocationServiceStatus.Failed)
        {
            txtLocation.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            showGPSValue();
            StartCoroutine(coroutine);
        }
    }

    private void showGPSValue()
    {
        camera_pos.updateGPS(Input.location.lastData.latitude, Input.location.lastData.longitude);
        txtLocation.text = "Latitude: " + camera_pos.latitude + ", Longitude: " + camera_pos.longitude;
    }

    IEnumerator updateGPS()
    {
        float UPDATE_TIME = 1f; //Every  3 seconds
        WaitForSeconds updateTime = new WaitForSeconds(UPDATE_TIME);

        while (true)
        {
            showGPSValue();
            yield return updateTime;
        }
    }

    void stopGPS()
    {
        Input.location.Stop();
        StopCoroutine(coroutine);
    }

    void OnDisable()
    {
        stopGPS();
    }

}
