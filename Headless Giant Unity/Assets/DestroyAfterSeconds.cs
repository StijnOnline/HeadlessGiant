using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float deathTime = 4f;

    private void Start()
    {
        Destroy(gameObject, deathTime);
    }
}
