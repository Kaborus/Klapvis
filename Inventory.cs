using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Inventory
{
    public string Name;
    public List<Slot> slots = new List<Slot>();

    public Inventory(string name, int numSlots)
    {
        this.Name = name;
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slot.slotType = SlotType.Normal;

            slots.Add(slot);
        }
    }

    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if ((slot.itemName == item.data.itemName && slot.canAddItem(item.data.itemName)) && item.data.max > slot.count)
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

    public bool InventoryIsFull() => !slots.Any(s => s.itemName == "");

    public void MoveSlot(int fromIndex, int toIndex, Inventory fromInventory, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];

        if (fromSlot.IsEmpty)
        {
            return;
        }

        Slot toSlot = toInventory.slots[toIndex];

        if (!CheckIfItemSlotHasMaxItems(fromSlot, toSlot)) return;

        if (!FromLoadoutConditions(fromSlot, toSlot, fromInventory, toInventory)) return;

        if (!CanEquipItemInInventory(fromSlot, toSlot)) return;

        LoadoutChange(fromSlot, toSlot, fromInventory, toInventory);

        MoveItems(fromSlot, toSlot, numToMove);
    }

    private void LoadoutChange(Slot fromSlot, Slot toSlot, Inventory fromInventory, Inventory toInventory)
    {
        if (fromInventory.Name == "Loadout")
        {
            Item item = GameManager.instance.itemManager.GetItemByName(fromSlot.itemName);
            GearData gearData = (GearData)item.data;
            GameManager.instance.player.stats.UpdateProtection(-(gearData.protection));
            GameManager.instance.player.stats.UpdateColdProtection(-(gearData.coldProtection));
            GameManager.instance.player.stats.UpdateHeatProtection(-(gearData.heatProtection));

            if (toSlot.itemName != "")
            {
                Item item2 = GameManager.instance.itemManager.GetItemByName(toSlot.itemName);
                GearData gearData2 = (GearData)item2.data;
                GameManager.instance.player.stats.UpdateProtection((gearData2.protection));
                GameManager.instance.player.stats.UpdateColdProtection((gearData2.coldProtection));
                GameManager.instance.player.stats.UpdateHeatProtection((gearData2.heatProtection));
            }
        }

        if (toInventory.Name == "Loadout")
        {
            Item item = GameManager.instance.itemManager.GetItemByName(fromSlot.itemName);
            GearData gearData = (GearData)item.data;
            GameManager.instance.player.stats.UpdateProtection(gearData.protection);
            GameManager.instance.player.stats.UpdateColdProtection((gearData.coldProtection));
            GameManager.instance.player.stats.UpdateHeatProtection((gearData.heatProtection));

            if (toSlot.itemName != "")
            {
                Item item2 = GameManager.instance.itemManager.GetItemByName(toSlot.itemName);
                GearData gearData2 = (GearData)item2.data;
                GameManager.instance.player.stats.UpdateProtection(-(gearData2.protection));
                GameManager.instance.player.stats.UpdateColdProtection(-(gearData2.coldProtection));
                GameManager.instance.player.stats.UpdateHeatProtection(-(gearData2.heatProtection));
            }
        }
    }

    private bool CheckIfItemSlotHasMaxItems(Slot fromSlot, Slot toSlot)
    {
        if (fromSlot.itemName == toSlot.itemName)
        {
            Item item = GameManager.instance.itemManager.GetItemByName(fromSlot.itemName);

            if (item.data.max <= fromSlot.count || item.data.max <= toSlot.count || ((fromSlot.count + toSlot.count) > item.data.max))
            {
                return false;
            }
        }
        return true;
    }

    private bool FromLoadoutConditions(Slot fromSlot, Slot toSlot, Inventory fromInventory, Inventory toInventory)
    {
        if (fromInventory.Name == "Loadout" && toInventory.Name != "Loadout")
        {
            if ((fromSlot.itemCategory != toSlot.itemCategory) && !toSlot.IsEmpty)
            {
                return false;
            }
        }
        return true;
    }

    private void MoveItems(Slot fromSlot, Slot toSlot, int numToMove)
    {
        if (toSlot.IsEmpty || toSlot.canAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.max, fromSlot.itemCategory);
                fromSlot.RemoveItem();
            }
        }
        else
        {
            SwitchItemsInSlot(fromSlot, toSlot);
        }
    }

    private bool CanEquipItemInInventory(Slot fromSlot, Slot toSlot)
    {
        if (toSlot.slotType == SlotType.Normal)
        {
            return true;
        }
        else if (toSlot.slotType.ToString() == fromSlot.itemCategory.ToString())
        {
            return true;
        }
        return false;
    }
}
