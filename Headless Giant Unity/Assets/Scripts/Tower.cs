using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public float StartHP;
    public float HP;
    public List<Transform> indicators = new List<Transform>();

    public void Start() {
        HP = StartHP;
    }

    public void TakeDamage(float damage) {
        print("OOF");


        HP -= damage;
        HP = Mathf.Max(0, HP);

        int c = (int)(((StartHP - HP) / StartHP) * (float)indicators.Count); //how many tower parts need to be broken

        print(c);

        for(int i = 0; i < c; i++) {
            GameObject go = indicators[i].gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if(rb == null) {
                rb = go.AddComponent<Rigidbody>();
                rb.AddForce(Vector3.up * 5);
            }
        }
    }
}
