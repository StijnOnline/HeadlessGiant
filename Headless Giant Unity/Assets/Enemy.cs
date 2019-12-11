using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
    public float speed;
    public NavMeshAgent navMeshAgent {get; private set;}
    public Vector3 target = Vector3.zero;
    public float targetDist;

    private void Start() {

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(target);
        navMeshAgent.speed = speed;
    }

    public void Die() {
        Debug.Log("OEF");
    }

    public void Update() {
        float dist = Mathf.Sqrt(Mathf.Pow((transform.position-target).x, 2) + Mathf.Pow((transform.position-target).z, 2));

        if(dist < targetDist) {
            navMeshAgent.enabled = false;
        }
    }


}