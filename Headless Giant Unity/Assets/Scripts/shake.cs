using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Shake : MonoBehaviour {
    public SteamVR_Action_Vibration vibrate_action;
    public float delay;
    public float duration;
    public float freq;
    public float amp;

    public bool debug;

    [ContextMenu("Test")]

    public void Update() {

        if(debug && Input.GetMouseButtonDown(0))
            StartCoroutine(Shak(duration));
    }
    public void StartShake() {
        StartCoroutine(Shak(duration));
    }
    public IEnumerator Shak(float length) {
        float c = 0;
        while(c < length) {
            c += Time.deltaTime;
            vibrate_action.Execute(delay, duration, freq, amp, SteamVR_Input_Sources.LeftHand);
            vibrate_action.Execute(delay, duration, freq, amp, SteamVR_Input_Sources.RightHand);
            yield return 0;
        }
    }


}
