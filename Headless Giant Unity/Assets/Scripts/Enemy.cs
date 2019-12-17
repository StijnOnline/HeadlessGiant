using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Enemy : MonoBehaviour {
    public float speed;
    public float distFromTower = 2.5f;
    public NavMeshAgent navMeshAgent { get; private set; }
    public NavMeshObstacle navMeshObstacle { get; private set; }
    private Vector3 target;

    bool moving = true, ready = false;
    public Vector3 tarDist;

    public GameObject arrowObject;
    public GameObject explosionObject;
    public float spawnDelay = 5f;
    private float lastSpawn = 5f;

    Quaternion fromReady, toReady;
    float fromReadyAngle, toReadyAngle;
    float rotateTimer;
    static readonly float rotateTime = 2.0f;

    public int attackPower = 1;

    Quaternion startRotation;

    public AudioClip clip;
    private AudioSource audioSource;
    private Rigidbody rb;

    private void Start() {

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();

        float yy = transform.rotation.eulerAngles.y * Mathf.PI / 180.0f;

        float arcSize = 0.5f * Mathf.PI;
        float angle = -Random.Range(yy - 0.5f * arcSize, yy + 0.5f * arcSize) - 0.5f * Mathf.PI;
        target = new Vector3(distFromTower * Mathf.Cos(angle), 0, distFromTower * Mathf.Sin(angle));

        //TODO add back
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    public void Die(Vector3 hitspeed) {
        rb.constraints = RigidbodyConstraints.None;

        Debug.Log("OOF");

        navMeshAgent.enabled = false;
        
        if(rb != null) {
            rb.AddForce(hitspeed * 300f + Vector3.up * 500f);
            Debug.Log("WOO");
        }

        if(clip != null)
            audioSource.PlayOneShot(clip, 1);

        Destroy(gameObject, 3f);

    }

    bool reachedTarget()
    {
        return (Mathf.Sqrt(tarDist.x * tarDist.x + tarDist.z * tarDist.z) <= 0.01f);
    }

    public void Update() {
        Debug.DrawLine(transform.position, transform.position + transform.forward);
        tarDist = target - navMeshAgent.destination;

        if (navMeshAgent.isOnNavMesh && moving) {
            
            NavMeshHit hit;
            if(NavMesh.SamplePosition(target - (target - transform.position).normalized * 0.1f, out hit, 5f, NavMesh.AllAreas)) {
                navMeshAgent.SetDestination(hit.position);
            }

            if (reachedTarget())
            {
                moving = false;
                navMeshAgent.enabled = false;
                lastSpawn = Time.time;
                fromReadyAngle = transform.rotation.eulerAngles.y;
                toReadyAngle = Vector3.Angle(transform.position, new Vector3(0, transform.position.y, 0));
                rotateTimer = rotateTime;


                fromReady = transform.rotation;
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                toReady = transform.rotation;
                transform.rotation = fromReady;
            }
        }

        if (!moving)
        {
            if (!ready)
            {
                rotateTimer -= Time.deltaTime;
                if (rotateTimer < 0)
                {
                    ready = true;
                    transform.LookAt(new Vector3(0, transform.position.y, 0));
                }
                else
                {
                    float t = (1.0f - rotateTimer / rotateTime);
                    transform.rotation = Quaternion.Lerp(fromReady, toReady, t);
                }
            }

            //schiet pijlen
            else if (Time.time > lastSpawn + spawnDelay)
            {
                GameObject arrow = Instantiate(arrowObject, transform.position + transform.forward * 0.2f, transform.rotation);
                Projectile pr = arrow.GetComponent<Projectile>();
                pr.attackPower = attackPower;
                lastSpawn = Time.time;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (explosionObject != null)
        {
            Instantiate(explosionObject, transform.position, transform.rotation);
        }
    }
}