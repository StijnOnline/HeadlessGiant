using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;


public class Calibrate : EditorWindow {

    public enum Steps {
        SetScripts,
        StartSearching,
        Searching,
        Aligning
    }


    public bool calibrating = false;
    public Steps step = Steps.SetScripts;

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

    public List<GameObject> trackedObjects = new List<GameObject>();
    public Vector3[] lastPositions = new Vector3[16];
    public float[] distancesTravelled = new float[16];


    [MenuItem("Window/Calibrate")]

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

        if(step == Steps.StartSearching || step == Steps.Searching) {
            GUILayout.Space(20);
            GUILayout.Label("Find Devices", "boldLabel");
            if(GUILayout.Button("Start finding devices") && step == Steps.StartSearching && EditorApplication.isPlaying) {
                step = Steps.Searching;
                CreateDevices();
            }
            GUILayout.Label("Move one controller at a time, every sec the most moved device will be shown");
            GUILayout.Label(status);
        }

    }


    void Update() {
        if(step == Steps.Searching) {

            for(int i = 1; i < 16; i++) {
                if(lastPositions[i] != null) {
                    distancesTravelled[i] += (trackedObjects[i].transform.position - lastPositions[i]).magnitude;
                }
                lastPositions[i] = trackedObjects[i].transform.position;
            }



            if(Time.time % 1f < 0.1f) {
                for (int i = 1; i < 16; i++)
                {
                    distancesTravelled[i] = 0;
                }
                int index = System.Array.IndexOf( distancesTravelled,  Mathf.Max(distancesTravelled)) + 1;
                status = "Device " + index + " Moved the most";
            }

        }
    }


    void CreateDevices() {
        for(int i = 1; i <= 16; i++) {
            GameObject go = new GameObject();
            SteamVR_TrackedObject ob = go.AddComponent<SteamVR_TrackedObject>();
            ob.index = (SteamVR_TrackedObject.EIndex) i;
            trackedObjects.Add(go);
        }
        
    }

}