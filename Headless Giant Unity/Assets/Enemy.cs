using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Enemy : MonoBehaviour {
    public float speed;
    public NavMeshAgent navMeshAgent { get; private set; }
    public NavMeshObstacle navMeshObstacle { get; private set; }
    private Vector3 target = Vector3.zero;
    public float targetDist;

    private void Start() {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        
    }

    public void Die() {
        Debug.Log("OEF");
    }

    public void Update() {
        //float dist = Mathf.Sqrt(Mathf.Pow((transform.position - target).x, 2) + Mathf.Pow((transform.position - target).z, 2));


        if(navMeshAgent.isOnNavMesh) {

            NavMeshHit hit;
            if(NavMesh.SamplePosition(target - (target - transform.position).normalized * 0.1f, out hit, 5f, NavMesh.AllAreas)) {
                navMeshAgent.SetDestination(hit.position);
            }
            if(navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance < targetDist) {
                navMeshAgent.enabled = false;
                navMeshObstacle.enabled = true;
            }
        }

    }


}