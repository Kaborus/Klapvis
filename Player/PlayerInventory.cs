using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Player player;
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    [Header("Loadout")]
    public Inventory loadout;
    public int loadoutSlotCount;

    public List<SlotType> slotTypes = new List<SlotType>() { SlotType.HeadGear, SlotType.BodyGear, SlotType.HandGear, SlotType.LegGear, SlotType.FootGear };

    private void Awake()
    {
        backpack = new Inventory("Backpack", backpackSlotCount);
        toolbar = new Inventory("Toolbar", toolbarSlotCount);
        loadout = new Inventory("Loadout", loadoutSlotCount);

        for (int i = 0; i < loadout.slots.Count; i++)
        {
            loadout.slots[i].max = 1;
            loadout.slots[i].slotType = slotTypes[i];
        }

        inventoryByName.Add(backpack.Name, backpack);
        inventoryByName.Add(toolbar.Name, toolbar);
        inventoryByName.Add(loadout.Name, loadout);
    }

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null;
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle * 1.25f;
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public void RemoveItemFromToolbar()
    {
        toolbar.slots[(player.controller.toolbar_UI.GetSelectedSlot().slotID)].RemoveItem();
    }

    public void RemoveProjectile(string itemName)
    {
        Slot slot = player.inventory.backpack.slots.FirstOrDefault(slot => slot.itemName == itemName);

        if (slot == null)
        {
            slot = player.inventory.toolbar.slots.FirstOrDefault(slot => slot.itemName == itemName);
        }

        if (slot != null)
        {
            slot.RemoveItem();
            GameManager.instance.uiManager.RefreshAll();
        }
    }

    public bool HasItemByItemCategory(ItemCategory itemCategory)
    {
        if (player.inventory.backpack.slots.Any(item => item.itemCategory == itemCategory))
        {
            return true;
        }
        else if (player.inventory.toolbar.slots.Any(item => item.itemCategory == itemCategory))
        {
            return true;
        }

        return false;
    }

    public Item GetFirstItemByCategory(ItemCategory itemCategory)
    {
        Slot slot = backpack.slots.FirstOrDefault(i => i.itemCategory == ItemCategory.Arrow);

        Item item = GameManager.instance.itemManager.GetItemByName(slot.itemName);

        return item;
    }
}
