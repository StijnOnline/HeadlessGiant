using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Transform arm;
    public Transform piv;

    public float xRot, amp;

    public bool fire = false;

    float timer = 0f;
    float timerEnd = 1f;

    Vector2 xRotRange = new Vector2(-25f, 90);

    private void Start()
    {
        xRot = xRotRange.x;
        amp = (xRotRange.y - xRotRange.x) / 2f;
    }

    
    void Update()
    {
        if (fire && timer < timerEnd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            fire = false;
            timer = 0;
        }

        xRot = amp * Mathf.Sin((2 * Mathf.PI) / 1 * (timer - 0.25f)) + (xRotRange.y - amp);

        arm.localRotation = Quaternion.Euler(new Vector3(xRot, 0, 0));
        piv.localPosition = new Vector3(0, 0, -MapValue(-25, 90, 0, 1, xRot));
    }

    float MapValue(float fromLow, float fromHigh, float toLow, float toHigh, float value)
    {
        return (fromHigh - value) / (fromHigh - fromLow) * (toHigh - toLow) + toLow;
    }
}
