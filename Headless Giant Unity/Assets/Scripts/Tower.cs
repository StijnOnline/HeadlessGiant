﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour {

    public float time = 0f;


    public float StartHP;
    public float HP;
    public List<Transform> indicators = new List<Transform>();

    public void Start() {
        time = 0f;
        HP = StartHP;
    }

    public void Update() {
        time += Time.deltaTime;
    }

    public void TakeDamage(float damage) {

        HP -= damage;
        HP = Mathf.Max(0, HP);

        int c = (int)(((StartHP - HP) / StartHP) * (float)indicators.Count); //how many tower parts need to be broken

        for(int i = 0; i < c; i++) {
            GameObject go = indicators[i].gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if(rb == null) {
                rb = go.AddComponent<Rigidbody>();
                rb.AddForce( new Vector3(Random.Range(-100,100), 100, Random.Range(-100, 100))   );
            }
            StartCoroutine(Remove(go, 3f));
        }

        if(HP == 0) {
            PlayerPrefs.SetInt("Time",(int) time);
            SceneManager.LoadScene("GameOver");
        }
    }

    public IEnumerator Remove(GameObject go, float delay) {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
