using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour
{
    public float speed = 5f;
    Vector3 OGAngle;
    bool angleFound = false;

    void Update()
    {
        if(Input.acceleration != Vector3.zero && !angleFound)
        {
            Debug.Log("first angle: " + Input.acceleration);
            OGAngle.x = Input.acceleration.x;
            OGAngle.y = 0;
            OGAngle.z = Input.acceleration.z;
            angleFound = true;
        }

        Vector3 deltaAngle = Vector3.zero;
        deltaAngle.x = Input.acceleration.x - OGAngle.x;
        deltaAngle.z = (Input.acceleration.z - OGAngle.z) * -1;
        if(deltaAngle.sqrMagnitude > 1)
        {
            deltaAngle.Normalize();
        }
        transform.Translate(deltaAngle * speed * Time.deltaTime);
    }
}
