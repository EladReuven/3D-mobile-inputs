using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneCamera : MonoBehaviour
{
    bool camAvailable;
    WebCamTexture backCam;
    WebCamTexture frontCam;
    Texture defaultBackground;
    bool camToggle = true;
    float scaleY;
    float ratio;
    int orient;

    public RawImage background;
    public AspectRatioFitter ARFit;
    public TextMeshProUGUI backCamStatus;
    public TextMeshProUGUI frontCamStatus;


    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            frontCamStatus.text = "No Front Camera Available";
            backCamStatus.text = "No Back Camera Available";
            camAvailable = false;
            return;
        }


        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
            else
            {
                frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        backCamStatus.text = backCam == null ? "No Back Camera Available" : "BackCam Available";
        frontCamStatus.text = backCam == null ? "No Front Camera Available" : "FrontCam Available";

        if (backCam != null || frontCam != null)
        {
            camAvailable = true;
        }

        camToggle = backCam == null ? false : true;
    }
    private void Update()
    {
        if (!camAvailable)
            return;

        if (camToggle)
        {
            frontCam.Stop();
            backCam.Play();
            background.texture = backCam;
            ratio = (float)backCam.width / (float)backCam.height;
            scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
            orient = -backCam.videoRotationAngle;
        }
        else
        {
            backCam.Stop();
            frontCam.Play();
            background.texture = frontCam;
            ratio = (float)frontCam.width / (float)frontCam.height;
            scaleY = frontCam.videoVerticallyMirrored ? 1f : -1f;
            orient = -frontCam.videoRotationAngle;
        }

        ARFit.aspectRatio = ratio;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
    }

    public void ToggleCameras()
    {
        camToggle = !camToggle;
    }
}
