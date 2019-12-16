using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public float attackPower = 1;
    //public AudioSource audioSource;
    //public AudioClip audioSource;

    public Vector2 xSpeedRange = new Vector2(-1.5f, 1.5f);
    public Vector2 ySpeedRange = new Vector2(3, 8);
    public Vector2 zSpeedRange = new Vector2(6, 12);
    
    void Start()
    {
        float xSpeed, ySpeed, zSpeed;
        xSpeed = Random.Range(xSpeedRange.x, xSpeedRange.y);
        ySpeed = Random.Range(ySpeedRange.x, ySpeedRange.y);
        zSpeed = Random.Range(zSpeedRange.x, zSpeedRange.y);
        rb = GetComponent<Rigidbody>();
        Vector3 force = transform.right * xSpeed + transform.up * ySpeed + transform.forward * zSpeed;
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            //audioSource.PlayOneShot();
            Destroy(gameObject);
        }
    }
}
