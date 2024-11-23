using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingManager : MonoBehaviour
{
    bool hasAllItems = false;
    public Player player;
    Item item;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void CraftItem(CraftingRecipe recipe)
    {
        item = recipe.result;

        if (!HasAllRequiredItems(recipe))
        {
            return;
        }

        RemoveRequiredItems(recipe);

        AddItemToInventory(recipe);
    }

    private bool HasAllRequiredItems(CraftingRecipe recipe)
    {
        foreach (Item i in recipe.ingredients)
        {
            hasAllItems = player.inventory.backpack.slots.Any(item => item.itemName == i.name);

            if (!hasAllItems)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveRequiredItems(CraftingRecipe recipe)
    {
        foreach (Item i in recipe.ingredients)
        {
            Slot slot = player.inventory.backpack.slots.FirstOrDefault(slot => slot.itemName == i.data.name);
            slot.RemoveItem();
        }
    }

    private void AddItemToInventory(CraftingRecipe recipe)
    {
        for (int i = 0; i < recipe.amount; i++)
        {
            player.inventory.Add("Backpack", item);
        }
    }
}
