using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public Toolbar_UI toolbar_UI;
    public List<Slot> hotbar;

    private IEquippedItem equippedItem;

    private bool canUseEquippedItem = true;

    private bool inTundra = false;
    private bool inDesert = false;

    private void Start()
    {
        player = GetComponent<Player>();
        toolbar_UI = FindObjectOfType<Toolbar_UI>();
        toolbar_UI.OnSelectedSlotChange += UpdateEquippedItem;
        hotbar = player.inventory.toolbar.slots;
        SetEquippedItem(new NoItem());
    }

    private void Update()
    {
        UseItem();

        if (inTundra)
        {
            if (player.stats.ColdProtection < 75)
            {
                player.stats.LoseHealth(-1.0f);
            }
        }
        else if (inDesert)
        {
            if (player.stats.HeatProtection < 75)
            {
                player.stats.LoseHealth(-1.0f);
            }
        }
    }

    private void SetEquippedItem(IEquippedItem equippedItem) => this.equippedItem = equippedItem;

    private void UseItem() => equippedItem.Use(this);

    public void UpdateEquippedItem()
    {
        switch (hotbar[(toolbar_UI.GetSelectedSlot().slotID)].itemCategory)
        {
            case ItemCategory.MeleeWeapon:
                SetEquippedItem(new MeleeItem());
                break;
            case ItemCategory.Bow:
                SetEquippedItem(new BowItem());
                break;
            case ItemCategory.Pickaxe:
                SetEquippedItem(new PickaxeItem());
                break;
            case ItemCategory.Axe:
                SetEquippedItem(new AxeItem());
                break;
            case ItemCategory.Consumable:
                ConsumableItem ci = new ConsumableItem();
                SetEquippedItem(ci);
                ci.OnConsumated += UpdateEquippedItem;
                break;
            default:
                SetEquippedItem(new NoItem());
                break;
        }
    }

    public void EnableUseEquippedItem(bool enable) => canUseEquippedItem = enable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tundra"))
        {
            inTundra = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Desert"))
        {
            inDesert = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tundra"))
        {
            inTundra = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Desert"))
        {
            inDesert = false;
        }
    }
}
