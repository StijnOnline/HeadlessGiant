using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public float attackPower = 1;
    
    void Start()
    {
        float xSpeed, ySpeed, zSpeed;
        xSpeed = Random.Range(-1.5f, 1.5f);
        ySpeed = Random.Range(3, 8);
        zSpeed = Random.Range(6, 12);
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
            Destroy(gameObject);
        }
    }
}
