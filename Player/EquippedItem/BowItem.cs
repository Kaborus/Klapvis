using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowItem : IEquippedItem
{
    public GameObject arrowPrefab;

    private float firePower;
    public float maxFirePower = 10f;

    public void Use(PlayerController controller)
    {
        if (!controller.player.inventory.HasItemByItemCategory(ItemCategory.Arrow))
        {
            return;
        }

        Item arrowType = controller.player.inventory.GetFirstItemByCategory(ItemCategory.Arrow);

        arrowPrefab = Resources.Load<GameObject>($"Projectiles/{arrowType.data.itemName}");

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
                controller.player.combat.FireProjectile(arrowPrefab, firePower, maxFirePower);
            }

            controller.player.stats.UpdateStamina(-1f);
            firePower = 0;
        }
    }
}
