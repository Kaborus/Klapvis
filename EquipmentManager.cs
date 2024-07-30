using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int totalProtection;
    public Player player;

    public List<Slot_UI> slots;

    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    void Update()
    {
        foreach (var slot_ui in slots)
        {
            foreach (var slot in slot_ui.inventory.slots)
            {
                Item item = GameManager.instance.itemManager.GetItemByName(slot.itemName);
                if (item == null)
                {
                    return;
                }
                ArmorData armorData = (ArmorData)item.data;
                totalProtection += armorData.protection;
            }
        }
        if (totalProtection != 0)
        {
            Debug.Log(totalProtection);
        }
    }
}
