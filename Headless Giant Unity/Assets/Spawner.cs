using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnpositions;
    public Vector2Int minMaxGroupSize;
    public GameObject prefab;
    public float spawnDelay = 10f;
    private float lastSpawn = 5f;

    public void Start() {
        lastSpawn = -spawnDelay;
    }

    public void Update() {
        if(Time.time > lastSpawn + spawnDelay) {
            Spawn(0);
            lastSpawn = Time.time;
        }
    }

    public void Spawn(int positionIndex) {
        int r = Random.Range(minMaxGroupSize.x, minMaxGroupSize.y);
        for(int i = 0; i < r; i++) {
            Instantiate(prefab, spawnpositions[positionIndex].position, spawnpositions[positionIndex].rotation);
        }

    }


}
