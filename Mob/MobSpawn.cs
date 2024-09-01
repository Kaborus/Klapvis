using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public Transform mobTransform;
    public GameObject mob;
    public Vector3 spawn;
    public float radius;
    public int mobAmount = 1;

    private void Start()
    {
        mobTransform = GameObject.Find("Mobs").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnMob();
        }
    }

    private void SpawnMob()
    {
        for (int i = 0; i < mobAmount; i++)
        {
            Vector3 position = spawn + Random.insideUnitSphere * radius;
            position.z = 0;

            GameObject mobToSpawn = Instantiate(mob, position, Quaternion.identity, mobTransform);
        }
    }
}
