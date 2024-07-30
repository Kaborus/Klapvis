using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingManager : MonoBehaviour
{

    public void CraftItem(CraftRecipe recipe)
    {
        bool hasAllItems = false;
        int count = recipe.ingredients.Count;

        Player player = FindObjectOfType<Player>();

        if (!player)
        {
            return;
        }


        Item item = recipe.result;

        if (item == null)
        {
            return;
        }

        foreach (Item i in recipe.ingredients)
        {
            var test = player.inventory.backpack.slots.Where(i => i.count == 0);

            hasAllItems = player.inventory.backpack.slots.Any(item => item.itemName == i.name);

            if (!hasAllItems)
            {
                return;
            }
        }

        foreach (Item i in recipe.ingredients)
        {
            Slot slot = player.inventory.backpack.slots.FirstOrDefault(slot => slot.itemName == i.data.name);
            slot.RemoveItem();
        }
        for (int i = 0; i < recipe.amount; i++)
        {
            player.inventory.Add("Backpack", item);
        }

    }
}
