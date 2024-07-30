using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Inventory
{
    //slot class

    public List<Slot> slots = new List<Slot>();
    public Equipable equipable;

    public Inventory(int numSlots, Equipable equipable = Equipable.Everything)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slot.equipable = equipable;
            slots.Add(slot);
        }
    }

    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.canAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void SwitchItemsInSlot(Slot fromSlot, Slot toSlot)
    {
        Slot slot = new Slot();

        slot.CopySlot(fromSlot);

        fromSlot.CopySlot(toSlot);
        toSlot.CopySlot(slot);
    }

    public void Remove(string name)
    {
        Slot slot = slots.FirstOrDefault(item => item.itemName == name);
        slot.count--;
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (fromSlot.IsEmpty)
        {
            return;
        }

        if (!CanEquipItemInInventory(fromSlot, toSlot))
        {
            return;
        }

        if (!toSlot.IsEmpty)
        {
            SwitchItemsInSlot(fromSlot, toSlot);
            return;
        }
        else if (toSlot.IsEmpty || toSlot.canAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.max, fromSlot.itemType);
                fromSlot.RemoveItem();
            }
        }


    }

    public bool CanEquipItemInInventory(Slot fromSlot, Slot toSlot)
    {
        if (toSlot.equipable == Equipable.Armor)
        {
            if (toSlot.equipable.ToString() != fromSlot.itemType.ToString())
            {
                return false;
            }
        }
        return true;
    }
}
