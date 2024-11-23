using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public Transform items;
    public Item item;

    private void Start() => items = transform.Find("Items");

    public void GiveResource() => GameManager.instance.player.inventory.Add("Backpack", item);
}
