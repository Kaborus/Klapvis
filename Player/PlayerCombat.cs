using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void FireProjectile(GameObject projectilePrefab, float firePower, float maxFirePower)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float offset = -45f;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + offset)));

        if (GameManager.instance.itemManager.GetItemByName(projectilePrefab.name).GetComponent<Item>().data.itemCategory == ItemCategory.Bow)
        {
            projectile.GetComponent<Projectile>().damage = (float)Math.Round(((float)((((Math.Round((float)firePower) / (float)maxFirePower)))) * (float)projectilePrefab.GetComponent<Projectile>().damage));
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.velocity = direction * firePower;

        GameManager.instance.player.inventory.RemoveProjectile(projectilePrefab.gameObject.name);
    }

}
