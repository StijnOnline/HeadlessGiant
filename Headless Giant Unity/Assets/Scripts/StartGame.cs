using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StartGame : MonoBehaviour
{
    public List<Behaviour> toEnable;
    public List<Behaviour> toDisable;
    private bool started = false;
    

    public void Play() {        

        if(started)
            return;

        foreach(Behaviour item in toEnable) {
            item.enabled = true;
        }

        foreach(Behaviour item in toDisable) {
            item.enabled = false;
        }

        started = true;
    }

}
[CustomEditor(typeof(StartGame))]
public class LookAtPointEditor : Editor {

    public override void OnInspectorGUI() {
        StartGame myTarget = (StartGame)target;

        base.OnInspectorGUI();
        if(GUILayout.Button("Play")) {
            myTarget.Play();
        }
    }
}