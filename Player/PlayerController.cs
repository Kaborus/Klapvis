using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;

    public EquippedItem equippedItem;
    public Toolbar_UI toolbar_UI;
    public List<Slot> hotbar;

    public bool canUseEquippedItem = true;

    private void Start()
    {
        player = GetComponent<Player>();
        toolbar_UI = FindObjectOfType<Toolbar_UI>();
        hotbar = player.playerInventory.toolbar.slots;
    }

    private void Update()
    {
        HandleInput();
        UpdateEquippedItem();
    }

    private void HandleInput()
    {
        if (canUseEquippedItem)
            switch (equippedItem)
            {
                case EquippedItem.Melee:
                    player.playerCombat.AttackWithMelee();
                    break;
                case EquippedItem.Bow:
                    player.playerCombat.AttackWithBow();
                    break;
                case EquippedItem.Gun:
                    player.playerCombat.AttackWithGun();
                    break;
                case EquippedItem.Pickaxe:
                    UsePickaxe();
                    break;
                case EquippedItem.Axe:
                    UseAxe();
                    break;
                case EquippedItem.Consumable:
                    EatConsumable();
                    break;
            }
    }

    private void UpdateEquippedItem()
    {
        switch (hotbar[(toolbar_UI.GetSelectedSlot().slotID)].itemCategory)
        {
            case ItemCategory.MeleeWeapon:
                equippedItem = EquippedItem.Melee;
                break;
            case ItemCategory.Bow:
                equippedItem = EquippedItem.Bow;
                break;
            case ItemCategory.Gun:
                equippedItem = EquippedItem.Gun;
                break;
            case ItemCategory.Pickaxe:
                equippedItem = EquippedItem.Pickaxe;
                break;
            case ItemCategory.Axe:
                equippedItem = EquippedItem.Axe;
                break;
            case ItemCategory.Consumable:
                equippedItem = EquippedItem.Consumable;
                break;
            default:
                equippedItem = EquippedItem.None;
                break;
        }
    }

    public void EnableUseEquippedItem(bool enable)
    {
        canUseEquippedItem = enable;
    }

    private void UseTool(string tag)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(tag))
            {
                hit.collider.gameObject.GetComponent<ResourceNode>().DecreaseHealth();
            }
        }
    }

    private void UsePickaxe()
    {
        UseTool("Stone");
    }

    private void UseAxe()
    {
        UseTool("Tree");
    }

    private void EatConsumable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Item item = GameManager.instance.itemManager.GetItemByName(hotbar[(toolbar_UI.GetSelectedSlot().slotID)].itemName);
            ConsumableData consumableData = (ConsumableData)item.data;
            player.playerStats.UpdateFood(consumableData.points);
            player.playerInventory.RemoveItemFromToolbar();
            GameManager.instance.uiManager.RefreshAll();
        }
    }
}
