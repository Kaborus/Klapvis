using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public string itemName;
    public int count;
    public int max;
    public Sprite icon;
    public Category itemType;
    public Equipable equipable;

    public Slot()
    {
        itemName = "";
        count = 0;
        max = 100;
    }

    public bool IsEmpty
    {
        get
        {
            if (itemName == "" && count == 0)
            {
                return true;
            }
            return false;
        }
    }

    public bool canAddItem(string itemName)
    {
        if (this.itemName == itemName && count < max)
        {
            return true;
        }
        return false;
    }

    public void CopySlot(Slot slot) {
        this.itemName = slot.itemName;
        this.icon = slot.icon;
        this.count = slot.count;
        this.max = slot.max;
        this.itemType = slot.itemType;
        this.equipable = slot.equipable;
    }

    public void AddItem(Item item)
    {
        this.itemName = item.data.itemName;
        this.icon = item.data.icon;
        this.itemType = item.data.category;
        count++;
    }

    public void AddItem(string itemName, Sprite icon, int maxAllowed, Category category)
    {
        this.itemName = itemName;
        this.icon = icon;
        count++;
        this.max = maxAllowed;
        this.itemType = category;
    }

    public void RemoveItem()
    {
        if (count > 0)
        {
            count--;

            if (count == 0)
            {
                icon = null;
                itemName = "";
                itemType = Category.None;
            }
        }
    }
}

public enum Equipable {
    Everything,
    Armor
}