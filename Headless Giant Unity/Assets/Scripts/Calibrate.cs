using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Calibrate : EditorWindow {

    public enum Steps {
        Waiting,
        Searching,
        Aligning,
        Done
    }
    public Steps step;


    public bool calibrating = false;
    public foot leftFoot;
    public foot rightFoot;
    public foot leftHand;
    public foot rightHand;




    [MenuItem("Window/Event Editor")]

    static void Init() {

        Calibrate window = (Calibrate)EditorWindow.GetWindow(typeof(Calibrate));

    }

    void OnGUI() {

    }
}