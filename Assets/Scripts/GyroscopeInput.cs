using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeInput : MonoBehaviour
{
    Gyroscope my_gyro;
    Quaternion orig_rotation;

    void Start()
    {
        my_gyro = Input.gyro;
        my_gyro.enabled = true;
        Debug.Log("orig rotation: " + my_gyro.attitude);
        orig_rotation = my_gyro.attitude;
    }
    void Update()
    {
        transform.rotation = my_gyro.attitude;
    }

    private Quaternion OffsetRotation(Quaternion q)
    {
        return new Quaternion(q.x - orig_rotation.x, q.y - orig_rotation.y, q.z - orig_rotation.z, q.w - orig_rotation.w);
    }
}
