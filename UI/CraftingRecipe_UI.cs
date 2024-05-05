using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipe_UI : MonoBehaviour
{
    public int recipeID;
    public Inventory inventory;
    public CraftingManager craftingManager;
    public Image itemIcon;

    private void Start()
    {
        craftingManager = FindObjectOfType<CraftingManager>();
    }

    public void Craft(CraftRecipe recipe) {
        this.craftingManager.CraftItem(recipe);
    }
}