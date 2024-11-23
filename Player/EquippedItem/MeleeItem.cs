using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeItem : IEquippedItem
{
    public float meleeAttackRange = 0.5f;
    private float meleeAttackTimer = 0f;

    public LayerMask enemyLayers;

    public float damage = 10;

    private void Start()
    {
        enemyLayers = LayerMask.GetMask("Mob");
    }

    public void Use(PlayerController controller)
    {
        if (Input.GetMouseButton(0))
        {
            meleeAttackTimer += Time.deltaTime;
            //player.playerAnimation.animator.SetTrigger("Attack");

            if (meleeAttackTimer >= 1)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(controller.player.transform.position, meleeAttackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<MobStats>().UpdateHealth(-(controller.player.stats.Strength + damage));
                }
                controller.player.stats.UpdateStamina(-1f);
                meleeAttackTimer = 0;
            }
        }
        else
        {
            meleeAttackTimer = 0;
        }
    }
}
