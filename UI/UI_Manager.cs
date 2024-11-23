using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public GameObject inventoryPanel;
    public GameObject craftPanel;
    public GameObject pausePanel;

    public Canvas inventoryCanvas;
    public Canvas craftCanvas;
    public Canvas pauseCanvas;

    public List<Inventory_UI> inventoryUIs;
    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    private bool hasUIOpen = false;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenuUI();
        }

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

    public void TogglePauseMenuUI()
    {
        if (pausePanel != null)
        {
            if (!pausePanel.activeSelf)
            {
                if (hasUIOpen)
                {
                    return;
                }
                pausePanel.SetActive(true);
                hasUIOpen = true;
                GameManager.instance.player.movement.EnableMovement(false);
                GameManager.instance.player.controller.EnableUseEquippedItem(false);

                // Zet sorteerorde van canvas
                pauseCanvas.sortingOrder = 0;
                inventoryCanvas.sortingOrder = -1;  // of een ander getal
                craftCanvas.sortingOrder = -1;    // Een lager getal dan inventoryCanvas
                RefreshInventoryUI("Backpack");
            }
            else
            {
                pausePanel.SetActive(false);
                hasUIOpen = false;
                GameManager.instance.player.movement.EnableMovement(true);
                GameManager.instance.player.controller.EnableUseEquippedItem(true);
            }
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
                GameManager.instance.player.movement.EnableMovement(false);
                GameManager.instance.player.controller.EnableUseEquippedItem(false);

                // Zet sorteerorde van canvas
                inventoryCanvas.sortingOrder = 0;  // of een ander getal
                craftCanvas.sortingOrder = -1;    // Een lager getal dan inventoryCanvas
                pauseCanvas.sortingOrder = -1;
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
                hasUIOpen = false;
                GameManager.instance.player.movement.EnableMovement(true);
                GameManager.instance.player.controller.EnableUseEquippedItem(true);
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
                GameManager.instance.player.movement.EnableMovement(false);
                GameManager.instance.player.controller.EnableUseEquippedItem(false);
                // Zet sorteerorde van canvas
                craftCanvas.sortingOrder = 0;  // of een ander getal
                inventoryCanvas.sortingOrder = -1;    // Een lager getal dan craftCanvas
                pauseCanvas.sortingOrder = -1;
            }
            else
            {
                craftPanel.SetActive(false);
                hasUIOpen = false;
                GameManager.instance.player.movement.EnableMovement(true);
                GameManager.instance.player.controller.EnableUseEquippedItem(true);
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

    private void Initialize()
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
