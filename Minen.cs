using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minen : MonoBehaviour
{
    public Movement movement;
    public Transform items;
    public Equipment equipment;
    private float hp = 2;
    public GameObject obj;
    public GameObject geefbareItem;

    void Start()
    {
        items = transform.Find("Items");
        movement = FindObjectOfType<Movement>();
        equipment = FindObjectOfType<Equipment>();
    }

    public void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (obj.tag == "Tree" && equipment.tool == Tool.Axe)
            {
                Hak();
            }
            if (obj.tag == "Stone" && equipment.tool == Tool.Pickaxe)
            {
                Hak();
            }
        }
    }

    public void Hak()
    {
        hp -= Time.deltaTime;
        //Debug.Log(hp);
    }

    public void HaalWegEnGeefItem()
    {
        Destroy(obj);
        Vector3 woodSpawnPosition = obj.transform.position - new Vector3(0f, 0.8f, 0f);
        Vector3 stoneSpawnPosition = obj.transform.position - new Vector3(0f, 0.2f, 0f);
        if (obj.tag == "Tree")
        {
            Instantiate(geefbareItem, woodSpawnPosition, transform.rotation, items);
        }
        if (obj.tag == "Stone")
        {
            Instantiate(geefbareItem, stoneSpawnPosition, transform.rotation, items);
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            HaalWegEnGeefItem();
        }
    }
}
