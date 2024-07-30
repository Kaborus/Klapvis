using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Player player;

    public Tool tool;

    public Toolbar_UI toolbar_UI;

    public List<Slot> hotbar;

    private void Awake()
    {
        player = GetComponent<Player>();
        toolbar_UI = FindObjectOfType<Toolbar_UI>();
    }

    private void Start()
    {
        hotbar = player.inventory.toolbar.slots;
    }

    void Update()
    {
        switch (hotbar[(toolbar_UI.GetSelectedSlot().slotID)].itemType)
        {
            case Category.Melee:
                MeleeEquiped();
                break;
            case Category.Range:
                RangeEquiped();
                break;
            case Category.Pickaxe:
                PickaxeEquiped();
                break;
            case Category.Axe:
                AxeEquiped();
                break;
            default:
                tool = Tool.None;
                break;
        }
    }

    public void MeleeEquiped()
    {
        tool = Tool.Melee;
    }

    public void RangeEquiped()
    {
        tool = Tool.Range;
    }

    public void PickaxeEquiped()
    {
        tool = Tool.Pickaxe;
    }

    public void AxeEquiped()
    {
        tool = Tool.Axe;
    }
}

public enum Tool
{
    None, Melee, Range, Pickaxe, Axe
}
