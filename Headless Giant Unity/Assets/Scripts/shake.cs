using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class shake : MonoBehaviour
{
    public SteamVR_Action_Vibration vibrate_action;
    public float delay;
    public float duration;
    public float freq;
    public float amp;

    [ContextMenu("SHAKE")]

    public void Update() {
        if(Input.GetMouseButton(0)) {
            vibrate_action.Execute(delay, duration, freq, amp, SteamVR_Input_Sources.RightHand);

        }
    }
    
}
