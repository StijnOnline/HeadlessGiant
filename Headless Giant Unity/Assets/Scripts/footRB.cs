using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class footRB : MonoBehaviour
{
    public Transform foot;
    public Shake shake;

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

            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Die(collision.relativeVelocity);

            shake.StartShake();
        }
    }
}
