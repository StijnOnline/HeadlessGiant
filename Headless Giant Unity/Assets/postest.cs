using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postest : MonoBehaviour
{
    public float scale = 5;
    public Transform controller;
    public Transform VRrig;
    
    void Update()
    {
        transform.position = (controller.localPosition) * scale;
        transform.rotation = controller.localRotation;
    }
}
