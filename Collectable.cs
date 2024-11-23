using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    public Inventory_UI ui;
    public float pickupRange = 1f;
    public bool canPickup = false;

    private void Awake()
    {
        ui = FindObjectOfType<Inventory_UI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.player.movement.canMove)
        {
            Player player = collision.GetComponent<Player>();

            if (player)
            {
                float distance = Vector2.Distance(player.transform.position, transform.position) - 1;
                if ((distance <= pickupRange))
                {
                    canPickup = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            canPickup = false;
        }
    }

    private void Update()
    {
        PickUpItem();
    }

    private void PickUpItem()
    {
        if (canPickup && Input.GetKey(KeyCode.Space))
        {

            if (GameManager.instance.player.inventory.backpack.InventoryIsFull()) return;


            Player player = FindObjectOfType<Player>();

            if (player)
            {
                Item item = GetComponent<Item>();

                if (item != null)
                {
                    player.inventory.Add("Backpack", item);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}