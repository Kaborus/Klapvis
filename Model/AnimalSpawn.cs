using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public Transform animal;
    public GameObject deer;
    public GameObject bear;
    public GameObject tiger;
    public Vector3 deerSpawn = Vector3.zero;
    public Vector3 bearSpawn = Vector3.zero;
    public Vector3 tigerSpawn = Vector3.zero;
    public float radius = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector3 deerPosition = deerSpawn + Random.insideUnitSphere * radius;
        Vector3 bearPosition = bearSpawn + Random.insideUnitSphere * radius;
        Vector3 tigerPosition = tigerSpawn + Random.insideUnitSphere * radius;
        GameObject animal1 = Instantiate(deer, deerPosition, Quaternion.identity, animal);
        GameObject animal2 = Instantiate(bear, bearPosition, Quaternion.identity, animal);
        GameObject animal3 = Instantiate(tiger, tigerPosition, Quaternion.identity, animal);
    }
}
