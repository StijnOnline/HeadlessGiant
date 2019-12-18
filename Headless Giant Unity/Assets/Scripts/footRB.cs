using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class footRB : MonoBehaviour
{
    public Transform foot;
    public Shake shake;

    private Rigidbody rigidB;

    private AudioSource audioSource;
    public List<AudioClip> stompSounds;
    public float volume = 1f;



    private void Start() {

        rigidB = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
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
        if(other.layer == LayerMask.NameToLayer("Ground")) {

            audioSource.PlayOneShot( stompSounds[Random.Range(0, stompSounds.Count)],  volume);

        }
    }
}
