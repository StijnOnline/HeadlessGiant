using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public Vector2Int minMaxGroupSize;
    //public GameObject prefab;

    public List<enem> prefabs;

    public float spawnDelay = 10f;
    private float lastSpawn = 5f;

    [System.Serializable]
    public struct enem
    {
        public GameObject prefab;
        public Vector2Int minMaxGroupSize; 
    }

    public void Start() {
        lastSpawn = -spawnDelay;
    }

    public void Update() {
        //Debug.DrawLine(transform.position, transform.position + transform.forward);
        if(Time.time > lastSpawn + spawnDelay) {
            Spawn();
            lastSpawn = Time.time;
        }
    }

    public void Spawn() {
        int r = Random.Range(0, prefabs.Count);
        int nEnemies = Random.Range(prefabs[r].minMaxGroupSize.x, prefabs[r].minMaxGroupSize.y);

        //int nEnemies = Random.Range(minMaxGroupSize.x, minMaxGroupSize.y);
        int spawnObjectIndex = Random.Range(0, transform.childCount);
        Transform spawnLocation = transform.GetChild(spawnObjectIndex);
        for(int i = 0; i < nEnemies; i++) {
            Instantiate(prefabs[r].prefab, spawnLocation.position + spawnLocation.right * 0.5f * (i - 0.5f * (nEnemies - 1)), spawnLocation.rotation);
        }
    }
}
