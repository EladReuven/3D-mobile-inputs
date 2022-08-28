using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPSstats : MonoBehaviour
{
    public TextMeshProUGUI GPSStatus;
    public TextMeshProUGUI latitudeValue;
    public TextMeshProUGUI longitudeValue;
    public TextMeshProUGUI altitudeValue;
    public TextMeshProUGUI horizontalAccuracyValue;
    public TextMeshProUGUI timeStampValue;

    private void Start()
    {
        StartCoroutine(GPSLocation());
    }

    IEnumerator GPSLocation()
    {
        if(!Input.location.isEnabledByUser)
        {
            yield break;
        }

        Input.location.Start();

        int waitTime = 10;
        while(Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
        {
            GPSStatus.text = "Thinking...";
            yield return new WaitForSeconds(1);
            waitTime--;
        }

        if(waitTime < 1)
        {
            GPSStatus.text = "No worky";
            yield break;

        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Location no findy";
            yield break;
        }
        else
        {
            GPSStatus.text = "Is worky";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccuracyValue.text = Input.location.lastData.horizontalAccuracy.ToString();
            timeStampValue.text = Input.location.lastData.timestamp.ToString();

        }
        else
        {
            //stop service
            GPSStatus.text = "Stahp";
        }
    }

}
