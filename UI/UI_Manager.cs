using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public GameObject inventoryPanel;
    public GameObject craftPanel;
    public List<Inventory_UI> inventoryUIs;
    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    private bool hasUIOpen = false;

    private void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryUI();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCraftMenuUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                if (hasUIOpen)
                {
                    return;
                }
                inventoryPanel.SetActive(true);
                hasUIOpen = true;
                GameManager.instance.player.playerMovement.EnableMovement(false);
                GameManager.instance.player.playerController.EnableUseEquippedItem(false);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
                hasUIOpen = false;
                GameManager.instance.player.playerMovement.EnableMovement(true);
                GameManager.instance.player.playerController.EnableUseEquippedItem(true);
            }
        }
    }

    public void ToggleCraftMenuUI()
    {
        if (craftPanel != null)
        {
            if (!craftPanel.activeSelf)
            {
                if (hasUIOpen)
                {
                    return;
                }
                craftPanel.SetActive(true);
                hasUIOpen = true;
                GameManager.instance.player.playerMovement.EnableMovement(false);
                GameManager.instance.player.playerController.EnableUseEquippedItem(false);
            }
            else
            {
                craftPanel.SetActive(false);
                hasUIOpen = false;
                GameManager.instance.player.playerMovement.EnableMovement(true);
                GameManager.instance.player.playerController.EnableUseEquippedItem(true);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach (KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }
        Debug.LogWarning("There is no inventory ui for " + inventoryName);
        return null;
    }

    void Initialize()
    {
        foreach (Inventory_UI ui in inventoryUIs)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
