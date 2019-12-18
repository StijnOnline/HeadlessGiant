using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(NavMeshObstacle))]
public class Enemy : MonoBehaviour {
    public float speed;
    public float runDistance;
    public float stopDistance = 2.5f;
    public float shootDelay = 5f;
    public int attackPower = 1;

    public GameObject arrowObject;
    public GameObject explosionObject;
    public GameObject effectObject;
    public AudioClip clip;

    public NavMeshAgent navMeshAgent { get; private set; }
    public NavMeshObstacle navMeshObstacle { get; private set; }

    public Catapult cat;

    private Vector3 target;
    bool moving = true, ready = false;
    private float lastShot = 5f;
    Quaternion fromReady, toReady;
    float fromReadyAngle, toReadyAngle;
    float rotateTimer;
    static readonly float rotateTime = 2.0f;

    Quaternion startRotation;

    private AudioSource audioSource;
    private Rigidbody rb;

    public enum EnemyType {
        wizard,
        catapult,
        bomber
    }

    public EnemyType enemyType;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cat = GetComponent<Catapult>();

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();


        float yy = transform.rotation.eulerAngles.y * Mathf.PI / 180.0f;

        float arcSize = 0.5f * Mathf.PI;
        float angle = -Random.Range(yy - 0.5f * arcSize, yy + 0.5f * arcSize) - 0.5f * Mathf.PI;
        target = new Vector3(stopDistance * Mathf.Cos(angle), 0, stopDistance * Mathf.Sin(angle));

        

        NavMeshHit hit;
        if(NavMesh.SamplePosition(target, out hit, 500f, NavMesh.AllAreas)) {
            navMeshAgent.SetDestination(hit.position);
        } else {
            Debug.LogWarning("Could not find suitable destination");
        }
        

        //TODO add back
        //navMeshObstacle = GetComponent<NavMeshObstacle>();
        //navMeshObstacle.enabled = false;


        navMeshAgent.speed = speed;
    }

    public void Die(Vector3 hitspeed) {
        rb.constraints = RigidbodyConstraints.None;

        navMeshAgent.enabled = false;

        if(rb != null) {
            rb.AddForce(hitspeed * 300f + Vector3.up * 500f);
        }

        if(clip != null)
            audioSource.PlayOneShot(clip, 1);

        Destroy(gameObject, 3f);

    }

    float currentDist() {

        Vector3 tarDist = transform.position - navMeshAgent.destination;
        return Mathf.Sqrt(tarDist.x * tarDist.x + tarDist.z * tarDist.z);



        //return navMeshAgent.remainingDistance;
    }

    public void Update() {
        //Debug.DrawLine(transform.position, transform.position + transform.forward);

        if(navMeshAgent.isOnNavMesh && moving) {



            if(enemyType == EnemyType.bomber && currentDist() < runDistance) {
                navMeshAgent.speed = 4 * speed;
                if (effectObject != null)
                {
                    if (effectObject.activeSelf == false) effectObject.SetActive(true);
                }
            }

            if(currentDist() < 0.01f) {
                moving = false;
                navMeshAgent.enabled = false;
                lastShot = Time.time;
                fromReadyAngle = transform.rotation.eulerAngles.y;
                toReadyAngle = Vector3.Angle(transform.position, new Vector3(0, transform.position.y, 0));
                rotateTimer = rotateTime;


                fromReady = transform.rotation;
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                toReady = transform.rotation;
                transform.rotation = fromReady;
            }
        }

        if(!moving) {
            if(!ready) {
                rotateTimer -= Time.deltaTime;
                if(rotateTimer < 0) {
                    ready = true;
                    transform.LookAt(new Vector3(0, transform.position.y, 0));
                } else {
                    float t = (1.0f - rotateTimer / rotateTime);
                    transform.rotation = Quaternion.Lerp(fromReady, toReady, t);
                }
            }

            //schiet pijlen
            else if(Time.time > lastShot + shootDelay && enemyType != EnemyType.bomber) {
                Vector3 offset = Vector3.zero;
                if (enemyType == EnemyType.wizard)
                {
                    Animator ani = transform.GetChild(0).GetComponent<Animator>();
                    ani.SetBool("Shoot", true);
                    offset = transform.forward * 0.2f;
                }

                if (enemyType == EnemyType.catapult)
                {
                    offset = transform.up * 0.2f - transform.forward * 0.2f;
                    if (audioSource != null) audioSource.PlayOneShot(clip, 0.5f);
                    cat.fire = true;
                }

                GameObject arrow = Instantiate(arrowObject, transform.position + /*transform.forward * 0.2f*/offset, transform.rotation);
                Projectile pr = arrow.GetComponent<Projectile>();
                pr.attackPower = attackPower;
                lastShot = Time.time;
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == 10) {

            Tower t = collision.transform.parent.GetComponent<Tower>();
            if(t != null) {
                t.TakeDamage(attackPower);
            }

            Instantiate(explosionObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(collision.gameObject.layer == 8 && enemyType == EnemyType.bomber) { //remove other enemies if hit
            Destroy(collision.gameObject);
        }
    }
}