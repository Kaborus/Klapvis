using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public Transform items;
    public float health;
    public GameObject drop;
    public Vector3 offset;

    void Start()
    {
        items = transform.Find("Items");
    }

    public void DecreaseHealth()
    {
        health -= Time.deltaTime;
    }

    public void BreakNode()
    {
        Destroy(this.gameObject);

        Instantiate(drop, transform.position - offset, transform.rotation, items);
    }

    void Update()
    {
        if (health <= 0)
        {
            BreakNode();
        }
    }
}
