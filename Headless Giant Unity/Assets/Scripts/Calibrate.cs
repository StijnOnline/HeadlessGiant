using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;


public class Calibrate : EditorWindow {

    public enum Steps {
        Waiting,
        SetScripts,
        StartSearching,
        Searching,
        Aligning
    }
    public Steps step = Steps.SetScripts;


    public bool calibrating = false;

    public SteamVR_TrackedObject Tracked_leftFoot;
    public SteamVR_TrackedObject Tracked_rightFoot;
    public SteamVR_TrackedObject Tracked_leftHand;
    public SteamVR_TrackedObject Tracked_rightHand;


    public foot leftFoot;
    public foot rightFoot;
    public foot leftHand;
    public foot rightHand;

    public string status;
    public int controller = 0;



    [MenuItem("Window/Calibration")]

    static void Init() {

        Calibrate window = (Calibrate)EditorWindow.GetWindow(typeof(Calibrate));
    }

    void OnGUI() {


        GUILayout.Label("Calibration", "boldLabel");
        GUILayout.Label("Current Step: " + step.ToString());

        if(step == Steps.SetScripts) {
            GUILayout.Space(20);
            GUILayout.Label("Scripts", "boldLabel");


            Tracked_leftFoot = EditorGUILayout.ObjectField("leftFoot", Tracked_leftFoot, typeof(SteamVR_TrackedObject), true) as SteamVR_TrackedObject;
            Tracked_rightFoot = EditorGUILayout.ObjectField("rightFoot", Tracked_rightFoot, typeof(SteamVR_TrackedObject), true) as SteamVR_TrackedObject;
            Tracked_leftHand = EditorGUILayout.ObjectField("leftHand", Tracked_leftHand, typeof(SteamVR_TrackedObject), true) as SteamVR_TrackedObject;
            Tracked_rightHand = EditorGUILayout.ObjectField("rightHand", Tracked_rightHand, typeof(SteamVR_TrackedObject), true) as SteamVR_TrackedObject;

            if(GUILayout.Button("Find Tracked Objects")) {
                Tracked_leftFoot = GameObject.Find("FootTracker L").GetComponent<SteamVR_TrackedObject>();
                Tracked_rightFoot = GameObject.Find("FootTracker R").GetComponent<SteamVR_TrackedObject>();
                Tracked_leftHand = GameObject.Find("Controller L").GetComponent<SteamVR_TrackedObject>();
                Tracked_rightHand = GameObject.Find("Controller R").GetComponent<SteamVR_TrackedObject>();
                
            }
            if(GUILayout.Button("Done")) {
                step = Steps.StartSearching;
            }
        }

        if(step == Steps.StartSearching) {
            GUILayout.Space(20);
            GUILayout.Label("Find Devices", "boldLabel");
            if(GUILayout.Button("Start finding devices") && step == Steps.Waiting && EditorApplication.isPlaying) {
                step = Steps.Searching;
                status = "Please stand still";
            }
            
            GUILayout.Label(status);
        }

    }

    void Update() {
        if(step == Steps.Searching) {


        }
    }

}