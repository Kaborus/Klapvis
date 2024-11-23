using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : IEquippedItem
{
    public event Action OnConsumated;

    public void Use(PlayerController controller)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Item item = GameManager.instance.itemManager.GetItemByName(controller.hotbar[(controller.toolbar_UI.GetSelectedSlot().slotID)].itemName);
            ConsumableData consumableData = (ConsumableData)item.data;
            GameManager.instance.player.stats.UpdateFood(consumableData.points);
            GameManager.instance.player.inventory.RemoveItemFromToolbar();
            GameManager.instance.uiManager.RefreshAll();
            OnConsumated?.Invoke();
        }
    }
}
