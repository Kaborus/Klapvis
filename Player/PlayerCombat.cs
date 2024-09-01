using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player player;

    public float damage;

    public GameObject arrowPrefab;
    public GameObject bulletPrefab;

    public float meleeAttackRange = 0.5f;
    private float meleeAttackTimer = 0f;

    private float firePower;
    public float maxFirePower = 10f;

    public LayerMask enemyLayers;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void AttackWithMelee()
    {
        if (Input.GetMouseButton(0))
        {
            meleeAttackTimer += Time.deltaTime;
            //player.playerAnimation.animator.SetTrigger("Attack");

            if (meleeAttackTimer >= 1)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<MobStats>().UpdateHealth(-(player.playerStats.Strength + damage));
                }
                player.playerStats.UpdateStamina(-1f);
                meleeAttackTimer = 0;
            }
        }
        else
        {
            meleeAttackTimer = 0;
        }
    }

    public void AttackWithBow()
    {
        if (!player.playerInventory.HasItemByItemCategory(ItemCategory.Arrow))
        {
            return;
        }

        if (Input.GetMouseButton(0) && firePower < maxFirePower)
        {
            firePower += (Time.deltaTime) * maxFirePower;
        }
        else if (firePower >= maxFirePower)
        {

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (firePower > (0.6f * maxFirePower))
            {
                FireProjectile(arrowPrefab);
            }

            player.playerStats.UpdateStamina(-1f);
            firePower = 0;
        }
    }

    public void AttackWithGun()
    {
        if (!player.playerInventory.HasItemByItemCategory(ItemCategory.Bullet))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            firePower = 20f;
            FireProjectile(bulletPrefab);
            firePower = 0f;
        }
    }

    private void FireProjectile(GameObject projectilePrefab)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        if (GameManager.instance.itemManager.GetItemByName(projectilePrefab.name).GetComponent<Item>().data.itemCategory == ItemCategory.Bow)
        {
            projectile.GetComponent<Projectile>().damage = (float)Math.Round(((float)((((Math.Round((float)firePower) / (float)maxFirePower)))) * (float)projectilePrefab.GetComponent<Projectile>().damage));
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.velocity = direction * firePower;

        player.playerInventory.RemoveProjectile(projectilePrefab.gameObject.name);
    }
}
