using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnHealthChange;
    public float maxHealth;
    public float health;
    public float regenerationTime = 0;

    public float maxStrength;
    public float strength;

    public float maxFood;
    public float food;

    public Equipment equipment;
    public GameObject arrowPrefab;
    public float arrowSpeed = 10f;
    public InventoryManager inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        health = maxHealth;
        food = maxFood;
    }

    void Start()
    {
        equipment = FindObjectOfType<Equipment>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        }

        if (equipment.tool == Tool.Range && Input.GetMouseButtonDown(0))
        {
            if (!inventory.backpack.slots.Any(item => item.itemType == Category.Arrow))
            {
                return;
            }

            ShootArrow();
            inventory.backpack.slots.FirstOrDefault(item => item.itemType == Category.Arrow).RemoveItem();
        }


        Regenerate();
    }

    public void Regenerate()
    {
        if (health < maxHealth)
        {
            regenerationTime += Time.deltaTime;
        }

        if (regenerationTime >= 1)
        {
            health++;
            DrainFood();
            OnHealthChange?.Invoke();
            regenerationTime = 0;
        }
    }

    public void DrainFood()
    {
        food--;
    }

    public void TakeDamage(Mob mob)
    {
        health -= mob.damage;
    }

    public void HandleAnimalDeath(Mob mob)
    {
        strength += mob.strengthValue;
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle * 1.25f;
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public void ShootArrow()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * arrowSpeed;
    }
}
