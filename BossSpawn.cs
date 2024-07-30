using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public Transform boss;
    public GameObject dragon;
    public GameObject lizard;
    public GameObject beetle;
    public Vector3 origin = Vector3.zero;
    public float radius = 1;
    bool spawned;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        if (spawned == false)
        {
            Vector3 randomPosition1 = origin + Random.insideUnitSphere * radius;
            Vector3 randomPosition2 = origin + Random.insideUnitSphere * radius;
            Vector3 randomPosition3 = origin + Random.insideUnitSphere * radius;
            if (gameObject.CompareTag("DragonSpawn"))
            {
                GameObject dragon1 = Instantiate(dragon, randomPosition1, Quaternion.identity, boss);
            }
            if (gameObject.CompareTag("LizardSpawn"))
            {
                GameObject lizard1 = Instantiate(lizard, randomPosition2, Quaternion.identity, boss);
            }
            if (gameObject.CompareTag("BeetleSpawn"))
            {
                GameObject beetle2 = Instantiate(beetle, randomPosition3, Quaternion.identity, boss);
            }
        }
        spawned = true;
    }
}
