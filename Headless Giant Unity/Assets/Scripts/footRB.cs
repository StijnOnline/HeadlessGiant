using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class footRB : MonoBehaviour
{
    public Transform foot;

    private Rigidbody rigidB;



    private void Start() {

        rigidB = GetComponent<Rigidbody>();

    }





    void FixedUpdate() {

        rigidB.MovePosition(foot.position);

        rigidB.MoveRotation(foot.rotation);

    }

    private void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        if(other.layer == LayerMask.NameToLayer("Enemy")) {
            //Enemy enemy = other.GetComponent<Enemy>();
            //enemy.Die();
            Debug.Log("OOF");

            other.GetComponent<NavMeshAgent>().enabled = false;
            Rigidbody rb;
            rb = other.GetComponent<Rigidbody>();
            if(!rb) {
               rb = other.AddComponent<Rigidbody>();
            }             
            rb.AddForce(collision.relativeVelocity * 300f + Vector3.up * 500f);

            Destroy(other, 3f);
        }
    }
}
