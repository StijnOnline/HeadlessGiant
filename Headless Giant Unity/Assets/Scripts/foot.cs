using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot : MonoBehaviour
{
    public float scale = 5;
    public Transform controller;
    public Transform relativePos;
    public Vector3 rotationOffset;
    public float yOffset;
    
    void Update()
    {
        transform.position = (controller.position - relativePos.position) * scale + Vector3.up * yOffset;
        //Vector3 trackerRotation = controller.localRotation.eulerAngles + rotationOffset;
        //trackerRotation.z = 0;
        //trackerRotation.x *= 0.5f;
        //transform.rotation = Quaternion.Euler(trackerRotation);
        transform.rotation = controller.localRotation * Quaternion.Euler(rotationOffset);
    }
}
